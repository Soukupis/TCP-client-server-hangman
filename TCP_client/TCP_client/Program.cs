using System;
using System.Net;
using System.Net.Sockets;

namespace TCP_client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress server = IPAddress.Parse("127.0.0.1");
            Console.Write("Do you want to start a new game? [y/n]: ");
            string main = Console.ReadLine();
            if(main == "y")
            {
                TcpClientService.Connect(server.ToString(), "start");
            }else if(main == "n"){
                TcpClientService.Connect(server.ToString(), "end");
                Console.ReadKey();
            }
            
            
            
            /*
            for(int i = 0; i < 10; i++)
            {
                string number = Console.ReadLine();
                try
                {
                    Convert.ToInt32(number);
                }
                catch
                {
                    Console.WriteLine("Cannot convert number");
                }

                try
                {
                    TcpClientService.Connect("127.0.0.1", number.ToString());
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
            }
            */
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
            
        }
    }
}
