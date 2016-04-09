using Pololu.UsbWrapper;
using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace Ursa.Cerbellum.Maestro24Controller
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
        bool wasConnected= false;
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

        public event Action<Maestro24Controller, bool> ConnectionStatusChanged;
        public event Action<Cerebellum.ICerebellum, DateTime> ValuesUpdated;

        public void Connect()
        {
            if (IsConnected)
                throw new InvalidOperationException("The device is already connected");
            if(wasConnected)
                throw new InvalidOperationException("the implementation does not allow recconection. Use a new cerebellum exemplar instead");
            // Get a list of all connected devices of this type.
            var connectedDevices = Usc.getConnectedDevices();

            if (connectedDevices.Count == 0)
                throw new Exception("Could not find device.  Make sure it is plugged in to USB " +
               "and check your Device Manager (Windows) or run lsusb (Linux).");
            Device = new Usc(connectedDevices.First());
            IsConnected = true;
            wasConnected = true;
        }

        public void Disconnect()
        {
            if(!isConnected)
                throw new InvalidOperationException("The device is not connected");
            Device.disconnect();
            Device = null;
            IsConnected = false;
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
                var servoSetting = setting as ServoSettings;
                if (servoSetting != null)
                    channel = new MaestroServoChannel(Device, setting.Num)
                    {
                        DegreesAtMin = servoSetting.DegreesAtMin,
                        DegreesAtMax = servoSetting.DegreesAtMax,
                    };
                else {
                    var sensorSetting = setting as SensorSettings;
                    if (sensorSetting != null)
                        channel = new MaestroSensorChannel(Device, setting.Num);
                    else
                        channel = new MaestroEmptyChannel(Device, setting.Num);
                }
                
                var baseChannel = channel as ChannelBase;
                baseChannel.Min = setting.Min;
                baseChannel.Max = setting.Max;

                newChannels[servoSetting.Num] = channel;
            }
            for (int i = 0; i < newChannels.Length; i++)
            {
                if (newChannels[i] == null)
                    newChannels[i] = new MaestroEmptyChannel(Device, (byte)i);
            }
            IsConfigurated = true;

        }
    }
}
