using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Game
{
    class StrongInvader: Invader
    {
        protected override int StepSize { get; } = 2;

        public override int Health { get; protected set; } = 2;

        public StrongInvader(Path path): base (path)
        { }
    }
}
