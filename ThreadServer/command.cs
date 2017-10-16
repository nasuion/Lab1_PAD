using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThreadServer
{
    class command
    {
        private string connect = "You are connected!!!\n";

        public string Connect
        {
            get { return connect; } set { connect = value; }
        }
        private string welcome = "Welcome is Vanea's Broker\nYou can execute ('PUT','GET','CLEAR','HELP')";

        public string Welcome1
        {
            get { return welcome; } set { welcome = value; }
        }
        
        private string get = "Your command is get";

        public string Get
        {
            get { return get; } set { get = value; }
        }
        private string Put = "Your Command is put";

        public string Put1
        {
            get { return Put; }set { Put = value; }
        }

        private string intoPUT = "Enter Your Text for queue (for exit enter STOP)";

        public string IntoPUT
        {
            get { return intoPUT; } set { intoPUT = value; }
        }
        private string next_command = "Select next command ('PUT','GET','CLEAR','HELP')";

        public string Next_command
        {
            get { return next_command; } set { next_command = value; }
        }
        private string help = "This is help!! \n\nSelect next command ('PUT','GET','CLEAR','HELP')";

        public string Help
        {
            get { return help; }
            set { help = value; }
        }
        private string verify = "Verify your command (error at client :";

        public string Verify
        {
            get { return verify; }
            set { verify = value; }
        }
        private string opps = "Ooooopssss error , try command again!!";

        public string Opps
        {
            get { return opps; }
            set { opps = value; }
        }
        private string clear = "backup successfully deleted!!!!";

        public string Clear
        {
            get { return clear; }
            set { clear = value; }
        }
        /*private string name = "You are connected!!!";*/
        /*private string name = "You are connected!!!";*/

  
        
     
     
      
    }
}
