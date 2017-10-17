using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThreadServer
{ 
   

    class Queue1
    {

        public string object_m = null;
        SERVER pro = new SERVER();
       static string QUeue = null;
       static string fileFullPath = "C:\\Users\\nasui\\Desktop\\file.txt";
// *********PUT*********
        public void Put(Queue myq, Byte[] sendBytes, NetworkStream networkStream, TcpClient clientSocket)
        {
            while ((true))
            {
                sendBytes = new byte[clientSocket.ReceiveBufferSize];
                int w = networkStream.Read(sendBytes, 0, (int)clientSocket.ReceiveBufferSize);
                string str = Encoding.ASCII.GetString(sendBytes, 0, w);

                
                
                if (str == "STOP")
                {
                    break;
                    Console.WriteLine(" >> Back to command");
                }
                else
                {
                    myq.Enqueue(str);
                    Object objectQueue = myq.Count;
                    Console.WriteLine(" >> Enqueued '" + objectQueue.ToString() + "' element");
                }

                string next = "Enter next string or enter 'STOP' for exit";
                to_client(sendBytes, networkStream, next);
                to_PUt(myq, sendBytes, networkStream);
                SaveStreamToFile(fileFullPath, QUeue);

                
            }
          
        }
//************GET***********
        public void Get(Queue myq, Byte[] sendBytes, NetworkStream networkStream, TcpClient clientSocket)
        {
                Object objectQueue = myq.Count;
                Console.WriteLine(" >> "+objectQueue.ToString());
                string m = objectQueue.ToString();
                int t = Int32.Parse(m);
                if (t == 0)
                {
                    
                    object_m = "In Queue is not message please select 'STOP'";
  //Send message to client
                        to_client(sendBytes, networkStream, object_m);
                }
                else
                {
//GET all elements from queue and stokee in LIST
                    GET_EXT(myq, sendBytes, networkStream);
                    //tes1();

                }
            
                sendBytes = new byte[clientSocket.ReceiveBufferSize];
                int w = networkStream.Read(sendBytes, 0, (int)clientSocket.ReceiveBufferSize);
                string get_que = Encoding.ASCII.GetString(sendBytes, 0, w);

            //to_client(sendBytes, networkStream, get_que)

           
        }
        


//*******************HELP***************
        public void Help(Byte[] sendBytes, NetworkStream networkStream)
        {
            int i = 0;
            List<string> list = new List<string>() { "Press Enter for show!!!", "**PUT** Add text in queue\n\n", "**GET** View all elements from queue\n\n", "**STOP** For Back to select another function \n\n","**CLEAR** Delete all elements from queue\n\n","**GETO** Consume each message from queue"};
            while (i <= 4)
            {
                string object_m= null;
                object_m = list[i];
                to_client(sendBytes, networkStream, object_m);
                i++;
            }

        }


        public void get_one(Queue myq, Byte[] sendBytes, NetworkStream networkStream, TcpClient clientSocket)
        {
            Utils util = new Utils();
            //util.read_queue(sendBytes,networkStream,clientSocket);
            Object o =  myq.Dequeue();
            object_m = "Your first element dequeued '"+o.ToString()+"'";
            to_client(sendBytes, networkStream, object_m);
            util.first_line();
            
           


        }
        public static void GET_EXT(IEnumerable myCollection, Byte[] sendBytes, NetworkStream networkStream)
        {
                List<string> queList = new List<string>() { "PressEnter" };
                int i = 0; 
                    while (i == 0)
                    {
                        string q = queList[i];
                        to_client(sendBytes, networkStream, q);
                        i++;
                    }
                    

                    foreach (Object obj in myCollection)
                    {
                        string w = "From queue Element " + obj.ToString() + "\n";
                        to_client(sendBytes, networkStream, w);
                    }

                    string object_m = "PRESS 'STOP' for EXIT";
                    to_client(sendBytes, networkStream, object_m);
            
        }

        public static void to_PUt(IEnumerable myCollection, Byte[] sendBytes, NetworkStream networkStream)
        {
            List<string> queueD = new List<string>();
      
            foreach (Object obj in myCollection)
            {
                string y = "From queue Element " + obj.ToString();
                queueD.Add(y);
            }
            int e;
            for (e = 0; e < queueD.Count; e++)
            {
                QUeue = queueD[e];
                // SAVE TO FILE
            }

            //to_client(sendBytes, networkStream, object_m);
        }
//Save queue to file
        public static void SaveStreamToFile(string fileFullPath, string QUeue)
        {
            using (FileStream f = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write))
            using (StreamWriter s = new StreamWriter(f))
                s.WriteLine(QUeue);
        }
//All message sends with this function
        public static void to_client(Byte[] sendBytes, NetworkStream networkStream, string object_m)
        {
            sendBytes = Encoding.ASCII.GetBytes(object_m);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }
   
    }
}
