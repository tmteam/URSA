using Pololu.UsbWrapper;
using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;
using Ursa.Cerebellum.Telemetry;

namespace Ursa.Cerebellum.Maestro24Controller
{
    /// <summary>
    /// Cerebellum implementation for maestro 24 IO controller
    /// </summary>
    public class Maestro24Controller: ICerebellum
    {
        public Maestro24Controller() { }
        bool isConnected = false;
        public bool IsConnected { get { return isConnected; }
            protected set
            {
                isConnected = value;
                if (ConnectionStatusChanged != null)
                    ConnectionStatusChanged(this, value);
            }
        }
        public Usc Device { get; protected set; }
        IMaestroChannel[] channels = null;
        public bool IsConfigurated { get; protected set; }
        public Cerebellum.IChannel[] Channels {
            get { return channels; }
        }
        DateTime lastUpdated;
        public DateTime LastUpdated {
            get { return lastUpdated; }
        }

        public ITelemetryWriter Transcription { get; set; }

        public event Action<Maestro24Controller, bool> ConnectionStatusChanged;
        public event Action<Cerebellum.ICerebellum, DateTime> ValuesUpdated;

        public void Connect() {
            if (IsConnected)
                throw new InvalidOperationException("The device is already connected");
            // Get a list of all connected devices of this type.
            var connectedDevices = Usc.getConnectedDevices();
            if (connectedDevices.Count == 0)
                throw new Exception("Could not find device.  Make sure it is plugged in to USB " +
               "and check your Device Manager (Windows) or run lsusb (Linux).");
            Device = new Usc(connectedDevices.First());
            IsConnected = true;
            foreach (var channel in channels) {
                channel.Device = Device;
            }
        }

        public void Disconnect() {
            if(!isConnected)
                throw new InvalidOperationException("The device is not connected");
            Device.disconnect();
            Device = null;
            IsConnected = false;
            foreach (var channel in channels) {
                channel.Device = null;
            }
        }

        public void StarupSetup()
        {
            if (Device == null)
                return;

            var currentSettings = Device.getUscSettings();

            foreach (var chn in Channels)
            {
                var devChannel = currentSettings.channelSettings[chn.Num];

                if (chn is IServoChannel)
                    devChannel.mode = ChannelMode.Servo;
                else if (chn is ISensorChannel)
                    devChannel.mode = ChannelMode.Input;
                else
                    devChannel.mode = ChannelMode.Input;

                devChannel.minimum = chn.Min;
                devChannel.maximum = chn.Max;
            }

            Device.setUscSettings(currentSettings, false);
        }
        public void UpdateValues() {
            if (Device == null)
                return;
            
                
                
                Pololu.Usc.ServoStatus[] statuses;
                Device.getVariables(out statuses);
                foreach (var chn in channels) {
                    chn.DeviceChannelStatus = statuses[chn.Num];
                }
                
            lastUpdated = DateTime.Now;
            if (ValuesUpdated != null)
                ValuesUpdated(this, lastUpdated);
            
            Tools.AddFrameIfItIsPossible(this.Transcription, this.channels);
        }
        public void Configurate(IEnumerable<IChannelSettings> settings)
        {
            settings.ThrowIfSettingsAreWrong();
            if (settings.Any(s => s.Num > 23))
                throw new ArgumentException("Count of channels can not be more than 23");
            IMaestroChannel[] newChannels = new IMaestroChannel[24];
            foreach (var setting in settings)
            {
                IMaestroChannel channel;
                if (setting is ServoSettings) {
                    var servoSetting = (ServoSettings)setting;
                    channel = new MaestroServoChannel(setting.Num)
                    {
                        DegreesAtMin = servoSetting.DegreesAtMin,
                        DegreesAtMax = servoSetting.DegreesAtMax,
                    };
                }
                else if (setting is SensorSettings) {
                    var sensorSetting = (SensorSettings)setting;
                    channel = new MaestroSensorChannel(setting.Num);
                }
                else
                    channel = new MaestroEmptyChannel(setting.Num);
                
                var baseChannel = channel as ChannelBase;
                baseChannel.Min = setting.Min;
                baseChannel.Max = setting.Max;

                newChannels[setting.Num] = channel;
            }
            for (int i = 0; i < newChannels.Length; i++)
            {
                if (newChannels[i] == null)
                    newChannels[i] = new MaestroEmptyChannel((byte)i);
            }
            channels = newChannels;
            IsConfigurated = true;

        }


        
    }
}
