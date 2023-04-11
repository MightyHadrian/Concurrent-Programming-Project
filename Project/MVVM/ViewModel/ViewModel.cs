using Dane;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrezentacjaModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Xml.Linq;
using System.Collections;

namespace PrezentacjaViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly Model _model;
        private ICommand? _startButton;
        private ICommand? _resetButton;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ViewModel()
        {
            _model = Model.Create();
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

        public void StartAction()
        {
            _model.Start(25, 780, 500, 1, 1);
        }

        public void ResetAction()
        {
            _model.Reset(25, 780, 500, 1, 1);
        }

        public bool CanExecute
        {
            get
            {
                return true;
            }
        }

    }

    public class ButtonCommand : ICommand
    {

        private Action _action;
        private Func<bool> _canExecute;

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
