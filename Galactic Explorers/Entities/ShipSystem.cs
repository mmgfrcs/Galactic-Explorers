using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GalacticExplorers.Entities
{
    [Serializable]
    public class ShipSystem
    {
        [JsonProperty] private float currIntegrity;

        [JsonProperty(PropertyName = "sysName")] public string SystemName { get; protected set; }
        [JsonProperty(PropertyName = "sysDescription")] public string SystemDescription { get; protected set; }
        [JsonProperty(PropertyName = "iconClass")] public string IconClass { get; protected set; }
        [JsonProperty(PropertyName = "maxIntegrity")] public float MaximumIntegrity { get; protected set; }

        [JsonIgnore] public float CurrentIntegrity { get => currIntegrity; }

        public ShipSystem() {
            SystemName = "";
            SystemDescription = "";
            IconClass = "";
            
        }

        public ShipSystem(string systemName, string systemDescription, string iconClass, float maximumIntegrity)
        {
            currIntegrity = maximumIntegrity;
            SystemName = systemName;
            SystemDescription = systemDescription;
            IconClass = iconClass;
            MaximumIntegrity = maximumIntegrity;
        }

        public void DamageIntegrity(float amount)
        {
            currIntegrity -= amount;
            if (currIntegrity < 0) currIntegrity = 0;
        }

        public static ShipSystem Load(string name)
        {
            return JsonConvert.DeserializeObject<ShipSystem>(File.ReadAllText($"ShipSystems/{name}.json"));
        }


    }
}
