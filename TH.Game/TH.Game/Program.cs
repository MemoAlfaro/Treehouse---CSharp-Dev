using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(8, 5);
            try
            {
                Path path = new Path(new MapLocation[] {
                    new MapLocation (0, 2, map),
                    new MapLocation (1, 2, map),
                    new MapLocation (2, 2, map),
                    new MapLocation (3, 2, map),
                    new MapLocation (4, 2, map),
                    new MapLocation (5, 2, map),
                    new MapLocation (6, 2, map),
                    new MapLocation (7, 2, map)
                });

                IInvader[] invaders =
                {
                    new ShieldedInvader(path),
                    new FastInvader(path),
                    new StrongInvader(path),
                    new BasicInvader(path),
                    new ResurrectingInvader (path)
                };

                Tower[] towers =
                {
                    new SnipperTower (new MapLocation (1, 3, map)),
                    new Tower (new MapLocation (3, 3, map)),
                    new Tower (new MapLocation (5, 3, map))
                };

                MapLocation myLoc = new MapLocation(7, 1, map);
                if (path.IsOnPath(myLoc))
                {
                    Console.WriteLine(myLoc + " is on path");
                }

                Level level1 = new Level(invaders)
                {
                    Towers = towers
                };

                bool playerWon = level1.Play();
                Console.WriteLine("Player " + (playerWon ? "won." : "lost."));
            }

            catch (OutOfBoundsException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (TH_GameException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
