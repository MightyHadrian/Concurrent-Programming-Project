using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dane;

namespace Logika
{
    public class Logic : ILogic
    {
        private List<Ball> balls;

        public Logic()
        {
            balls = new();
        }

        public Logic(int amount, int size, float x, float y, float velX, float velY, float minVel)
        {
            balls = new();


            for (int i = 0; i < amount; i++)
            {
                balls.Add(new Ball(size, x, y, velX, velY, minVel));
            }
        }

        public void ResetBalls(int size, float x, float y, float velX, float velY, float minVel)
        {
            int amount = balls.Count;
            balls.Clear();

            for (int i = 0; i < amount; i++)
            {
                balls.Add(new Ball(size, x, y, velX, velY, minVel));
            }
        }

        public void UpdateBalls()
        {
            foreach (var ball in balls)
            {
                ball.X += ball.VelX;
                ball.Y += ball.VelY;
            }
        }

        public IReadOnlyList<Ball> GetBalls()
        {
            return balls;
        }

        public int GetBallSize(int index)
        {
            return balls[index].Size;
        }

        public Tuple<float, float> GetBallXY(int index)
        {
            return new Tuple<float, float>(balls[index].X, balls[index].Y);
        }

        public void ClearBalls()
        {
            balls.Clear();
        }
    }
}
