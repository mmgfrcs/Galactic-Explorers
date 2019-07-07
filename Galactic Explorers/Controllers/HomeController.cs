using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GalacticExplorers.Models;
using Newtonsoft.Json;
using GalacticExplorers.Entities;

namespace GalacticExplorers.Controllers
{
    public class HomeController : Controller
    {
        public static GameData loadedData = null;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            if (Request.Cookies.TryGetValue("GE-Session", out string val))
            {
                return View(GameManager.Instance.GetGame(val));
            }
            else return View(null);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(string name)
        {
            string id = GameManager.Instance.StartNewGame(name);
            Response.Cookies.Append("GE-Session", id, new Microsoft.AspNetCore.Http.CookieOptions()
            {
                MaxAge = TimeSpan.FromSeconds(30)
                
            });
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
                    Response.Cookies.Append("GE-Session", sessionId, new Microsoft.AspNetCore.Http.CookieOptions() { MaxAge = TimeSpan.FromSeconds(30) });
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
