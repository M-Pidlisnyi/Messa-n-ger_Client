using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    class Programm
    {
        public Programm()
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

            MainWindow MainWindow = new MainWindow();

            MainWindow.Write("Starting, creating socket\n");
            Socket sender = new Socket(AddressFamily.InterNetwork,
                                       SocketType.Stream,
                                       ProtocolType.Tcp);

            sender.Connect(ip_endpoint);
            MainWindow.Write($"Succsesfully connected to {sender.RemoteEndPoint}\n");
            MainWindow.Write("Enter message to server");

            string message_to_server = MainWindow.ReadLine();

            MainWindow.Write("Creating message");
            byte[] sending_bytes = Encoding.UTF8.GetBytes(message_to_server);
            sender.Send(sending_bytes);
            int total_recieved_bytes = sender.Receive(recieved_bytes);
            MainWindow.Write($"Message provided from server: {Encoding.UTF8.GetString(recieved_bytes)}");
            sender.Close();

        }
    }
}
