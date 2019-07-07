using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.Entities
{
    public class Engines : ShipSystem
    {
        [JsonProperty] private float fuelUse;
        [JsonProperty] private float speed;

        public Engines(float speed, float fuelUse, string systemDescription, string iconClass, float maximumIntegrity) : base("Engines", systemDescription, iconClass, maximumIntegrity)
        {
        }

        [JsonIgnore] public float Speed { get => speed; private set => speed = value; }
        [JsonIgnore] public float FuelUse { get => fuelUse; private set => fuelUse = value; }
    }
}
