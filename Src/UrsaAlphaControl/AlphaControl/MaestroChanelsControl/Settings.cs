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
        ushort Min { get; set; }
        ushort Max { get; set; }
    }
    public class ServoSettings : IChannelSettings
    {
        public ServoSettings() { }
        public ServoSettings(byte num) {
            Min = 0;
            Max = ushort.MaxValue;
            Num = num;
            DegreesAtMin = 0;
            DegreesAtMax = 180;
        }   
        public byte Num { get; set; }
        public ushort Min { get; set; }
        public ushort Max { get; set; }
        public float DegreesAtMin { get; set; }
        public float DegreesAtMax { get; set; }
    }
    public class SensorSettings : IChannelSettings
    {
        public SensorSettings() { }
        public SensorSettings(byte num)
        {
                Min = 0;
                Max = ushort.MaxValue;
                Num = num;
        }
        public byte Num { get; set; }
        public ushort Min { get; set; }
        public ushort Max { get; set; }
    }
}
