using System.ComponentModel;

namespace Dane
{
    public class DataController : INotifyPropertyChanged
    {
        private readonly IData _idata;
        public event PropertyChangedEventHandler? PropertyChanged;

        public DataController(IData idata)
        {
            _idata = idata;
        }

        public static DataController Create(IData data)
        {
            return new DataController(data);
        }

        public int Width
        {
            get => _idata.Width;
        }

        public int Height
        {
            get => _idata.Height;
        }

        public int Size
        {
            get => _idata.Size;
        }

        public float Mass
        {
            get => _idata.Mass;
        }

        public float X
        {
            get => _idata.X;

            set
            {
                _idata.X = value;
                RaisePropertyChanged(nameof(X));
            }
        }

        public float Y
        {
            get => _idata.Y;

            set
            {
                _idata.Y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }

        public float VelX
        {
            get => _idata.VelX;

            set
            {
                _idata.VelX = value;
                RaisePropertyChanged(nameof(VelX));
            }
        }

        public float VelY
        {
            get => _idata.VelY;

            set
            {
                _idata.VelY = value;
                RaisePropertyChanged(nameof(VelY));
            }
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

}
