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
using System.Windows.Input;

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
            get { return selectedHuman; }
            set
            {
                MessageBoxResult result;

                result = MessageBox.Show("Удалить обьект?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    this.selectedHuman = value;
                    //HumanList.Remove(value);
                    this.model.RemoveHuman(value);
                    this.model.HumanSync();
                    MessageBox.Show("Обьект удалён!");
                }

                else
                {
                    MessageBox.Show("Обьект не удалён!");
                }

                OnPropertyChanged("SelectedHuman");
            }
        }


        private void removeHuman()
        {
           model.RemoveHuman(this.selectedHuman);
            MessageBox.Show("Hi... " + MessageText, "Message", MessageBoxButton.OK);
        }
        private ICommand removeHumandCommand;
        public ICommand RemoveHumandCommand
        {
           
            get
            {
                if (removeHumandCommand == null)
                    removeHumandCommand = new RelayCommand(p => removeHuman());
                //HumanList.Remove(value);
                this.model.RemoveHuman(selectedHuman);
                this.model.HumanSync();
                MessageBox.Show("Обьект удалён!");
                return removeHumandCommand;
            }
        }
        private void addHuman()
        {
            // ViewModel view = new ViewModel();
            model.AssembleNewHuman();
        }
        private ICommand addHumandCommand;
        public ICommand AddHumandCommand
        {
            get
            {
                if (addHumandCommand == null)
                    addHumandCommand = new RelayCommand(p => addHuman());

                //Hu.Items.Refresh();
                //Hu.DataContext = new ViewModel(););
                return addHumandCommand;
            }
            set
            {
                this.model.HumanSync();
            }
        }
        private string messageText;
        public string MessageText
        {
            get
            {
                return messageText;
            }
            set
            {
                messageText = value;
                OnPropertyChanged("MessageText");
            }
        }

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


