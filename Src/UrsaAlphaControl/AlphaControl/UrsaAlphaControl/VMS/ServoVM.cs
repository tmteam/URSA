using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Cerebellum;

namespace UrsaAlphaControl.VMS
{
    public class ServoVM : VMBase
    {
        public ServoVM(IServoChannel servo) {
            this.Servo = servo;
            servo.ChannelUpdated += servo_ChannelUpdated;
        }

        void servo_ChannelUpdated(IChannel arg1, DateTime arg2)
        {
            ActualPercentValue = Servo.GetActualNormalized()*100;
            ActualValueDegrees = Servo.GetActualInDegrees();
            ActualAcceleration = Servo.Status.Acceleration;
            ActualSpeed        = Servo.Status.Speed;
        }

        public IServoChannel Servo { get; protected set; }
        public float PercentValueForSet
        {
            get { return percentValueForSet; }
            set {
                percentValueForSet = value; 
                Raise("PercentValueForSet");
                Servo.WriteNormalized(value / (float)100);
            } 
        }
        float percentValueForSet;

        public double ActualSpeed { get { return actualSpeed; } set { actualSpeed = value; Raise("ActualSpeed"); } }
        double actualSpeed;
        public double ActualAcceleration { get { return actualAcceleration; } set { actualAcceleration = value; Raise("ActualAcceleration"); } }
        double actualAcceleration;

        public double ActualPercentValue { get { return actualPercentValue; } set { actualPercentValue = value; Raise("ActualPercentValue"); } }
        double actualPercentValue;

        public double ActualValueDegrees { get { return actualValueDegrees; } set { actualValueDegrees = value; Raise("ActualValueDegrees"); } }
        double actualValueDegrees;
        
        public string Name { get; set; }
    }
}
