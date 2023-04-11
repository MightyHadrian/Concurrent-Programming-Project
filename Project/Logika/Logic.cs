using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        private ObservableCollection<DataController> _balls;

        public Logic()
        {
            _balls = new();
        }

        public Logic(int amount, int size, float x, float y, float velX, float velY)
        {
            _balls = new();


            for (int i = 0; i < amount; i++)
            {
                DataController data = DataController.Create(new Ball(size, x, y, velX, velY));
                _balls.Add(data);
            }
        }

        public ILogic Create()
        {
            return new Logic();
        }

        public void Start(int amount, int size, float x, float y, float velX, float velY)
        {
            _balls.Clear();

            for (int i = 0; i < amount; i++)
            {
                DataController data = DataController.Create(new Ball(size, x, y, velX, velY));
                _balls.Add(data);
            }
        }

        public void Reset(int size, float x, float y, float velX, float velY)
        {
            int amount = _balls.Count;
            _balls.Clear();

            for (int i = 0; i < amount; i++)
            {
                DataController data = DataController.Create(new Ball(size, x, y, velX, velY));
                _balls.Add(data);
            }
        }

        public void Update()
        {
            foreach (var ball in _balls)
            {
                ball.X += ball.VelX;
                ball.Y += ball.VelY;
            }
        }

        public ObservableCollection<DataController> GetList()
        {
            return _balls;
        }

        public int GetSize(int index)
        {
            return _balls[index].Size;
        }

        public Tuple<float, float> GetPositionXY(int index)
        {
            return new Tuple<float, float>(_balls[index].X, _balls[index].Size);
        }

        public void Clear()
        {
            _balls.Clear();
        }
    }
}
