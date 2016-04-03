using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrsaAlphaControl
{
    public class Servo
    {
        public Servo(byte num)
        {
            Num = num;
        }
        public byte Num { get; protected set; }
        public Usc Device { get; set; }
        public ushort GetValue()
        {
            throw new NotImplementedException();
        }
        public void SetValue(ushort value)
        {
            if(Device!=null)
                Device.setTarget(Num, value);
        }
    }
    public class Leg
    {
        public int Num;
        public Leg(int num)
        {
            this.Num = num;
            Scapula = new Servo((byte)((num - 1) * 6 + 0));
            Thigh   = new Servo((byte)((num - 1) * 6 + 1));
            Shin    = new Servo((byte)((num - 1) * 6 + 2));
        }
        Usc device;
        public Usc Device { get { return device; } 
            set { 
                device = value;
                Scapula.Device = value;
                Thigh.Device = value;
                Shin.Device = value;
            } }
        public Servo Scapula { get; protected set; }
        public Servo Thigh { get; protected set; }
        public Servo Shin { get; protected set; }
    }
    public class Body
    {
        public Body()
        {
            FrontLeft  = new Leg(1);
            FrontRight = new Leg(2);
            BackLeft   = new Leg(3);
            BackRight  = new Leg(4);
        }
        Usc device;
        public Usc Device {
            get { return device; }
            set {
                device = value;
                FrontLeft.Device = value;
                FrontRight.Device = value;
                BackLeft.Device = value;
                BackRight.Device = value;
            }
        }
        public Leg FrontLeft  { get; protected set; }
        public Leg FrontRight { get; protected set; }
        public Leg BackLeft   { get; protected set; }
        public Leg BackRight  { get; protected set; }

    }
}
