using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace Ursa.Cerebellum.Maestro24Controller
{
    public class MaestroSensorChannel: ChannelBase, ISensorChannel, IMaestroChannel
    {
        public MaestroSensorChannel( byte num)
            : base(num)
        {
        }
        public Pololu.Usc.Usc Device { get; set; }
        
        ServoStatus deviceChannelStatus;
        public Pololu.Usc.ServoStatus DeviceChannelStatus
        {
            get { return deviceChannelStatus; }
            set {
                deviceChannelStatus = value;
                RaiseUpdated(value.position);    
            }
        }
    }
}
