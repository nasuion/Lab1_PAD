using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThreadServer
{
    class Utils
    {
        Queue myq = new Queue();
        command com = new command();
        Queue1 que = new Queue1();

        public void sendTOclient(Byte[] sendBytes, NetworkStream networkStream, String str, string first_Comm, TcpClient clientSocket, string Number_Client)
        {
            string comm = null;
          
                try
                {

                    if (str == "GO")
                    {
                        Console.WriteLine(" >> Command 'GO' selected at client:" + Number_Client);
                        comm = com.Welcome1;

                    }
                    else if (str == "PUT" || str == "P")
                    {

                        Console.WriteLine(" >> Command 'PUT' is selected at client:" + Number_Client);
                        string into_put = com.IntoPUT;
                        sender_to_client(sendBytes, networkStream, into_put, clientSocket);
                        //Show message to client from command.cs
                        comm = com.Next_command;
                        que.Put(myq, sendBytes, networkStream, clientSocket);
                        myq.Clear();
                    }
                    else if (str == "GET")
                    {
                        //Read queue from file
                        read_queue(sendBytes, networkStream, clientSocket);
                        Console.WriteLine(" >> Command 'GET' selected at client:" + Number_Client);
                        //Show message to client from command.cs
                        comm = com.Next_command;
                        que.Get(myq, sendBytes, networkStream, clientSocket);
                        //Clear queue to avoid duplication when execute GET
                    }

                    else if (str == "HELP")
                    {
                        Console.WriteLine(" >> Command 'HELP' selected at client:" + Number_Client);
                        que.Help(sendBytes, networkStream);
                        comm = com.Help;
                    }
                    else if (str == "CLEAR")
                    {
                        Console.WriteLine(" >> Command 'CLEAR' selected at client:" + Number_Client);
                        clear_file();
                        comm = com.Clear;
                    }
                    else if (str == "GETO")
                    {
                        read_queue(sendBytes, networkStream, clientSocket);
                        Console.WriteLine(com.Geto + "at client" + Number_Client);
                        que.get_one(myq, sendBytes, networkStream,clientSocket);
                        comm = com.Next_command;
                    }
                    else
                    {
                        Console.WriteLine(" >> " + com.Verify + Number_Client + ")");
                        comm = com.Opps;
                    }
                 
                }
                catch(Exception ex){

                        comm = com.Opps;
                }
               

            sender_to_client(sendBytes, networkStream, comm, clientSocket);
        }
//Send message from server(at orders) to client
        public static void sender_to_client(Byte[] sendBytes, NetworkStream networkStream, string into_put, TcpClient clientSocket)
        {
            command com = new command();
            sendBytes = Encoding.ASCII.GetBytes(into_put);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }
//Read each line from text file
        public void read_queue(Byte[] sendBytes, NetworkStream networkStream, TcpClient clientSocket)
        {
            
            string[] lines = System.IO.File.ReadAllLines(@"C:\\Users\\nasui\\Desktop\\file.txt");
// Display the file contents by using a foreach loop.
            try
            {
                
                foreach (string line in lines)
                {
                    String[] substrings = line.Split(new string[] { " Element " }, StringSplitOptions.None);
                    myq.Enqueue(substrings[1]);

                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(">> File null");
            }
        }
        public static void clear_file()
        {
            string path = "C:\\Users\\nasui\\Desktop\\file.txt";
            File.WriteAllText(path, String.Empty);
        }
        public void first_line()
        {
            string filePath = "C:\\Users\\nasui\\Desktop\\file.txt";
            System.IO.StreamReader fileR = new System.IO.StreamReader(filePath);
            string data = fileR.ReadToEnd();
            fileR.Close();
            string firstline = data.Substring(0, data.IndexOf('\n') + 1);
            data = Regex.Replace(data, firstline, "");
            StreamWriter file = new StreamWriter(filePath, false);
            file.Write(data);
            file.Close();
        }
        
    }
}
