using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    public struct ServoStatus {
        public ushort Target;
        public ushort Actual;
        public ushort Speed;
        public ushort Acceleration;
    }
}
