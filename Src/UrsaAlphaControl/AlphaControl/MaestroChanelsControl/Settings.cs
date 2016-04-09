using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    public interface IChannelSettings
    {
        byte Num { get; set; }
        public ushort Min { get; set; }
        public ushort Max { get; set; }
      
    }
    public class ChannelSettingsBase : IChannelSettings
    {

        public byte Num{get;set;}

        public ushort Min{get;set;}

        public ushort Max{get;set;}
    }
    public class ServoSettings : ChannelSettingsBase
    {
        public float DegreesAtMin { get; set; }
        public float DegreesAtMax { get; set; }
    }
    public class SensorSettings : ChannelSettingsBase
    {
    }
}
