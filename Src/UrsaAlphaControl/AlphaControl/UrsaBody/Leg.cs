using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace Ursa.Body
{
    public class Leg
    {
        public Leg(LegType type, 
            ICerebellum cerebellum, 
            IServoChannel scapula, 
            IServoChannel thigh, 
            IServoChannel shin, 
            ISensorChannel pressure)
        {
            this.Type       = type;
            this.Scapula    = scapula;
            this.Thigh      = thigh;
            this.Shin       = shin;
            this.Pressure   = pressure;
            this.Cerebellum = cerebellum;
            cerebellum.ValuesUpdated += cerebellum_ValuesUpdated;
        }
        
        public LegType Type { get; protected set; }
        public ICerebellum Cerebellum { get; protected set; }
        public IServoChannel Scapula { get; protected set; }
        public IServoChannel Thigh { get; protected set; }
        public IServoChannel Shin { get; protected set; }
        public ISensorChannel Pressure { get; protected set; }

        public event Action<Leg, DateTime> ValuesUpdated;
        void cerebellum_ValuesUpdated(ICerebellum arg1, DateTime arg2)
        {
            if (ValuesUpdated != null)
                ValuesUpdated(this, arg2);
        }

    }
}
