using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrsaAlphaControl.VMS
{
    public class ServoVM : VMBase
    {
        public ServoVM(Servo servo)
        {
            this.Servo = servo;
        }
        public Servo Servo { get; protected set; }
        public ushort PercentValueForSet
        {
            get { return percentValueForSet; }
            set
            {
                percentValueForSet = value; Raise("PercentValueForSet");
                Servo.SetValue((ushort)(value*100));
            } }
        ushort percentValueForSet;

        public double ActualValue { get { return actualValue; } set { actualValue = value; Raise("ActualValue"); } }
        double actualValue;

        public ushort ActualValueMs { get { return actualValueMs; } set { actualValueMs = value; Raise("ActualValueMs"); } }
        ushort actualValueMs;

        
        public string Name { get; set; }
    }
}
