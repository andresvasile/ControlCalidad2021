using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CC2021Proyecto.Controllers
{
    public class GestionAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
