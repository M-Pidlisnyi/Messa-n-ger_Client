using System;
using System.Collections.Generic;
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

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Programm p = new Programm();
            
        }

        public  void Write(string input)
        {
            Output.Text = input;
        }

        public  string ReadLine()
        {
            return Input.Text;
        }

    }  

    

}
