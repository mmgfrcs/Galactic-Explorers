using GalacticExplorers.Entities;
using GalacticExplorers.JSONUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers
{
    public class GameManager
    {
        public static GameManager Instance;

        Dictionary<string, GameData> games = new Dictionary<string, GameData>();

        List<Story> stories = new List<Story>();

        public GameManager()
        {
            foreach(var path in Directory.GetFiles("Stories"))
            {
                Story s = JsonConvert.DeserializeObject<Story>(File.ReadAllText(path));
                stories.Add(s);
            }
        }

        public string StartNewGame(string playername)
        {
            GameData data = GameData.NewGame(playername, stories.Find(x => x.id == "GAME-START-LOST"));

            games.Add(data.SessionId, data);
            return data.SessionId;
        }

        public GameData GetGame(string sessionId)
        {
            if (games.TryGetValue(sessionId, out GameData val))
            {
                return val;
            }
            else return null;
        }

        public void SaveGameData(string sessionId, GameData data)
        {
            if (games.ContainsKey(sessionId))
            {
                games[sessionId] = data;
            }
        }
    }
}
