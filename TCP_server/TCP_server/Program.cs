using System;
using System.Net;
using System.Net.Sockets;

namespace TCP_server
{
    class MyTcpListener
    {
        static void Main()
        {
            TcpListener server = null;
            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddr, port);

            string[] words = new string[] { "subway", "walkway", "fluffiness", "dizzying", "cockiness" };
            string[] hangman = new string[]
            {
                "\n +---+\n |   |\n     |\n     |\n     |\n     |\n =========\n",
                "\n +---+\n |   |\n O   |\n     |\n     |\n     |\n =========\n",
                "\n +---+\n |   |\n O   |\n |   |\n     |\n     |\n =========\n",
                "\n  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n =========\n",
                "\n  +---+\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n =========\n",
                "\n  +---+\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n =========\n",
                "\n  +---+\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n =========\n",
            };

            bool win;
            Random rnd = new Random();
            string word = words[rnd.Next(5)];
            
            char[] choosenChars = new char[word.Length];
            string result;
            try
            {
                server.Start();
                Byte[] bytes = new Byte[256];
                Console.Write("Waiting for a connection...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");
                NetworkStream stream = client.GetStream();
                int i;
                Console.WriteLine(word);
                int counter = 0;
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    Byte[] message = new Byte[256];
                    string gaps = "";
                    result = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    bool contains = false;
                    if (result != "start")
                    {
                        for (i = 0; i < word.Length; i++)
                        {
                            if (result.Length > 1 || result.Length < 1)
                            {
                                continue;
                            }
                            else
                            {
                                if (word[i] == Convert.ToChar(result))
                                {
                                    choosenChars[i] = word[i];
                                    contains = true;
                                }
                            }
                        }
                    }
                    if (!contains)
                    {
                        counter++;
                    }
                    for (int j = 0; j < choosenChars.Length; j++)
                    {
                        if (choosenChars[j] != '\x0000')
                        {
                            gaps += choosenChars[j] + " ";
                        }
                        else
                        {
                            gaps += "_ ";
                        }
                    }
                    
                    win = true;
                    foreach (char k in choosenChars)
                    {
                        if (k == '\x0000')
                        {
                            win = false;
                            
                        }
                    }
                    if (win)
                    {
                        message = System.Text.Encoding.ASCII.GetBytes("You won!");
                    }
                    else
                    {
                        if(counter >= 6)
                        {
                            message = System.Text.Encoding.ASCII.GetBytes("You lost!" + hangman[counter]);
                            client.Close();
                            client.Close();
                            Console.WriteLine("\nHit enter to continue...");
                            Console.Read();
                        }
                        else
                        {
                            message = System.Text.Encoding.ASCII.GetBytes(gaps + hangman[counter]);
                        }
                    }
                    stream.Write(message);
                    Console.WriteLine("Received: {0}", result);
                }
                client.Close();
            }
            catch(SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
            
        }
    }
}
