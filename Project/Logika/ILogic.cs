using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dane;

namespace Logika
{
    public interface ILogic
    {
        public void Start(int amount, int size, float x, float y, float velX, float velY);
        public ILogic Create();
        public void Reset(int size, float x, float y, float velX, float velY);
        public void Update();
        public ObservableCollection<DataController> GetList();
        public int GetSize(int index);
        public Tuple<float, float> GetPositionXY(int index);
        public void Clear();
    }
}
