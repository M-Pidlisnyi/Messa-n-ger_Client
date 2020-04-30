using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPHostEntry ip_host;
        private IPAddress ip_address;
        private IPEndPoint ip_endpoint;

        private Socket sender_socket = new Socket(AddressFamily.InterNetwork,
                                       SocketType.Stream,
                                       ProtocolType.Tcp);

        private byte[] recieved_bytes = new byte[2048];

        public MainWindow()
        {
            InitializeComponent();

            Connect();    
        }

        private void Connect()
        {
            ip_host = Dns.GetHostEntry(new IPAddress(new byte[] { 192, 168, 1, 103 }));
            ip_address = default;

            foreach (var current_ip in ip_host.AddressList)
            {
                if (current_ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip_address = current_ip;
                }
            }

            ip_endpoint = new IPEndPoint(ip_address, 2000);

            sender_socket.Connect(ip_endpoint);
            WriteDebug($"Succesfully connected to {sender_socket.RemoteEndPoint}\n");
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            WriteDebug("Enter message to server");
            string message_to_server = ReadLine();

            WriteDebug("Creating message");
            byte[] sending_bytes = Encoding.UTF8.GetBytes(message_to_server);
            sender_socket.Send(sending_bytes);

            
        }

        private void RefreshMessages(object sender, RoutedEventArgs e)
        {
            int total_recieved_bytes = sender_socket.Receive(recieved_bytes);
            WriteDebug($"Message provided from server: {Encoding.UTF8.GetString(recieved_bytes)}");
            Write(Encoding.UTF8.GetString(recieved_bytes));
        }

        private void CloseClient(object sender, RoutedEventArgs e)
        {
            sender_socket.Close();
            Close();
        }

        private void Write(string input)
        {
            Output.Text += input;
        }

        private void WriteDebug(string input)
        {
            DebugInfo.Text += input;
        }

        private string ReadLine()
        {
            return Input.Text;
        }

        
    }  

    

}
