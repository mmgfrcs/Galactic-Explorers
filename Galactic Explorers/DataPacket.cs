using GalacticExplorers.Entities;
using GalacticExplorers.JSONUtilities;
using GalacticExplorers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers
{
    [Serializable]
    public class DataPacket
    {
        public int ver;
        public string error;
        public Story updatedStory;
        public float hullMod;
        public List<ShipSystem> systemChanges;
        public Point positionChange;
        public Sector sectorChange;
        public int cashChange, materialChange, fuelChange;
    }
}
