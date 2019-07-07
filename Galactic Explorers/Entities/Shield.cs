using GalacticExplorers.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.Entities
{
    public class Shield : ShipSystem, IDamageable
    {
        [JsonProperty] private float maxCapacity;
        [JsonProperty] public float CurrentCapacity { get; private set; }
        [JsonIgnore] public float MaxCapacity { get { return maxCapacity * (CurrentIntegrity / MaximumIntegrity); } }
        public Shield(float maxCapacity, string systemDescription, string iconClass, float maximumIntegrity) : base("Shield", systemDescription, iconClass, maximumIntegrity)
        {
            this.maxCapacity = maxCapacity;
            CurrentCapacity = maxCapacity;
        }

        public void Damage(float amount)
        {
            CurrentCapacity -= amount;
            if (CurrentCapacity < 0) CurrentCapacity = 0;
        }
    }
}
