using System.Collections.ObjectModel;
using Dane;

namespace Logika
{
    public interface ILogic
    {
        public void Start(int amount, int size, int width, int height, float velX, float velY);
        public void Restart();
        public void Reset(int size, int width, int height, float velX, float velY);
        public void Stop();
        public Task CreateDataTask(DataController data);
        public void Move(DataController data);
        public void CheckForWallCollisions(DataController data);
        public void CheckForBallCollisions(DataController data);
        public ObservableCollection<DataController> GetCollection();
        public void Clear();
    }
}
