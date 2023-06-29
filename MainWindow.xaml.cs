using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace test_sqlite_04_12_06_2023
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private ObservableCollection<Model> _list = new ObservableCollection<Model>();
        CommandBinding commandBinding = new CommandBinding();
        Model Model = new Model();
       

        public MainWindow()
        {
            DataContext = new ViewModel();
            InitializeComponent();
        }
        List<Human> HumanList;

        private void ButtRemove_Click(object sender, RoutedEventArgs e)
        {
            Hu.Items.Refresh();
            Hu.DataContext = new ViewModel() { };
           //MessageBox.Show("Объектов в базе данных: "  );
            //ViewModel viewModel = new ViewModel();
            // MainWindow window = new MainWindow();
        }

        private void ButtAdd_Click(object sender, RoutedEventArgs e)
        {
            Hu.Items.Refresh();
            Hu.DataContext = new ViewModel() { };
        }

        private void ButtRefresh_Click(object sender, RoutedEventArgs e)
        {
            //ViewModel model = new ViewModel();
            Hu.DataContext = new ViewModel() { };
        }
    }
}
