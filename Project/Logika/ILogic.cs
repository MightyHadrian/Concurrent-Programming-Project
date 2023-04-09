using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dane;

namespace Logika
{
    interface ILogic
    {
        public void ResetBalls(int size, float x, float y, float velX, float velY, float minVel);
        public void UpdateBalls();
        public IReadOnlyList<Ball> GetBalls();
        public int GetBallSize(int index);
        public Tuple<float, float> GetBallXY(int index);
        public void ClearBalls();
    }
}
