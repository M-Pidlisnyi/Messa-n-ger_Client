using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
            RunServer();
        }

        public  void Write(string input)
        {
            Output.Text = input;
        }

        public  string ReadLine()
        {
            return Input.Text;
        }
       
        private void RunServer()
        {
            byte[] recieved_bytes = new byte[2048];

            var ip_host = Dns.GetHostEntry(new IPAddress(new byte[] { 192, 168, 1, 103 }));
            IPAddress ip_address = default;

            foreach (var current_ip in ip_host.AddressList)
            {
                if (current_ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip_address = current_ip;
                }
            }

            var ip_endpoint = new IPEndPoint(ip_address, 2000);

            Write("Starting, creating socket\n");
            Socket sender = new Socket(AddressFamily.InterNetwork,
                                       SocketType.Stream,
                                       ProtocolType.Tcp);

            sender.Connect(ip_endpoint);
            Write($"Succsesfully connected to {sender.RemoteEndPoint}\n");

            Write("Enter message to server");
            string message_to_server = ReadLine();

            Write("Creating message");
            byte[] sending_bytes = Encoding.UTF8.GetBytes(message_to_server);
            sender.Send(sending_bytes);
            int total_recieved_bytes = sender.Receive(recieved_bytes);
            Write($"Message provided from server: {Encoding.UTF8.GetString(recieved_bytes)}");
            sender.Close();
        }

    }  

    

}
