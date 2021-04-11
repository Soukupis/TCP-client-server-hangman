using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace TCP_client
{
    class TcpClientService
    {
        public static void Connect(String server, String message)
        {
            try
            {
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                Byte[] bytes = new Byte[256];
                int i;
                stream.Write(System.Text.Encoding.ASCII.GetBytes(message));
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    Console.Clear();
                    string responseData = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine(responseData);
                    if(responseData == "You won!" || responseData == "You lost!")
                    {
                        Console.WriteLine("\nHit enter to continue...");
                        Console.Read();
                    }
                    else
                    {
                        Console.Write("Choose a char: ");
                        string choice = Console.ReadLine();
                        Byte[] choiceBytes = System.Text.Encoding.ASCII.GetBytes(choice);
                        stream.Write(choiceBytes);
                    }
                }
                stream.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNUllException: {0}", e);
            }catch(SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
