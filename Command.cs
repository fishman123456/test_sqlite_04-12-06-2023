using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace test_sqlite_04_12_06_2023
{
    internal class Command
    {
        private Action<object> value;
        private Func<object, bool> value1;

        public Command(Action<object> value)
        {
            this.value = value;
        }
        internal sealed class Commandof : ICommand
        {

            private Action<object> execute;
            private Func<object, bool> canExecute;
            // команда добавление / удаление обьекта
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
            // проверка на изменение обьекта, существует он или нет
            public Commandof(Action<object> execute, Func<object, bool> canExecute = null)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return this.canExecute == null || this.canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                this.execute(parameter);
            }
        }
    }
}
