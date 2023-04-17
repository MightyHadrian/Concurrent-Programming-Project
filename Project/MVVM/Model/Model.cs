using System.Collections.ObjectModel;
using Logika;
using Dane;

namespace PrezentacjaModel
{
    public class Model
    {
        private int _amount;
        private readonly LogicController _logic;

        public Model() 
        {
            _logic = LogicController.Create(new Logic());
        }

        static public Model Create()
        {
            return new Model();
        }

        public ObservableCollection<DataController> GetCollection()
        {
            return _logic.GetCollection();
        }

        public void Start(int size, int width, int height, float velX, float velY)
        {
            _logic.Start(_amount, size, width, height, velX, velY);
        }

        public void Restart()
        {
            _logic.Restart();
        }

        public void Reset(int size, int width, int height, float velX, float velY)
        {
            _logic.Reset(size, width, height, velX, velY);
        }

        public void Stop()
        {
            _logic.Stop();
        }

        public int GetNewObjectsAmount()
        {
            return _amount;
        }

        public void SetNewObjectsAmount(int amount)
        {
            _amount = amount;
        }

    }

}
