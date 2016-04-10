using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    [ProtoContract]
    public struct SensorValue
    {
        [ProtoMember(1)] public byte Num;
        [ProtoMember(2)] public ushort Value;
    }
}
