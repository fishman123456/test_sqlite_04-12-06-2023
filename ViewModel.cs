using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace test_sqlite_04_12_06_2023
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private Human selectedHuman; // объект человек, что сейчас активный (выбранный)
        Model model; // объект модели


        public ViewModel() // конструктор
        {
            this.model = new Model();
        }
        public List<Human> HumanList
        {
            get { return this.model.HumansList(); }

         
    }
        private Command addCommand;

        public Command AddCommand => addCommand ??
                  (addCommand = new Command(obj =>
                  {
                      Human NewHuman = new Human();
                      model.AddHuman(NewHuman);
                      selectedHuman = NewHuman;
                      OnPropertyChanged("Humans");
                  }));
        // public event PropertyChangedEventHandler? PropertyChanged;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
   
}
