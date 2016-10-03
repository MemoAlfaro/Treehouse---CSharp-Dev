using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Game
{
    interface IMappable
    {
        MapLocation Location { get; }
    }

    interface IMoveable
    {
        void Move();
    }

    interface IInvader : IMappable, IMoveable
    {
        //MapLocation Location { get; }
        bool HasScored { get; }
        int Health { get; }
        bool IsNeutralized { get; }
        bool IsActive { get; }
        //void Move();
        void DecreaseHealth(int factor);
    }
}

