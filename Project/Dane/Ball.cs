using System.ComponentModel;

namespace Dane
{
    public class Ball : IData
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _size;
        private readonly float _mass;
        private float _x;
        private float _y;
        private float _velX;
        private float _velY;

        public Ball(int size, int width, int height, float velX, float velY)
        {
            var random = new System.Random();

            _width = width;
            _height = height;

            _size = size + random.Next(1, 11);
            _mass = _size - size;
            _x = random.NextSingle() * (width - size);
            _y = random.NextSingle() * (height - size);
            _velX = (random.NextSingle() - 0.5f) * 2 * velX;
            _velY = (random.NextSingle() - 0.5f) * 2 * velY;
        }

        public int Width
        {
            get => _width;
        }

        public int Height
        {
            get => _height;
        }

        public int Size
        {
            get => _size;
        }

        public float Mass
        {
            get => _mass;
        }

        public float X
        {
            get => _x;

            set => _x = value;
        }

        public float Y
        {
            get => _y;

            set => _y = value;
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

    }
}