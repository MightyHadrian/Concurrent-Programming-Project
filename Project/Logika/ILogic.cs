using System.Collections.ObjectModel;
using System.ComponentModel;
using Dane;

namespace Logika
{
    public interface ILogic : INotifyPropertyChanged
    {
        public ILogic Create();
        public void Start(int amount, int size, int width, int height, float velX, float velY);
        public void Restart();

        public void Reset(int size, int width, int height, float velX, float velY);
        public void Stop();
        public Task CreateDataTask(DataController data);
        public void CheckForWallCollision(DataController data);
        public ObservableCollection<DataController> GetCollection();
        public void Clear();
        public void RaisePropertyChanged(string name);
    }
}
