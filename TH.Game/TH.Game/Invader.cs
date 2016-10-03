using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Game
{
    abstract class Invader : IInvader
    {
        // private MapLocation _location;

        //public MapLocation GetLocation()
        //{
        //    return _location;
        //}

        //public void SetLocation (MapLocation value)
        //{
        //    _location = value;
        //}

        //Property - getter & setter
        //public MapLocation Location
        //{
        //    get { return _location; }
        //    set { _location = value; }
        //}

        //public MapLocation Location { get; set; }

        private readonly Path _path;
        private int _pathStep = 0;

        protected virtual int StepSize { get; } = 1;

        //public bool HasScored => _pathStep > 6;
        public bool HasScored { get { return _pathStep >= _path.Length; } }
        //public int Health { get; private set; } = 2;
        // public virtual int Health { get; protected set; } = 2;
        public abstract int Health { get; protected set; }

        public bool IsNeutralized => Health <= 0;
        //public bool IsActive() => Health > 0;
        public bool IsActive => !(IsNeutralized || HasScored); 

        //public MapLocation Location 
        //{
        //    get { return _path.GetMapLocationAt (_pathStep); }
        //}

        public MapLocation Location => _path.GetMapLocationAt(_pathStep);

        public Invader(Path path)
        {
            _path = path;
            //Location= _path.GetMapLocationAt(_pathStep);
        }

        //public void Move ()
        //{
        //    _pathStep += 1;
        //    //Location = _path.GetMapLocationAt(_pathStep);
        //}

        public void Move() => _pathStep += StepSize;
        //public bool Hit()
        //{
        //    _health -= 1;
        //    return (_health > 0);
        //}

        //public bool IsActive()
        //{
        //    return (Health > 0);
        //}

        public virtual void DecreaseHealth (int factor)
        {
            Health -= factor;
            Console.WriteLine("Shot at and hit an invader.");
        }
    }
}
