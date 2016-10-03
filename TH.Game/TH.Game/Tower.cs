using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Game
{
    class Tower
    {
        //private const int _range = 1;
        //private const int _power = 1;
        //private const double _accuracy = 0.8;
        protected virtual int Range { get; } = 1;
        protected virtual int Power { get; } = 1;
        protected virtual double Accuracy { get; } = 0.8;

        //private static readonly Random _random = new Random();

        private readonly MapLocation _location;

        public Tower (MapLocation location)
        {
            _location = location;
        }

        public bool IsSuccessfulShot()
        {
            //return _random.NextDouble() < Accuracy;
            return Random.NextDouble() < Accuracy;
        }

        public void FireOnInvaders (IInvader[] invaders)
        {
           // int index = 0;
           // while (index < invaders.Length)
           // {
           //     Invader invader = invaders[index];
           //     // do stuff with invader

           //     index++;
           //}

            //for (int index= 0; index < invaders.Length; index++)
            //{
            //    Invader invader = invaders[index];
            //    // do stuff with invader
            //}

            foreach (IInvader invader in invaders)
            {
                if (invader.IsActive && _location.InRangeOf (invader.Location, Range))
                {
                    if (IsSuccessfulShot())
                    {
                        invader.DecreaseHealth(Power);

                        if (invader.IsNeutralized)
                        {
                            Console.WriteLine("Neutralized an invader at " + invader.Location + "! :)");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Shot at and missed an invader.");
                    }

                    // towers can shoot only one invader at the time
                    break;
                }
            }
        }
    }
}
