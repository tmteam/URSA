using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum.Telemetry;

namespace Ursa.Cerebellum
{
    /// <summary>
    /// Low level interface for IO-controller
    /// </summary>
    public interface ICerebellum {
        /// <summary>
        /// Channels denending on the cerebellum configuration
        /// </summary>
        IChannel[] Channels { get; }
        bool IsConfigurated { get; }

        ITelemetryWriter Transcription { get; set; }
        void Configurate(IEnumerable<IChannelSettings> settings);
        /// <summary>
        /// Shows timestamp of a last data update
        /// </summary>
        DateTime LastUpdated { get; }
        /// <summary>
        /// make the cerebellum to update the channel values
        /// </summary>
        void UpdateValues();
        /// <summary>
        /// Fires when values were updated
        /// </summary>
        event Action<ICerebellum, DateTime> ValuesUpdated;
    }
}
