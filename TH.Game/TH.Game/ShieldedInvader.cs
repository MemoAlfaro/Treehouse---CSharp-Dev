using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Game
{
    class ShieldedInvader : Invader
    {
        //private static Random _shield = new Random();
        public override int Health { get; protected set; } = 2;

        public ShieldedInvader(Path path) : base (path)
        { }

        public override void DecreaseHealth (int factor)
        {
            if (Random.NextDouble() < 0.5)
            {
                base.DecreaseHealth(factor);
            }
            else
            {
                Console.WriteLine("Shot at a shielded invader but it sustained no damage.");
            }
        }
    }
}
