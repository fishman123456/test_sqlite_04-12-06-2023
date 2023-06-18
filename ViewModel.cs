using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace test_sqlite_04_12_06_2023
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private Human selectedHuman; // объект человек, что сейчас активный (выбранный)
        Model model = new Model(); // объект модели


        public ViewModel() // конструктор
        {
            this.model = new Model();
        }
        public List<Human> HumanList
        {
            get { return this.model.HumansList(); }

        }
        // 13-06-2023 Мы научились удалять из списка по щелчку
        public Human SelectedHuman
        {
            get { return  selectedHuman; }
            set
            {
                this.selectedHuman = value;
                //HumanList.Remove(value);
                this.model.RemoveHuman(value);
                this.model.HumanSync();
                OnPropertyChanged("SelectedHuman");
            }
        }
        //private Command addcommand;

        //public Command Addcommand => addcommand ??
        //          (addcommand = new Command(obj =>
        //          {
        //              Human newhuman = new Human();
        //              model.AddHuman(newhuman);
        //              SelectedHuman = newhuman;
        //              OnPropertyChanged("humanlist");
        //              this.model.HumanSync();
        //          }));
        private Command removeCommand;

        public Command RemoveCommand => removeCommand ?? (removeCommand = new Command(obj =>
                                                     {
                                                         if (obj is Human oldhuman)
                                                         {
                                                             model.RemoveHuman(oldhuman);
                                                             OnPropertyChanged("HumanList");
                                                             MessageBox.Show("сработала команда RemoveCommand");
                                                         }
                                                     }
                ));


        //get
        //{
        //    return removeCommand ??
        //      (removeCommand = new Command(obj =>
        //      {
        //          if (obj is Human oldHuman)
        //          {
        //              model.RemoveHuman(oldHuman);
        //              OnPropertyChanged("HumanList");

        //          }
        //      },
        //     (obj) => this.HumanList.Count > 0));
        //}
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}


