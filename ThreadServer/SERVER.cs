using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadServer
{
    class SERVER
    {

        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");

            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> Client No:" + Convert.ToString(counter) + " started!");
                ThreadClient client = new ThreadClient();
                client.startClient(clientSocket, Convert.ToString(counter));
            }

           
        }
    }

    //Each client connect to server
    public class ThreadClient
    {
        Utils util = new Utils();
        public static string first_Comm = null;

        TcpClient clientSocket;
        TcpListener serverSocket;
        string clNo;
        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(Execute);
            ctThread.Start();
        }
        private void Execute()
        {
            byte[] bytesFrom;
            Byte[] sendBytes = null;

            while ((true))
            {
                try
                {
                    NetworkStream networkStream = clientSocket.GetStream();
                    bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                    int w = networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);

                    string str = Encoding.ASCII.GetString(bytesFrom, 0, w);
                    
                    //Send 3 param to sendTOclient from Utils
                    util.sendTOclient(sendBytes, networkStream, str, first_Comm, clientSocket, clNo);
                    //util.read_queue();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }
    } 
}
