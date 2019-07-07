using GalacticExplorers.JSONUtilities;
using GalacticExplorers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace GalacticExplorers.Entities
{
    [Serializable]
    public class GameData
    {
        [JsonProperty] private Story story;
        [JsonProperty] private string sessionId;
        [JsonProperty] private List<Sector> sectors;
        [JsonProperty] private PlayerData player;

        [JsonIgnore] public PlayerData Player { get => player; private set => player = value; }
        [JsonIgnore] public List<Sector> Sectors { get => sectors; private set => sectors = value; }
        [JsonIgnore] public string SessionId { get => sessionId; private set => sessionId = value; }
        [JsonIgnore] public Story Story { get => story; private set => story = value; }

        public static GameData NewGame(string playerName, Story startStory)
        {
            GameData data = new GameData();

            string[] sectors = File.ReadAllLines("Sectors.txt");
            data.sectors = new List<Sector>();
            foreach(string sec in sectors)
            {
                data.sectors.Add(new Sector(sec));
            }

            Random rnd = new Random();
            Sector chosenSector = data.sectors[(int)Math.Round(rnd.NextDouble() * (data.sectors.Count - 1))];
            Location chosenLocation = chosenSector.Locations[(int)Math.Round(rnd.NextDouble() * (chosenSector.Locations.Count - 1))];
            
            data.sessionId = Cryptography.GetHashString(playerName + DateTime.Now.ToFileTime().ToString()).Substring(0, 10);
            data.Player = new PlayerData(playerName, 50000, 2500, 100, 800, new List<ShipSystem>() {
                new Shield(300, "", "fas fa-circle-notch", 400),
                new Medbay("", "fas fa-plus-square", 240),
                new LifeSupport("", "fas fa-thermometer-full", 200),
                new Engines(25, 1.667f, "", "fas fa-rocket", 650),
                new Power(1000, "", "fas fa-car-battery", 500)
            }, chosenSector, chosenLocation.position);
            data.story = startStory;
            return data;
        }

        
    }
}
