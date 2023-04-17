using System;
using System.ComponentModel;
using System.Windows.Input;
using PrezentacjaModel;
using System.Collections;

namespace PrezentacjaViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly Model _model;
        private ICommand? _startButton;
        private ICommand? _resetButton;
        private ICommand? _stopButton;
        private bool isStopped;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ViewModel() 
        {
            _model = Model.Create();
            isStopped = false;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IList Objects
        {
            get => _model.GetCollection();
        }

        public int ObjectsAmount
        {
            get => _model.GetNewObjectsAmount();

            set
            {
                _model.SetNewObjectsAmount(value);
                RaisePropertyChanged(nameof(ObjectsAmount));
            }
        }

        public ICommand StartClickCommand
        {
            get
            {
                RaisePropertyChanged(nameof(_startButton));
                return _startButton ??= new ButtonCommand(() => StartAction(), () => CanExecute);
            }
        }

        public ICommand ResetClickCommand
        {
            get
            {
                RaisePropertyChanged(nameof(_resetButton));
                return _resetButton ??= new ButtonCommand(() => ResetAction(), () => CanExecute);
            }
        }

        public ICommand StopClickCommand
        {
            get
            {
                RaisePropertyChanged(nameof(_stopButton));
                return _stopButton ??= new ButtonCommand(() => StopAction(), () => CanExecute);
            }
        }

        public void StartAction()
        {
            if (isStopped)
            {
                _model.Restart();
                isStopped = false;
            } else
            {
                _model.Start(25, 770, 500, 3, 3);
            }
        }

        public void ResetAction()
        {
            _model.Reset(25, 770, 500, 3, 3);
        }

        public void StopAction()
        {
            isStopped = true;
            _model.Stop();
        }

        public static bool CanExecute
        {
            get
            {
                return true;
            }
        }

    }

    public class ButtonCommand : ICommand
    {

        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="action">Action to be executed by the command</param>
        /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
        public ButtonCommand(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Wires CanExecuteChanged event 
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Forcess checking if execute is allowed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object? parameter)
        {
            _action();
        }

    }

}
