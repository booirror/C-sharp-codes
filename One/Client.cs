using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace csLesson
{
    class Client
    {
        private static byte[] result = new byte[10240];
        public static void start()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Socket client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(new IPEndPoint(ip, 8888));
                Console.WriteLine("connect success");
            }catch
            {
                Console.WriteLine("connect failure");
                return;
            }
            int receiveLen = client.Receive(result);
            Console.WriteLine("msg:{0}", Encoding.ASCII.GetString(result, 0, receiveLen)); ;
            try
            {
                Thread.Sleep(1000);
                string msg = "client msg:hello";
                client.Send(Encoding.ASCII.GetBytes(msg));

            } catch
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                
            }
            Console.WriteLine("over");
        }
        
    }
}
