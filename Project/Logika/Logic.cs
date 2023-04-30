using System.Collections.ObjectModel;
using System.Numerics;
using Dane;

namespace Logika
{
    public class Logic : ILogic
    {
        private readonly Mutex _mutex;
        private CancellationTokenSource _cancelToken;
        private CancellationTokenSource _stopToken;
        private readonly ObservableCollection<DataController> _balls;
        private readonly ObservableCollection<Task> _tasks;

        public Logic()
        {
            _mutex = new();
            _cancelToken = new();
            _stopToken = new();
            _balls = new();
            _tasks = new();
        }

        public void Start(int amount, int size, int width, int height, float velX, float velY)
        {
            ThreadPool.SetMinThreads(amount, amount / 2);
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
            _stopToken.Dispose();

            _stopToken = new();
        }

        public void Reset(int size, int width, int height, float velX, float velY)
        {
            int amount = _balls.Count;
            ThreadPool.SetMinThreads(amount, amount / 2);
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
            _stopToken.Cancel();
        }

        public Task CreateDataTask(DataController data)
        {
            return new Task(() =>
            {
                while (!_cancelToken.IsCancellationRequested)
                {
                    while (_stopToken.IsCancellationRequested) {
                        Thread.Sleep(50);
                    }

                    Thread.Sleep(10);

                    //
                    // Start of critical section
                    //
                    lock (data)
                    {
                        Move(data);
                        CheckForWallCollisions(data);
                    }

                    _mutex.WaitOne();

                    CheckForBallCollisions(data);

                    _mutex.ReleaseMutex();
                    //
                    // End of critical section
                    //
                }

                Task.FromResult(0);
            });
        }

        public void Move(DataController data)
        {
            lock (data) { 
                data.X += data.VelX;
                data.Y += data.VelY;
            }
        }

        public void CheckForWallCollisions(DataController data)
        {
            if (data.X <= 0)
            {
                data.X = 0;
                data.VelX *= -1;
            }
            else if ((data.X + data.Size) >= data.Width)
            {
                data.X = data.Width - data.Size;
                data.VelX *= -1;
            }

            if (data.Y <= 0)
            {
                data.Y = 0;
                data.VelY *= -1;
            }
            else if ((data.Y + data.Size) >= data.Height)
            {
                data.Y = data.Height - data.Size;
                data.VelY *= -1;
            }
        }

        public void CheckForBallCollisions(DataController data)
        {
            float distanceBetweenCenters, distanceToCollision, distanceDifference;
            float newBallVelX, newBallVelY, newDataVelX, newDataVelY;
            float ballRadius, dataRadius;

            foreach (var ball in _balls) 
            {
                if (ball.Equals(data))
                {
                    continue;
                }

                ballRadius = ball.Size / 2;
                dataRadius = data.Size / 2;

                distanceBetweenCenters = Vector2.Distance(new(data.X + dataRadius, data.Y + dataRadius), new(ball.X + ballRadius, ball.Y + ballRadius));

                distanceToCollision = dataRadius + ballRadius;

                if (distanceBetweenCenters < distanceToCollision)
                {
                    if (data.Mass == ball.Mass)
                    {
                        // Swapping velocities if balls have equal mass
                        (data.VelX, ball.VelX) = (ball.VelX, data.VelX);
                        (data.VelY, ball.VelY) = (ball.VelY, data.VelY);
                    }
                    else
                    {
                        // Calculating new velocities based on elastic collision equation
                        newBallVelX = (ball.VelX * (ball.Mass - data.Mass) + 2 * data.Mass * data.VelX) / (ball.Mass + data.Mass);
                        newBallVelY = (ball.VelY * (ball.Mass - data.Mass) + 2 * data.Mass * data.VelY) / (ball.Mass + data.Mass);

                        newDataVelX = (data.VelX * (data.Mass - ball.Mass) + 2 * ball.Mass * ball.VelX) / (ball.Mass + data.Mass);
                        newDataVelY = (data.VelY * (data.Mass - ball.Mass) + 2 * ball.Mass * ball.VelY) / (ball.Mass + data.Mass);

                        ball.VelX = newBallVelX;
                        ball.VelY = newBallVelY;

                        data.VelX = newDataVelX;
                        data.VelY = newDataVelY;
                    }

                    // Calculating seperation vectors
                    distanceDifference = distanceToCollision - distanceBetweenCenters;

                    data.X += (data.X - ball.X) / distanceBetweenCenters * distanceDifference;
                    data.Y += (data.Y - ball.Y) / distanceBetweenCenters * distanceDifference;
                    ball.X += (ball.X - data.X) / distanceBetweenCenters * distanceDifference;
                    ball.Y += (ball.Y - data.Y) / distanceBetweenCenters * distanceDifference;
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

    }

}
