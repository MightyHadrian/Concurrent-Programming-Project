using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    interface IBall
    {
        int Size { get; }
        float X { get; set; }
        float Y { get; set; }
        float VelX { get; set; }
        float VelY { get; set; }
    }
}
