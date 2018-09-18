using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WowCharComparerWebApp.Controllers.CharacterControllers
{
    public class CharacterComparatorController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }

        public IActionResult GetFirstCharacterName(string firstNickToCompare, string secondNickToCompare)
        {
            string firstTextBoxPassedInput = firstNickToCompare;
            string secondTextBoxPassedInput = secondNickToCompare;
            return Ok();
        }
    }
}