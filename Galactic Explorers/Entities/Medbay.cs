using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.Entities
{
    public class Medbay : ShipSystem
    {
        [JsonProperty] private int potency;
        [JsonIgnore] public int Potency { get => potency; private set => potency = value; }
        public Medbay(string systemDescription, string iconClass, float maximumIntegrity) : base("Medbay", systemDescription, iconClass, maximumIntegrity)
        {
            potency = 1;
        }

    }
}
