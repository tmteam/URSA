using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrsaAlphaControl
{
    public static class Tools
    {
        public static ushort Proportion(double value, double valueA, double valueB, ushort returnsAtA, ushort returnsAtB)
        {
            var k = (value -valueA)/ (valueB - valueA);
            return (ushort)(returnsAtA + Convert.ToUInt16((returnsAtB - returnsAtA) * k));
        }
        public static double Proportion(double value, double valueA, double valueB, double returnsAtA, double returnsAtB)
        {
            var k = (value- valueA)/(valueB - valueA);
            return returnsAtA + (returnsAtB - returnsAtA) * k;
        }
    }
    public class Sensor
    {
        public Sensor(byte channel)
        {
            this.Channel = channel;
        }
        public Usc Device { get; set; }
        public byte Channel { get; protected set; }
        public ushort RawValue { get { return status.position; } }
        ServoStatus status;
        public ServoStatus Status { get { return status; } set { status = value; if (StatusUpdated != null) StatusUpdated(this, value); } }
        public event Action<Sensor, ServoStatus> StatusUpdated;
        

    }
    public class Servo
    {
        public Servo(byte channel)
        {
            Channel = channel;
            ValueAt0Percent = 1680;
            ValueAt100Percent = 10200;
            DegreeAt0Percent = 0;
            DegreeAt100Percent = 180;
        }
        public double DegreeAt0Percent { get; set; }
        public double DegreeAt100Percent { get; set; }
        public ushort ValueAt0Percent { get; set; }
        public ushort ValueAt100Percent { get; set; }
        public byte Channel { get; protected set; }
        public Usc Device { get; set; }

        public double GetDegreesValue() {
            var percent = GetPercentValue();
            return Tools.Proportion(percent, DegreeAt0Percent, DegreeAt100Percent, 0 , 100);
        }

        public double GetPercentValue() {
            var mm = Math.Min(Math.Max(status.position, ValueAt0Percent), ValueAt100Percent);
            return Tools.Proportion(mm, ValueAt0Percent, ValueAt100Percent, 0 ,100);
        }
        
        ServoStatus status;
        public ServoStatus Status { get { return status; } set { status = value; if (StatusUpdated != null) StatusUpdated(this, value); } }
        public event Action<Servo, ServoStatus> StatusUpdated;
        
        public void SetPercentValue(double percentValue) {
            var value = Tools.Proportion(percentValue, 0,100, ValueAt0Percent, ValueAt100Percent);
            if(Device!=null)
                Device.setTarget(Channel, value);
        }
        
        public void SetAcceleration(ushort acceleration) {
            if (Device != null)
                Device.setAcceleration(Channel, acceleration);
        }

        public void SetSpeed(ushort speed) {
            if (Device != null)
                Device.setSpeed(Channel, speed);
        }
    }
    public class Leg
    {
        public Leg(int num, Servo scapula, Servo thigh, Servo shin, Sensor pressure) {
            this.Num        = num;
            this.Scapula    = scapula;
            this.Thigh      = thigh;
            this.Shin       = shin;
            
            this.Servos = new[] { this.Scapula, this.Thigh, this.Shin };
            this.Pressure = pressure;
        }

        public Leg(int num) {
            this.Num        = num;
            this.Scapula    = new Servo((byte)(num * 6 + 0));
            this.Thigh      = new Servo((byte)(num * 6 + 1));
            this.Shin       = new Servo((byte)(num * 6 + 2));
            this.Servos = new[] { this.Scapula, this.Thigh, this.Shin };
            this.Pressure   = new Sensor((byte)(num * 6 + 3));
        }
        Usc device;
        public Usc Device { get { return device; } 
            set { 
                device = value;
                foreach (var srv in Servos)
                    srv.Device = value;
                Pressure.Device = value;
            } }
        public int Num { get; protected set; }
        public Servo[] Servos { get; protected set; }
        public Servo Scapula { get; protected set; }
        public Servo Thigh { get; protected set; }
        public Servo Shin { get; protected set; }
        public Sensor Pressure { get; protected set; }
        public void StatusesHaveUpdated(){
            
        }
    }
    public class Body
    {
        public Body()
        {
            FrontLeft  = new Leg(0);
            FrontRight = new Leg(1);
            BackLeft   = new Leg(2);
            BackRight  = new Leg(3);
            Legs = new[] { FrontLeft, FrontRight, BackLeft, BackRight };
            Servos = Legs.SelectMany(s => s.Servos).ToArray();
            Sensors = Legs.Select(l => l.Pressure).ToArray();
        }
        Usc device;
        public Usc Device {
            get { return device; }
            set {
                device = value;
                foreach (var leg in Legs)
                    leg.Device = value;
            }
        }
        public Leg[] Legs { get; protected set; }
        public Servo[] Servos { get; protected set; }
        public Sensor[] Sensors { get; protected set; }
        public Leg FrontLeft  { get; protected set; }
        public Leg FrontRight { get; protected set; }
        public Leg BackLeft   { get; protected set; }
        public Leg BackRight  { get; protected set; }
        public void StarupServoSetup()
        {
            if (Device == null)
                return;
            var currentSettings = Device.getUscSettings();
            
            foreach(var srv in Servos){
                var channel = currentSettings.channelSettings[srv.Channel];
                channel.mode = ChannelMode.Servo;
                channel.minimum = srv.ValueAt0Percent;
                channel.maximum = srv.ValueAt100Percent;
            }
            
            foreach (var sns in Sensors) {
                var channel = currentSettings.channelSettings[sns.Channel];
                channel.mode = ChannelMode.Input;
            }

            Device.setUscSettings(currentSettings, false);
        }
        public void UpdateData()
        {
            if (Device == null)
                return;
            //try
            {
                ServoStatus[] statuses;
                Device.getVariables(out statuses);
                //SeparateServos info anong the legs;
                foreach (var leg in Legs)
                {
                    leg.StatusesHaveUpdated();
                }
                foreach (var srv in Servos)
                {
                    srv.Status = statuses[srv.Channel];
                }
                foreach (var sns in Sensors)
                {
                    sns.Status = statuses[sns.Channel];
                }
            }
            //catch(Exception ex){

            //}
        }

    }
}
