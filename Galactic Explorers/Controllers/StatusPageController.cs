using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GalacticExplorers.Models;

namespace GalacticExplorers.Controllers
{
    public class StatusPageController : Controller
    {
        [Route("/error/{code}")]
        public IActionResult Index(string code)
        {
            return View(new StatusCodeModel() { statusCode = code });
        }
    }
}