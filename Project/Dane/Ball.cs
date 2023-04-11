using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Dane
{
    public class Ball : IData, INotifyPropertyChanged
    {
        private readonly int _size;
        private float _x;
        private float _y;
        private readonly float _velX;
        private readonly float _velY;
        public event PropertyChangedEventHandler? PropertyChanged;

        public Ball(int size, float x, float y, float velX, float velY)
        {
            var random = new System.Random();

            _x = random.NextSingle() * (x - size);
            _y = random.NextSingle() * (y - size);
            _size = random.Next(size - 10, size + 10);
            _velX = random.Next(-1, 1) * velX + 0.1f;
            _velY = random.Next(-1, 1) * velY + 0.1f;
        }

        public IData Create(int size, float x, float y, float velX, float velY)
        {
            return new Ball(size, x, y, velX, velY);
        }

        public int Size
        {
            get;
        }

        public float X
        {
            get => _x;

            set 
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public float Y
        {
            get => _y;

            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        public float VelX
        {
            get;
            set;
        }

        public float VelY
        {
            get;
            set;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "Ball")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}