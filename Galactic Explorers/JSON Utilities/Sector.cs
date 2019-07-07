using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.JSONUtilities
{
    public class Sector
    {
        [JsonProperty] private string sectorName;
        [JsonIgnore] private List<Location> locations;

        [JsonIgnore] public string SectorName { get => sectorName; private set => sectorName = value; }
        [JsonIgnore] public List<Location> Locations { get => locations; private set => locations = value; }

        public Sector(string sectorName)
        {
            this.sectorName = sectorName;
            LoadLocation();
        }

        public void LoadLocation()
        {
            locations = Location.LoadAll(sectorName);
        }
    }
}
