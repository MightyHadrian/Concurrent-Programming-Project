using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Timers;
using Dane;

namespace Logika
{
    internal enum EventType
    {
        WallCollision,
        BallsCollision,
        Start,
        Reset,
        Stop,
        Restart
    }

    internal class Event
    {
        private readonly EventType _eventType;
        private readonly DateTime _time;
        private readonly int _reportingId;
        private readonly int _collidingId;

        public Event(int reportingId, int collidingId, EventType eventType)
        {
            _time = DateTime.Now;
            _reportingId = reportingId;
            _collidingId = collidingId;
            _eventType = eventType;
        }

        public Event(int reportingId, EventType eventType)
        {
            _time = DateTime.Now;
            _reportingId = reportingId;
            _eventType = eventType;
        }

        public Event(EventType eventType)
        {
            _time = DateTime.Now;
            _eventType = eventType;
        }

        public string Type
        {
            get => _eventType.ToString();
        }

        public DateTime Time 
        {
            get => _time;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ReportingId
        {
            get => _reportingId;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int CollidingId
        {
            get => _collidingId;
        }

    }

    public class Logic : ILogic
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly System.Timers.Timer _logTimer;
        private CancellationTokenSource _cancelToken;
        private CancellationTokenSource _stopToken;
        private readonly ObservableCollection<DataController> _balls;
        private readonly ObservableCollection<Task> _tasks;
        private readonly List<Event> _events;

        public Logic()
        {
            _filePath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName
                + "/logs_" + DateTime.Now.ToShortDateString() + ".json";

            File.Create(_filePath);

            _logTimer = new(1000);
            _logTimer.Elapsed += OnTimedLogEvents;
            _logTimer.AutoReset = true;
            _logTimer.Enabled = true;

            _serializerOptions = new()
            {
                WriteIndented = true
            };

            _cancelToken = new();
            _stopToken = new();
            _balls = new();
            _tasks = new();
            _events = new();
        }

        public void Start(int amount, int size, int width, int height, float velX, float velY)
        {
            _events.Add(new Event(EventType.Start));

            ThreadPool.SetMinThreads(amount, amount);
            Clear();

            for (int i = 0; i < amount; i++)
            {
                DataController data = DataController.Create(new Ball(size, width, height, velX, velY));

                _balls.Add(data);

                _tasks.Add(CreateDataTask(data));
            }

            foreach (var task in _tasks)
            {
                task.Start();
            }
        }

        public void Restart()
        {
            _events.Add(new Event(EventType.Restart));

            _stopToken.Dispose();

            _stopToken = new();
        }

        public void Reset(int size, int width, int height, float velX, float velY)
        {
            _events.Add(new Event(EventType.Reset));

            _stopToken.Dispose();

            _stopToken = new();

            int amount = _balls.Count;
            ThreadPool.SetMinThreads(amount, amount);
            Clear();

            for (int i = 0; i < amount; i++)
            {
                DataController data = DataController.Create(new Ball(size, width, height, velX, velY));

                _balls.Add(data);

                _tasks.Add(CreateDataTask(data));
            }

            foreach (var task in _tasks)
            {
                task.Start();
            }
        }

        public void Stop()
        {
            _events.Add(new Event(EventType.Stop));

            _stopToken.Cancel();
        }

        public Task CreateDataTask(DataController data)
        {
            return new Task(() =>
            {
                long currentTicks, previousTicks = 0;
                int elapsedTicks;

                Stopwatch stopWatch = new();
                stopWatch.Start();

                while (!_cancelToken.IsCancellationRequested)
                {
                    currentTicks = stopWatch.ElapsedMilliseconds;
                    elapsedTicks = (int)(currentTicks - previousTicks);

                    while (_stopToken.IsCancellationRequested)
                    {
                        currentTicks = stopWatch.ElapsedMilliseconds;
                        previousTicks = currentTicks;
                        Thread.Sleep(50);
                    }

                    //
                    // Start of critical section
                    //
                    MoveWithTime(data, elapsedTicks);
                    CheckForWallCollisions(data);
                    CheckForBallCollisions(data);
                    //
                    // End of critical section
                    //

                    previousTicks = currentTicks;

                    Thread.Sleep(10);
                }

                stopWatch.Stop();
                Task.FromResult(0);
            });
        }

        public void Move(DataController data)
        {
            lock (data)
            {
                data.X += data.VelX;
                data.Y += data.VelY;
            }
        }

        public static void MoveWithTime(DataController data, int ticks)
        {
            lock (data)
            {
                data.X += (data.VelX * ticks / 10.0f);
                data.Y += (data.VelY * ticks / 10.0f);
            }
        }

        public void CheckForWallCollisions(DataController data)
        {
            float newX = data.X, newY = data.Y, newVelX = data.VelX, newVelY = data.VelY;

            if (data.X <= 0)
            {
                newX = 0;
                newVelX *= -1;
                _events.Add(new Event(data.GetHashCode(), EventType.WallCollision));
            }
            else if ((data.X + data.Size) >= data.Width)
            {
                newX = data.Width - data.Size;
                newVelX *= -1;
                _events.Add(new Event(data.GetHashCode(), EventType.WallCollision));
            }

            if (data.Y <= 0)
            {
                newY = 0;
                newVelY *= -1;
                _events.Add(new Event(data.GetHashCode(), EventType.WallCollision));
            }
            else if ((data.Y + data.Size) >= data.Height)
            {
                newY = data.Height - data.Size;
                newVelY *= -1;
                _events.Add(new Event(data.GetHashCode(), EventType.WallCollision));
            }

            lock (data)
            {
                data.X = newX;
                data.Y = newY;
                data.VelX = newVelX;
                data.VelY = newVelY;
            }
        }

        public void CheckForBallCollisions(DataController data)
        {
            float distanceBetweenCenters, distanceToCollision, distanceDifference,
                newBallVelX, newBallVelY, newDataVelX, newDataVelY,
                ballRadius, dataRadius, oldDataX, oldDataY;

            foreach (var ball in _balls)
            {
                if (ball.Equals(data))
                {
                    continue;
                }

                ballRadius = ball.Size / 2.0f;
                dataRadius = data.Size / 2.0f;

                distanceBetweenCenters = Vector2.Distance(new(data.X + dataRadius, data.Y + dataRadius), new(ball.X + ballRadius, ball.Y + ballRadius));

                distanceToCollision = dataRadius + ballRadius;

                if (distanceBetweenCenters < distanceToCollision)
                {
                    _events.Add(new Event(data.GetHashCode(), ball.GetHashCode(), EventType.BallsCollision));

                    distanceDifference = distanceToCollision - distanceBetweenCenters;

                    if (data.Mass == ball.Mass)
                    {
                        // Swapping velocities if balls have equal mass
                        newDataVelX = ball.VelX;
                        newDataVelY = ball.VelY;

                        newBallVelX = data.VelX;
                        newBallVelY = data.VelY;
                    }
                    else
                    {
                        // Calculating new velocities based on elastic collision equation
                        newBallVelX = (ball.VelX * (ball.Mass - data.Mass) + 2 * data.Mass * data.VelX) / (ball.Mass + data.Mass);
                        newBallVelY = (ball.VelY * (ball.Mass - data.Mass) + 2 * data.Mass * data.VelY) / (ball.Mass + data.Mass);

                        newDataVelX = (data.VelX * (data.Mass - ball.Mass) + 2 * ball.Mass * ball.VelX) / (ball.Mass + data.Mass);
                        newDataVelY = (data.VelY * (data.Mass - ball.Mass) + 2 * ball.Mass * ball.VelY) / (ball.Mass + data.Mass);
                    }

                    // Calculating seperation vectors
                    distanceDifference = distanceToCollision - distanceBetweenCenters;

                    lock (data)
                    {
                        data.VelX = newDataVelX;
                        data.VelY = newDataVelY;

                        oldDataX = data.X;
                        oldDataY = data.Y;

                        data.X += (data.X - ball.X) / distanceBetweenCenters * distanceDifference;
                        data.Y += (data.Y - ball.Y) / distanceBetweenCenters * distanceDifference;
                    }

                    lock (ball)
                    {
                        ball.VelX = newBallVelX;
                        ball.VelY = newBallVelY;

                        ball.X += (ball.X - oldDataX) / distanceBetweenCenters * distanceDifference;
                        ball.Y += (ball.Y - oldDataY) / distanceBetweenCenters * distanceDifference;
                    }
                }
            }
        }

        public ObservableCollection<DataController> GetCollection()
        {
            return _balls;
        }

        public void Clear()
        {
            if (_tasks.Count > 0)
            {
                _cancelToken.Cancel();

                Task.WaitAll(_tasks.ToArray());

                _tasks.Clear();

                _cancelToken.Dispose();

                _cancelToken = new();

                _balls.Clear();
            }
        }

        private void OnTimedLogEvents(object? source, ElapsedEventArgs e)
        {
            if (_events.Count > 0)
            {
                lock (_events)
                {
                    File.AppendAllTextAsync(_filePath, JsonSerializer.Serialize(_events, _serializerOptions));

                    _events.Clear();
                }
            }
        }

    }

}
