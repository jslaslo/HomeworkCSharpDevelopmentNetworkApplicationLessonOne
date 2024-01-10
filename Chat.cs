using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TaskOne
{
    internal class Chat
    {       
        public static void Server()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            UdpClient udpClient = new UdpClient(12345);            
            Console.WriteLine("Сервер ожидает сообщение от клиента...");

            while (true)
            {
                try
                {
                    byte[] buffer = udpClient.Receive(ref localEndPoint);
                    string str = Encoding.UTF8.GetString(buffer);

                    Message? newMessage = Message.FromJson(str);
                    if (newMessage != null)
                    {
                        Console.WriteLine(newMessage.ToString());
                        Message message = new Message("server", "Сообщение получено");

                        string json = message.GetJsonString();

                        byte[] bytes = Encoding.UTF8.GetBytes(json);

                        udpClient.Send(bytes, localEndPoint);
                    }
                    else
                    {
                        Console.WriteLine("Сообщение некорректно!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void Client(string nickname)
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
            UdpClient udpClient = new UdpClient();

            while (true)
            {
                Console.WriteLine("Введите сообщение");
                string? text = Console.ReadLine();
                if (String.IsNullOrEmpty(text))
                {
                    break;
                }
                Message message = new Message(nickname, text);
                string json = message.GetJsonString();
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                udpClient.Send(bytes, localEndPoint);

                byte[] buffer = udpClient.Receive(ref localEndPoint);
                string str = Encoding.UTF8.GetString(buffer);
                Message? newMessage = Message.FromJson(str);
                Console.WriteLine(newMessage);
            }
        }
    }
}
