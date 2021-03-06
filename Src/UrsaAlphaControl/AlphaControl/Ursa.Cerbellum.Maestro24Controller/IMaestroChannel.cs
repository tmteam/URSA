﻿using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace Ursa.Cerebellum.Maestro24Controller
{
    public interface IMaestroChannel:IChannel
    {
        Usc Device { get; set; }
        ServoStatus DeviceChannelStatus { get; set; }
    }
}
