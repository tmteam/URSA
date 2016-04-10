using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    [ProtoContract]
    public struct ServoValue {
        [ProtoMember(0)] public byte   Num;
        [ProtoMember(1)] public ushort Target;
        [ProtoMember(2)] public ushort Actual;
        [ProtoMember(3)] public ushort Speed;
        [ProtoMember(4)] public ushort Acceleration;
    }
}
