using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrsaAlphaControl.VMS
{
    public class ServoVM : VMBase
    {
        public ServoVM(Servo servo) {
            this.Servo = servo;
            servo.StatusUpdated += servo_StatusUpdated;
        }

        void servo_StatusUpdated(Servo arg1, Pololu.Usc.ServoStatus arg2)
        {
            ActualPercentValue = arg1.GetPercentValue();
            ActualValueDegrees = arg1.GetDegreesValue();
            ActualAcceleration = arg1.Status.acceleration;
            ActualSpeed        = arg1.Status.speed;
        }
        public Servo Servo { get; protected set; }
        public double PercentValueForSet
        {
            get { return percentValueForSet; }
            set {
                percentValueForSet = value; 
                Raise("PercentValueForSet");
                Servo.SetPercentValue(value);
            } 
        }
        double percentValueForSet;

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
