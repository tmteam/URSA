using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace Ursa.Cerbellum.Maestro24Controller
{
    public class MaestroEmptyChannel: ChannelBase, IMaestroChannel
    {
        public MaestroEmptyChannel(Usc device, byte num) : base(num) {
            this.Device = device;
        }
        public Pololu.Usc.Usc Device { get; protected set; }

        public Pololu.Usc.ServoStatus DeviceChannelStatus { get; set;}
    }
}
