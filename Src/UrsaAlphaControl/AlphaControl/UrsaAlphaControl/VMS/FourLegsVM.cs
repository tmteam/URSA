using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ursa.Body;

namespace UrsaAlphaControl.VMS
{
    public class FourLegsVM: VMBase
    {
        public FourLegsVM(Body body) {
            this.Body = body;
            FrontLeft = Create(body.FrontLeft) ;
            FrontRight = Create(body.FrontRight);
            BackLeft = Create(body.BackLeft) ;
            BackRight = Create(body.BackRight);
        }
        LegVM Create(Leg leg)
        {
            return new LegVM(leg) { Name = leg.Type.ToString() };
        }
        public Body Body { get; protected set; }
        
        public LegVM FrontLeft { get; protected set; }
        public LegVM FrontRight { get; protected set; }
        
        public LegVM BackLeft { get; protected set; }
        public LegVM BackRight { get; protected set; }

    }
}
