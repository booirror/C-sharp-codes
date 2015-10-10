using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace csLesson
{
    class Server
    {
        static void start()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(ip, port));
            socket.Listen(8);
            Console.WriteLine("start server");
            Thread connect = new Thread(ClientConnect);
            connect.Start();
        }

        private static void ClientConnect()
        {
            while (true)
            {
                Socket client = socket.Accept();
                client.Send(Encoding.ASCII.GetBytes("welcome"));
                Thread receiveThread = new Thread(ReceiveMsg);
                receiveThread.Start(client);
            }
        }

        private static void ReceiveMsg(object client)
        {
            Socket clientsock = (Socket)client;
            while (true)
            {
                try
                {
                    int receiveNum = clientsock.Receive(result);
                    Console.WriteLine("receive {0} : {1}", 
                        clientsock.RemoteEndPoint.ToString(),
                        Encoding.ASCII.GetString(result, 0, receiveNum));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    clientsock.Shutdown(SocketShutdown.Both);
                    clientsock.Close();
                    break;
                }
            }
        }


        static Socket socket;
        private static byte[] result = new byte[1024];
        private static int port = 8888;
    }
}
