using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum.Telemetry
{
    [ProtoContract]
    public class Frame {
        [ProtoMember(0)] public int Num;
        [ProtoMember(1)] public int MsecFromLastFrame;
        [ProtoMember(2)] public SensorValue[] Sensors;
        [ProtoMember(3)] public ServoValue[]  Servos;
    }
}
