using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.BookStore.Areas.Financial.Controllers
{
    [Area("financial")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //http://localhost:65267/financial/dashboard/graph/elavarasan
        public IActionResult Graph()
        {
            return View();
        }
    }
}
