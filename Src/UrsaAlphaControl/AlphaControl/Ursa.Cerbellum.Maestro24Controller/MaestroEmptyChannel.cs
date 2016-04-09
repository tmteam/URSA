using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace Ursa.Cerebellum.Maestro24Controller
{
    public class MaestroEmptyChannel: ChannelBase, IMaestroChannel
    {
        public MaestroEmptyChannel(byte num) : base(num) {
        }
        public Pololu.Usc.Usc Device { get;  set; }

        public Pololu.Usc.ServoStatus DeviceChannelStatus { get; set;}
    }
}
