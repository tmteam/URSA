using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum.Telemetry
{
    public interface ITelemetryWriter
    {
        bool IsRecoring { get; }
        int FramesCount { get; }
        void Add(Frame frame);
        DateTime LastAddition { get; }
    }
}
