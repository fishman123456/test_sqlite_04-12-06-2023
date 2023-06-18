using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace test_sqlite_04_12_06_2023
{
    public class Human : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; } = "NameConstr  ";
        public string Surname { get; set; } = "SurnameConstr  ";

       public Human() { }
        public string NameBind
        {
            get { return Name; }
            set
            {
                Name = value;
                OnPropertyChanged("NameBind");// триггеим событие - вызываем обновление во всех view событие
            }
        }
        public string SurnameBind
        {
            get { return Surname; }
            set
            {
                Surname = value;
                OnPropertyChanged("SurnameBind");// триггеим событие - вызываем обновление во всех view событие
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class ApplicationContext : DbContext// выполняет функцию соединения с бд
    {
        public DbSet<Human> Humans => Set<Human>();// непосредственно коллекция объектов. Именно она будет храниться в базе данных

        public ApplicationContext() => Database.EnsureCreated();// при создании этого объекта удостоверяяемся, что база данных существует и создаем, если нет
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)// говорим чем и куда сохранять наши данные
        {
            optionsBuilder.UseSqlite("Data Source=Humans.db");
        }
    }
    class Model : INotifyPropertyChanged
    {
        private ApplicationContext db;// контекст для работы с бд. Коллекция собак в нем

        public void AssembleNewHuman() // стандартный набор собак
        {
            AddHuman(new Human { Name = "Петр ", Surname = "Петров " });
            AddHuman(new Human());
        }

        public List<Human> HumansList() // метод возвращает текущую коллекцию
        {
            return this.db.Humans.ToList<Human>();
        }
        // сравниваем значение в текстбоксе
        public void AddHuman(Human human) // добавить
        {
            this.db.Humans.Add(human);
            HumanSync();
        }

        public void RemoveHuman(Human human) // Удалить
        {
            this.db.Humans.Remove(human);
            HumanSync();
        }

        public void HumanSync() // Сохраняем изменения в бд
        {
            this.db.SaveChanges();
            //MessageBox.Show("Объекты сохранены");
            OnPropertyChanged("HumanSync");// уведомляем отображение о изменении 
        }


        public Model()
        {

            this.db = new ApplicationContext();// создаем контекст (сессию для работы с бд)
           // this.AssembleNewHuman();
           //MessageBox.Show ("Объектов в базе данных: " + this.db.Humans.ToList<Human>().Count.ToString());
        }


        public event PropertyChangedEventHandler ?PropertyChanged;// событие для уведомления вида пользователя
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //internal void RemoveHuman(object selectedHuman)
        //{
        //    throw new NotImplementedException();
        //}
}
}
