using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    public class SensorChannel: ChannelBase, ISensorChannel
    {
        public SensorChannel(byte channelNum) : base(channelNum) { }
        public void UpdateValue(ushort value) {
            RaiseUpdated(value);
        }
    }
}
