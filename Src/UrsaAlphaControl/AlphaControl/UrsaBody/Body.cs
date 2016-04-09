using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace Ursa.Body
{
    public class Body
    {
        public Body(ICerebellum cerebellum) {
            this.Cerebellum = cerebellum;
        }
        public ICerebellum Cerebellum { get; protected set; }
        public Leg[] Legs { get; protected set; }
        public Leg FrontLeft { get; protected set; }
        public Leg FrontRight { get; protected set; }
        public Leg BackLeft { get; protected set; }
        public Leg BackRight { get; protected set; }

        public void Configurate(BodySettings settings){
            if(Legs!=null)
                throw new InvalidOperationException("Ursa body is already configurated");
            
            if (settings.Legs.Length != 4)
                throw new InvalidOperationException("Legs count should be 4");

            if(!Cerebellum.IsConfigurated)
                Cerebellum.Configurate(settings.ChannelSettngs);

            foreach (var legset in settings.Legs)
            {
                var scapula = Cerebellum.Channels[legset.ScapulaSettings.Num]   as IServoChannel;
                var thigh   = Cerebellum.Channels[legset.ThighSettings.Num]     as IServoChannel;
                var shin    = Cerebellum.Channels[legset.ShinSettings.Num]      as IServoChannel;
                var pressure = Cerebellum.Channels[legset.PressureSettings.Num] as ISensorChannel;
                var leg = new Leg(legset.Type, Cerebellum, scapula, thigh, shin, pressure);
                switch(legset.Type)
                {
                    case LegType.FrontLeft:     FrontLeft   = leg; break;
                    case LegType.FrontRight:    FrontRight  = leg; break;
                    case LegType.BackLeft:      BackLeft    = leg; break;
                    case LegType.BackRight:     BackRight   = leg; break;
                }
            }
            Legs = new[] { FrontLeft, FrontRight, BackLeft, BackRight };
        }

    }
}
