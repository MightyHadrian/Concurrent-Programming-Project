namespace Dane
{
    public class Ball : IBall
    {
        private readonly int _size;
        private float _x;
        private float _y;
        private float _velX;
        private float _velY;

        public Ball() { }

        public Ball(int size, float x, float y, float velX, float velY)
        {
            _size = size;
            _x = x;
            _y = y;
            _velX = velX;
            _velY = velY;
        }

        public Ball(int size, float x, float y, float velX, float velY, float minVel)
        {
            var random = new System.Random();

            _size = random.Next(size - 10, size + 10);
            _x = random.NextSingle() * x;
            _y = random.NextSingle() * y;
            _velX = random.NextSingle() * velX + minVel;
            _velY = random.NextSingle() * velY + minVel;
        }

        ~Ball() { }

        public int Size
        {
            get;
        }

        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
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
    }
}