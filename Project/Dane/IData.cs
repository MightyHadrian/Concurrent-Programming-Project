using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public interface IData
    {
        public IData Create(int size, float x, float y, float velX, float velY);
        int Size { get; }
        float X { get; set; }
        float Y { get; set; }
        float VelX { get; set; }
        float VelY { get; set; }
    }
}
