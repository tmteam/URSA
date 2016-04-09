using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace Ursa.Body
{
    public class LegSettings
    {
        public LegType Type { get; set; }
        public ServoSettings ScapulaSettings { get; set; }
        public ServoSettings ThighSettings { get; set; }
        public ServoSettings ShinSettings { get; set; }
        public SensorSettings PressureSettings { get; set; }
    }
}
