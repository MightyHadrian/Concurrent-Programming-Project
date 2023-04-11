using Dane;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public class LogicController
    {
        private readonly ILogic _ilogic;

        public LogicController(ILogic ilogic)
        {
            _ilogic = ilogic;
        }

        static public LogicController Create(ILogic logic)
        {
            return new LogicController(logic);
        }

        public void Start(int amount, int size, float x, float y, float velX, float velY)
        {

            _ilogic.Start(amount, size, x, y, velX, velY);
        }

        public void Reset(int size, float x, float y, float velX, float velY)
        {
            _ilogic.Reset(size, x, y, velX, velY);
        }

        public void Update() 
        { 
            _ilogic.Update(); 
        
        }
        public ObservableCollection<DataController> GetList() 
        {
            return _ilogic.GetList();
        }

        public int GetSize(int index) 
        {
            return _ilogic.GetSize(index);
        }

        public Tuple<float, float> GetPositionXY(int index) 
        {
            return _ilogic.GetPositionXY(index);
        }

        public void Clear()
        {
            _ilogic.Clear();
        }

    }

}
