using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.BookStore.Areas.Admin.Controllers
{
    [Area("admin")]
    //[Route("admin/[controller]/[action]")]
    [Route("admin")]
    public class HomeController : Controller
    {

        //http://localhost:65267/admin
        // GET: HomeController
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        //http://localhost:65267/admin/home/details/1
        // GET: HomeController/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int id)
        {
            return View(id);
        }

        // GET: HomeController/Create
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        [Route("edit/id")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/id")]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        [Route("delete/id")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/id")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
