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
        public enum GameOverType
        {
            Generic, Stranded
        }

        public static GameManager Instance;

        Dictionary<string, GameData> games = new Dictionary<string, GameData>();

        List<Story> stories = new List<Story>();

        public GameManager()
        {
            foreach (var path in Directory.GetFiles("Stories", "*.*", SearchOption.AllDirectories))
            {
                Story s = JsonConvert.DeserializeObject<Story>(File.ReadAllText(path));
                stories.Add(s);
            }
        }

        public Story GetGameOverStory()
        {
            return stories.Find(x => x.id == "GAME-OVER");
        }

        public Story GetNextStory(Story currStory, int opt)
        {
            return stories.Find(x => x.id == currStory.choices[0].gotoId);
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
            else throw new ArgumentException("Session ID not found");
        }
    }
}
