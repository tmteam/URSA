using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    public class ChannelBase: IChannel
    {
        public ChannelBase(byte channelNum) {
            this.Num = channelNum;
        }

        public byte Num { get; private set;}
        public ushort Min { get; set; }
        public ushort Max { get; set; }
        
        public ushort Actual { get; protected set; }

        protected void RaiseUpdated(ushort value) {
            this.Actual = value;
            LastUpdated = DateTime.Now;
            if (ChannelUpdated != null)
                ChannelUpdated(this, LastUpdated);
        }
        public DateTime LastUpdated { get; private set; }

        public event Action<IChannel, DateTime> ChannelUpdated;
    }
}
