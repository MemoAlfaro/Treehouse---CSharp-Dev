using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Game
{
    class TH_GameException : System.Exception
    {
        public TH_GameException ()
        {
        }

        public TH_GameException (string message): base (message)
        {
        }
    }

    class OutOfBoundsException : TH_GameException
    {
        public OutOfBoundsException()
        {
        }

        public OutOfBoundsException (string message) : base (message)
        {
        }
    }
}