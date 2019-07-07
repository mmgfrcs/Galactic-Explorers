using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.Entities
{
    public class Power : ShipSystem
    {
        [JsonProperty] private float maxCapacity;
        [JsonProperty] private float capacity;

        public Power(float capacity, string systemDescription, string iconClass, float maximumIntegrity) : base("Power", systemDescription, iconClass, maximumIntegrity)
        {
            Capacity = capacity;
            MaxCapacity = capacity;
        }

        [JsonIgnore] public float Capacity { get => capacity; private set => capacity = value; }
        [JsonIgnore] public float MaxCapacity { get => maxCapacity; private set => maxCapacity = value; }
    }
}
