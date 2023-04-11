using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logika;
using Dane;
using System.Drawing;
using System.Diagnostics;

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
            return _logic.GetList();
        }

        public void Start(int size, float x, float y, float velX, float velY)
        {
            _logic.Start(_amount, size, x, y, velX, velY);
        }

        public void Reset(int size, float x, float y, float velX, float velY)
        {
            _logic.Reset(size, x, y, velX, velY);
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
