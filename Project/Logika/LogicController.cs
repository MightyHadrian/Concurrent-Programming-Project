using Dane;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Logika
{
    public class LogicController : INotifyPropertyChanged
    {
        private readonly ILogic _ilogic;
        public event PropertyChangedEventHandler? PropertyChanged;

        public LogicController(ILogic ilogic)
        {
            _ilogic = ilogic;
        }

        static public LogicController Create(ILogic logic)
        {
            return new LogicController(logic);
        }

        public void Start(int amount, int size, int width, int height, float velX, float velY)
        {
            _ilogic.Start(amount, size, width, height, velX, velY);
            RaisePropertyChanged(nameof(Start));
        }

        public void Restart()
        {
            _ilogic.Restart();
            RaisePropertyChanged(nameof(Restart));
        }

        public void Reset(int size, int width, int height, float velX, float velY)
        {
            _ilogic.Reset(size, width, height, velX, velY);
            RaisePropertyChanged(nameof(Reset));
        }

        public void Stop()
        {
            _ilogic.Stop();
            RaisePropertyChanged(nameof(Stop));
        }

        public void Clear()
        {
            _ilogic.Clear();
            RaisePropertyChanged(nameof(Clear));
        }

        public ObservableCollection<DataController> GetCollection()
        {
            return _ilogic.GetCollection();
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

}
