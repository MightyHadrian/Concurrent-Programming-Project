using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
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
        public event PropertyChangedEventHandler? PropertyChanged;

        public Logic()
        {
            _balls = new();
            _tasks = new();  
            _mutex = new();
            _cancelToken = new();
            _stopToken = new();
        }

        public ILogic Create()
        {
            return new Logic();
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
        }

        public void Stop()
        {
            _stopToken.Cancel();
        }

        public async Task CreateDataTask(DataController data)
        {
            await Task.Run(() => {
                while (!_cancelToken.IsCancellationRequested)
                {
                    while (_stopToken.IsCancellationRequested) { }

                    Thread.Sleep(10);
                    //await Task.Delay(10);

                    _mutex.WaitOne();

                        // Critical section
                        data.Move();
                        CheckForWallCollision(data);

                    _mutex.ReleaseMutex();
                }
            });
        }

        public void CheckForWallCollision(DataController data)
        {
            if (data.X <= 0) 
            {
                data.X = 0;
                data.VelX *= -1;
            } 
            else if ((data.X + data.Size + 2) >= data.Width)
            {
                // Minus 2 due to border thickness
                data.X = data.Width - data.Size - 2;
                data.VelX *= -1;
            }

            if (data.Y <= 0)
            {
                data.Y = 0;
                data.VelY *= -1;
            }
            else if ((data.Y + data.Size + 2) >= data.Height)
            {
                // Minus 2 due to border thickness
                data.Y = data.Height - data.Size - 2;
                data.VelY *= -1;
            }
        }

        public ObservableCollection<DataController> GetCollection()
        {
            return _balls;
        }

        public void Clear()
        {
            _balls.Clear();
           
            if (_tasks.Count > 0) 
            {
                _cancelToken.Cancel();

                Thread.Sleep(20);

                _tasks.Clear();

                _cancelToken.Dispose();

                _cancelToken = new();
            }
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

}
