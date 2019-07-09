using GalacticExplorers.JSONUtilities;
using GalacticExplorers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text;

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

        static Random rnd = new Random();
        public void GameOverStory()
        {
            story = GameManager.Instance.GetGameOverStory();
        }

        public void ChangeStory(int opt)
        {
            story = GameManager.Instance.GetNextStory(story, opt);
            ParseStoryEffect();
            if (Player.CurrentHull == 0) story = GameManager.Instance.GetGameOverStory();
        }

        string ParseStoryEffect()
        {
            if (story.effects != null)
            {
                StringBuilder sb = new StringBuilder();
                try
                {

                    foreach (var eff in story.effects)
                    {
                        string[] effStr = eff.Split(' ');

                        switch (effStr[0].ToLower())
                        {
                            case "hull":
                                {
                                    if (effStr.Length == 2) sb.AppendLine(ParseHullModifier(effStr[1]));//hull -10 and hull -10%
                                    else if (effStr.Length == 3)
                                    {
                                        if (effStr[2].ToLower() == "max") sb.AppendLine(ParseHullModifier(effStr[1], null, true)); //hull -10% max
                                        else sb.AppendLine(ParseHullModifier(effStr[1], effStr[2])); //hull -10 -20 and hull -10% -20%
                                    }
                                    else if (effStr.Length == 4) sb.AppendLine(ParseHullModifier(effStr[1], effStr[2], true)); //hull -10% -20% max
                                    break;
                                }
                            case "cash":
                                {
                                    if (effStr.Length == 2) sb.AppendLine(ParseCoinModifier(effStr[1]));
                                    else if (effStr.Length == 3) sb.AppendLine(ParseCoinModifier(effStr[1], effStr[2]));
                                    break;
                                }
                            case "material":
                                {
                                    if (effStr.Length == 2) sb.AppendLine(ParseMaterialModifier(effStr[1]));
                                    else if (effStr.Length == 3) sb.AppendLine(ParseMaterialModifier(effStr[1], effStr[2]));
                                    break;
                                }
                        }
                    }
                }
                catch
                {
                    return "Parse Error";
                }
            }
            else return null;

            return null;
        }

        string ParseHullModifier(string n1, string n2 = null, bool max=false)
        {
            float dmgVal = max ? GetCurrentMaxStat(Player.MaximumHull, n1, n2) : GetCurrentMaxStat(Player.CurrentHull, n1, n2);

            Player.Damage(-dmgVal);
            if (dmgVal > 0)
                return $"Hull {(dmgVal < 0 ? "damaged" : "repaired")} by {MathF.Abs(dmgVal)}";
            else return "";
        }

        string ParseCoinModifier(string n1, string n2 = null)
        {
            int val = GetSingularStat(Player.Cash, n1, n2);

            Player.AddCash(val);
            if (val > 0)
                return $"{(val < 0 ? "Lost" : "Earned")} {MathF.Abs(val)} coins";
            else return "";
        }

        string ParseMaterialModifier(string n1, string n2 = null)
        {
            int val = GetSingularStat(Player.Material, n1, n2);

            Player.AddMaterial(val);
            if (val > 0)
                return $"{(val < 0 ? "Lost" : "Earned")} {MathF.Abs(val)} materials";
            else return "";
        }

        int GetSingularStat(int baseStat, string n1, string n2 = null)
        {
            int val = 0;
            if (n2 == null)
            {
                if (n1.EndsWith('%'))
                {
                    string dmg = n1.Remove(n1.IndexOf('%'));
                    val = (int)Math.Round(float.Parse(dmg) * baseStat / 100f); //cash -10%

                }
                else val = int.Parse(n1); //cash -10
            }
            else
            {
                if (n1.EndsWith('%'))
                {
                    string dmg1 = n1.Remove(n1.IndexOf('%'));
                    string dmg2 = n2.Remove(n2.IndexOf('%'));
                    float num1 = float.Parse(dmg1), num2 = float.Parse(dmg2);
                    val = (int)Math.Round((rnd.NextDouble() * (num2 - num1) + num1) * baseStat / 100f); //cash -10% -20%

                }
                else
                {
                    float num1 = float.Parse(n1), num2 = float.Parse(n2);
                    val = (int)Math.Round((rnd.NextDouble() * (num2 - num1) + num1)); //cash -10 -20
                }
            }
            return val;
        }

        /// <summary>
        /// Parse damage number for stat that has Current/Max value such as Shield Capacity and Hull
        /// </summary>
        /// <param name="baseStat">The stat to compare against</param>
        /// <param name="n1">String 1</param>
        /// <param name="n2">String 2</param>
        /// <returns></returns>
        float GetCurrentMaxStat(float baseStat, string n1, string n2 = null)
        {
            float dmgVal = 0;
            if (n2 == null)
            {
                if (n1.EndsWith('%'))
                {
                    string dmg = n1.Remove(n1.IndexOf('%'));
                    dmgVal = float.Parse(dmg) * baseStat / 100f; //hull -10%
                }
                else dmgVal = float.Parse(n1); //hull -10
            }
            else
            {
                if (n1.EndsWith('%') && n2.EndsWith('%'))
                {
                    string dmg1 = n1.Remove(n1.IndexOf('%'));
                    string dmg2 = n2.Remove(n2.IndexOf('%'));
                    float num1 = float.Parse(dmg1), num2 = float.Parse(dmg2);
                    dmgVal = (float)(rnd.NextDouble() * (num2 - num1) + num1) * baseStat / 100f; //hull -10% -10% max
                    
                }
                else
                {
                    float num1 = float.Parse(n1), num2 = float.Parse(n2);
                    dmgVal = (float)(rnd.NextDouble() * (num2 - num1) + num1); //hull -10 -20
                }
            }

            return dmgVal;
        }


        public static GameData NewGame(string playerName, Story startStory)
        {
            GameData data = new GameData();
            GameData.rnd = new Random();
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
