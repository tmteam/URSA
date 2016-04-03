using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrsaAlphaControl.VMS
{
    public class FourLegsVM: VMBase
    {
        public FourLegsVM(Body body) {
            this.Body = body;
            FrontLeft = new LegVM(body.FrontLeft)   {
                Name = body.FrontLeft.Num+ "FL",
            };
            FrontRight = new LegVM(body.FrontRight) {
                Name = body.FrontRight.Num + "FR"
            };
            BackLeft = new LegVM(body.BackLeft)     {
                Name = body.BackLeft.Num + "BL"
            };
            BackRight = new LegVM(body.BackRight)   {
                Name = body.BackRight.Num + "BR"
            };
        }
        public Body Body { get; protected set; }
        
        public LegVM FrontLeft { get; protected set; }
        public LegVM FrontRight { get; protected set; }
        
        public LegVM BackLeft { get; protected set; }
        public LegVM BackRight { get; protected set; }

    }
}
