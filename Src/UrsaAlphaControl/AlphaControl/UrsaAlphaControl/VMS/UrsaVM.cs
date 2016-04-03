using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrsaAlphaControl.Commands;

namespace UrsaAlphaControl.VMS
{
    public class UrsaVM: VMBase
    {
        public Body Body { get; protected set; }
        public UrsaVM(Body body) {
            isConnected = false;
            Legs = new FourLegsVM(body);
            Connect = new CommandBase();
            Disconnect = new CommandBase();
            Disconnect.SetCanExecute(false);
            Body = body;
        }    

        public bool IsConnected { get { return isConnected; } 
            set { 
                isConnected = value; 
                Raise("IsConnected");
                Connect.SetCanExecute(!isConnected);
                Disconnect.SetCanExecute(isConnected);
            } }
        bool isConnected;


        public CommandBase Connect { get; set; }
        public CommandBase Disconnect { get; set; }


        public FourLegsVM Legs { get; set; }

      
    }
}
