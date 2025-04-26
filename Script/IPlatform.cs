using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogger.Script
{
    internal interface IPlatform
    {
        public float GetVelocity();
        public float GetDirection();
    }
}
