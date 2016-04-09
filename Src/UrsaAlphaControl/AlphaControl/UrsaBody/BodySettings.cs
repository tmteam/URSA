using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace Ursa.Body
{
    public class BodySettings
    {
        public LegSettings[] Legs{get;set;}
        public IEnumerable<IChannelSettings> ChannelSettngs 
        {
            get
            {
                return Legs
                    .SelectMany(l=> new IChannelSettings[]{ 
                        l.PressureSettings, 
                        l.ScapulaSettings, 
                        l.ShinSettings, 
                        l.ThighSettings
                    });
            }
        }
    }
    
}
