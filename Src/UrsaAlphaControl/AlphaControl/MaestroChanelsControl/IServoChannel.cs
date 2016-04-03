using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    public interface IServoChannel : IChannel {
        ushort Min { get; }
        ushort Max { get; }
        float DegreesAtMin { get; }
        float DegreesAtMax { get; }
        ServoStatus Status { get; }
        void WriteSpeed(ushort speed);
        void WriteAcсeleration(ushort Acceleration);
        void WriteTarget(ushort target);
    }
}
