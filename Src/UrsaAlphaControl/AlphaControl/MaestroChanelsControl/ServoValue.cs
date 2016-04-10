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
        [ProtoMember(1)] public byte   Num;
        [ProtoMember(2)] public ushort Target;
        [ProtoMember(3)] public ushort Actual;
        [ProtoMember(4)] public ushort Speed;
        [ProtoMember(5)] public ushort Acceleration;
    }
}
