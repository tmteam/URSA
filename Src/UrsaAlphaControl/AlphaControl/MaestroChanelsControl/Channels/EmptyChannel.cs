using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    public class EmptyChannel: IChannel
    {
        public EmptyChannel(byte channelNum)
        {
            Num = channelNum;
            LastUpdated = DateTime.Now;
        }
        public byte Num
        {
            get;
            protected set;
        }

        public ushort Min
        {
            get { return 0; }
        }

        public ushort Max
        {
            get{return 0;}
        }

        public ushort Actual
        {
            get { return 0; }
        }

        public DateTime LastUpdated
        {
            get;
            protected set;
        }

        public event Action<IChannel, DateTime> ChannelUpdated;
    }
}
