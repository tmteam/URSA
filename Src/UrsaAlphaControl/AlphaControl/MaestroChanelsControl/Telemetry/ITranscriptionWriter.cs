using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ursa.Cerebellum.Telemetry
{
    public interface ITranscriptionWriter
    {
        HeaderInfo Header { get; }
        IEnumerable<Frame> Frames { get; }
        void Add(Frame frame);
        void Finalize();
    }
}
