using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Game
{
    class MapLocation: Point
    {
        public MapLocation (int x, int y, Map map) : base (x, y)
        {
            if (! map.OnMap(this))
            {
                //throw new OutOfBoundsException("OutOfBoundsException: "
                //    + x + "," + y + " is not on the map.");

                throw new OutOfBoundsException("OutOfBoundsException: "
                    + this + " is not on the map.");

                //throw new TH.GameException("TH.GameException: "
                //    + x + "," + y + " is not on the map.");

                //throw new System.Exception("System.Exception: "
                //    + x + "," + y + " is not on the map.");

            }
        }

        public bool InRangeOf (MapLocation location, int range)
        {
            return DistanceTo(location) <= range;
        }
    }
}
