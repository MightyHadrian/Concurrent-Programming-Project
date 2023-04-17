using System.ComponentModel;

namespace Dane
{
    public class Ball : IData
    {
        private readonly int _size;
        private readonly int _width;
        private readonly int _height;
        private float _x;
        private float _y;
        private float _velX;
        private float _velY;
        public event PropertyChangedEventHandler? PropertyChanged;

        public Ball(int size, int width, int height, float velX, float velY)
        {
            var random = new System.Random();

            _size = size;
            _width = width;
            _height = height;
            _x = random.NextSingle() * (width - size);
            _y = random.NextSingle() * (height - size);
            _velX = random.NextSingle() - 0.5f * 2 * velX;
            _velY = random.NextSingle() - 0.5f * 2 * velY;
        }

        public IData Create(int size, int width, int height, float velX, float velY)
        {
            return new Ball(size, width, height, velX, velY);
        }

        public void Move()
        {
            X += _velX;
            Y += _velY;
        }

        public int Size
        {
            get => _size;
        }

        public int Width
        {
            get => _width;
        }

        public int Height
        {
            get => _height;
        }

        public float X
        {
            get => _x;

            set 
            {
                _x = value;
                RaisePropertyChanged(nameof(X));
            }
        }

        public float Y
        {
            get => _y;

            set
            {
                _y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }

        public float VelX
        {
            get => _velX;

            set => _velX = value;
        }

        public float VelY
        {
            get => _velY;

            set => _velY = value;
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}