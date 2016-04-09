using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace Ursa.Cerebellum.Maestro24Controller
{
    public class MaestroServoChannel: ServoChannelBase, IMaestroChannel
    {
        public MaestroServoChannel(byte channelNum) : base(channelNum) { }
        public override void WriteSpeed(ushort speed) {
            if (Device != null)
                Device.setTarget(this.Num, speed);
        }

        public override void WriteAcсeleration(ushort acceleration) {
            if (Device != null)
                Device.setTarget(this.Num, acceleration);
        }

        public override void WriteTarget(ushort target) {
            if (Device != null)
                Device.setTarget(this.Num, target);
        }

        public Usc Device { get;  set; }

        ServoStatus channelStatus;
        public new ServoStatus DeviceChannelStatus {
            get {
                return channelStatus;
            }
            set {
                channelStatus = value;
                Status = new ServoValue {
                    Acceleration = value.acceleration,
                    Actual = value.position,
                    Target = value.target,
                    Speed = value.speed,
                };
            }
        }
    }
}
