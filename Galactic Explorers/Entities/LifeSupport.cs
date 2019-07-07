using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.Entities
{
    public class LifeSupport : ShipSystem
    {
        public LifeSupport(string systemDescription, string iconClass, float maximumIntegrity) : base("Life Support", systemDescription, iconClass, maximumIntegrity)
        {
        }
    }
}
