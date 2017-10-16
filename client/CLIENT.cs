using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class CLIENT
    {
        static void Main(string[] args)
        {
           

            try
            {
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");
                tcpclnt.Connect("localhost", 8888);
                // use the ipaddress as in the server program
                Console.WriteLine("Connected");
                Console.WriteLine("For to continue please enter GO");
                while ((true))
                {
                    String str = Console.ReadLine();



                     NetworkStream networkStream = tcpclnt.GetStream();
                        Byte[] sendBytes = Encoding.ASCII.GetBytes(str);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();


                        byte[] bytesFrom = new byte[tcpclnt.ReceiveBufferSize];
                        int w = networkStream.Read(bytesFrom, 0, (int)tcpclnt.ReceiveBufferSize);
                        string str1 = Encoding.ASCII.GetString(bytesFrom, 0, w);
                        Console.WriteLine(str1);
                }

                //tcpclnt.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
            
        }
    }
}
