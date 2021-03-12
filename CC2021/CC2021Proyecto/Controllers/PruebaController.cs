using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CC2021Proyecto.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CC2021Proyecto.Controllers
{
    public class PruebaController : Controller
    {
        public IActionResult Index()
        {
            var model = new CountryViewModel();
            model.Country = "CA";
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var msg = model.Country + " selected";
                
            }
            return View(model);
            // If we got this far, something failed; redisplay form.

        }
    }
}
