using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GalacticExplorers.Models;
using Newtonsoft.Json;
using GalacticExplorers.Entities;
using Microsoft.AspNetCore.Http;

namespace GalacticExplorers.Controllers
{
    public class HomeController : Controller
    {
        public static GameData loadedData = null;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            string session = HttpContext.Session.GetString("GE-Session");
            if (session != null)
            {
                return View(GameManager.Instance.GetGame(session));
            }
            else return View(null);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(string name)
        {
            string id = GameManager.Instance.StartNewGame(name);

            HttpContext.Session.SetString("GE-Session", id);
            return LocalRedirect("/");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(string name, string sessionId)
        {
            GameData data = GameManager.Instance.GetGame(sessionId);
            if (data != null)
            {
                if (data.Player.Name == name)
                {
                    HttpContext.Session.SetString("GE-Session", data.SessionId);
                    //return LocalRedirect("/");
                }
            }
            
            return LocalRedirect("/");
        }

        [HttpPost]
        public IActionResult Damage(string data)
        {
            GameData gameData = GameManager.Instance.GetGame(data);
            float dmg = gameData.Player.CurrentHull / 2f;
            gameData.Player.Damage(dmg);
            return Ok(dmg / gameData.Player.MaximumHull);
        }

        [HttpPost, Route("{controller}/options")]
        public IActionResult OnPostOptions(int opt)
        {
            string session = HttpContext.Session.GetString("GE-Session");
            if (session != null)
            {
                GameData gameData = GameManager.Instance.GetGame(session);
                Console.WriteLine($"{session}: {gameData.Story.id}");
                gameData.ChangeStory(opt);
                Console.WriteLine($"{session}: {gameData.Story.id}");
                GameManager.Instance.SaveGameData(session, gameData);
                return StatusCode(204);
            }
            else return Unauthorized();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
