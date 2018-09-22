using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WowCharComparerLib.APIConnection;
using WowCharComparerLib.Enums;
using WowCharComparerLib.Enums.BlizzardAPIFields;
using WowCharComparerLib.Models;
using WowCharComparerWebApp.Models;

namespace WowCharComparerWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}