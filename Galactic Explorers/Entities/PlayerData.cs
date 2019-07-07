using GalacticExplorers.Entities;
using GalacticExplorers.Interfaces;
using GalacticExplorers.JSONUtilities;
using GalacticExplorers.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.Entities
{
    [Serializable]
    public class PlayerData : IDamageable
    {
        [JsonProperty]
        private Point position;
        
        [JsonProperty] public string Name { get; private set; }
        [JsonProperty] public int Cash { get; set; }
        [JsonProperty] public int Material { get; set; }
        [JsonProperty] public int Fuel { get; set; }
        [JsonProperty] public List<ShipSystem> Systems { get; private set; }
        [JsonProperty] public float CurrentHull { get; private set; }
        [JsonProperty] public float MaximumHull { get; private set; }
        [JsonProperty] public Sector CurrentSector { get; private set; }
        [JsonIgnore] public Point Position => position;
        
        public PlayerData()
        {
            Name = "";
            Systems = new List<ShipSystem>();
            position = new Point(0, 0);
            MaximumHull = 1;
            CurrentHull = 1;
        }

        public PlayerData(string name, int cash, int material, int fuel, float hull, List<ShipSystem> systems, Sector currSector,Point position)
        {
            Name = name;
            Cash = cash;
            Material = material;
            Fuel = fuel;
            MaximumHull = hull;
            CurrentHull = hull;
            Systems = systems;
            CurrentSector = currSector;
            this.position = position;
        }

        public void Damage(float amount)
        {
            CurrentHull -= amount;
            if (CurrentHull < 0) CurrentHull = 0;
        }

        public void ChangeSector(Sector newSector, Point newPosition)
        {
            CurrentSector = newSector;
            Move(newPosition);
        }

        public void Move(Point newPosition)
        {
            position = newPosition;
        }
    }
}
