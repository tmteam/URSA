﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum.Telemetry
{
    [ProtoContract] 
    public class Header {
        [ProtoMember(1)] public string   Description { get; set; }
        [ProtoMember(2)] public DateTime RecordStart { get; set; }
        [ProtoMember(3)] public DateTime RecordEnd { get; set; }
        [ProtoMember(4)] public int FramesCount      { get; set; }
    
    }
}
