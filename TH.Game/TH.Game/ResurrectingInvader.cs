using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Game
{
    class ResurrectingInvader : IInvader
    {
        private BasicInvader _life1;
        private StrongInvader _life2;

        public MapLocation Location =>
            _life1.IsNeutralized ? _life2.Location : _life1.Location;

        public bool HasScored => _life1.HasScored || _life2.HasScored;

        public int Health =>
            _life1.IsNeutralized ? _life2.Health : _life1.Health;

        public bool IsNeutralized => _life1.IsNeutralized && _life2.IsNeutralized;
        public bool IsActive => !(IsNeutralized || HasScored);

        public ResurrectingInvader (Path path)
        {
            _life1 = new BasicInvader(path);
            _life2 = new StrongInvader(path);
        }

        public void Move()
        {
            _life1.Move();
            _life2.Move();
        }

        public void DecreaseHealth(int factor)
        {
            if (! _life1.IsNeutralized)
                _life1.DecreaseHealth(factor);
            else
                _life2.DecreaseHealth(factor);
        }
    }
}
