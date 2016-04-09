using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Body;
using UrsaAlphaControl.Commands;

namespace UrsaAlphaControl.VMS
{
    public class BodyVM: VMBase
    {
        public Body Body { get; protected set; }
        public BodyVM(Body body) {
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
