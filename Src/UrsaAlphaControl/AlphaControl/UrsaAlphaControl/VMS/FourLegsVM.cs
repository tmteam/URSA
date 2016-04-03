using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrsaAlphaControl.VMS
{
    public class FourLegsVM: VMBase
    {
        public FourLegsVM(Body body)
        {
            this.Body = body;
            FrontLeft = new LegVM(body.FrontLeft)   {
                Name = "1FL",
            };
            FrontRight = new LegVM(body.FrontRight) {
                Name = "2FR"
            };
            BackLeft = new LegVM(body.BackLeft)     {
                Name = "3BL"
            };
            BackRight = new LegVM(body.BackRight)   {
                Name = "4BR"
            };
        }
        public Body Body { get; protected set; }
        
        public LegVM FrontLeft { get; protected set; }
        public LegVM FrontRight { get; protected set; }
        
        public LegVM BackLeft { get; protected set; }
        public LegVM BackRight { get; protected set; }

    }
}
