using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum
{
    public interface IChannel {
        byte Num      { get; }
        ushort Min    { get; }
        ushort Max    { get; }
        ushort Actual { get; }
        DateTime LastUpdated { get; }
        
        event Action<IChannel, DateTime> ChannelUpdated;
    }
   
  
   
}
