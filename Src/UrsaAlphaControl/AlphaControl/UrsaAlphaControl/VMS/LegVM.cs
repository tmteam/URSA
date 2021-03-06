﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Body;

namespace UrsaAlphaControl.VMS
{
    public class LegVM: VMBase
    {
        public Leg Leg { get; protected set; }
        public LegVM(Leg leg)
        {
            this.Leg = leg;
            Scapula = new ServoVM(leg.Scapula) {
                 Name = "Scapula",
            };
            Thigh = new ServoVM(leg.Thigh) {
                Name = "Thigh",
            };
            Shin = new ServoVM(leg.Shin) {
                Name = "Shin",
            };
            MaxRawPressure = 1024;
            leg.ValuesUpdated += leg_ValuesUpdated;
        }
        public ServoVM Scapula { get; protected set; }
        public ServoVM Thigh { get; protected set; }
        public ServoVM Shin { get; protected set; }
        public string Name { get; set; }
        public double Pressure { get { return pressure; } set { pressure = value; Raise("Pressure"); } }
        double pressure;
        public double RawPressure { get { return rawPressure; } set { rawPressure = value; Raise("RawPressure"); } }
        double rawPressure;
        public double MaxRawPressure { get; set; }
        void leg_ValuesUpdated(Leg arg1, DateTime arg2)
        {
            RawPressure = Leg.Pressure.Actual;
            Pressure = ((double)RawPressure / 1024) * 100;
        }
    }
}
