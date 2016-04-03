using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
namespace UrsaAlphaControl.Commands
{
    public class CommandBase: ICommand
    {
        bool canExecute = true;
        public void SetCanExecute(bool value)
        {
            canExecute = value;
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
        public bool CanExecute(object parameter) {
            return canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter) {
            if (canExecute && Executed != null)
                Executed(this, parameter);
        }
        public event Action<ICommand, object> Executed;
    }
}
