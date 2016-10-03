using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Game
{
    class SnipperTower : Tower
    {
        protected override double Accuracy { get; } = 1.0;

        public SnipperTower(MapLocation location) : base (location)
        { }
    }
}
