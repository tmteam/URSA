using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    public abstract class ServoChannelBase: ChannelBase, IServoChannel
    {
        public ServoChannelBase(byte channelNum) : base(channelNum) { }
      

        public float DegreesAtMin { get; set; }

        public float DegreesAtMax { get; set; }

        ServoValue status;
        public ServoValue Status 
        { 
            get { return status; }
            set {
                status = value;
                RaiseUpdated(status.Actual);
            } 
        }

        public abstract void WriteSpeed(ushort speed);

        public abstract void WriteAcсeleration(ushort Acceleration);

        public abstract void WriteTarget(ushort target);

    }
}
