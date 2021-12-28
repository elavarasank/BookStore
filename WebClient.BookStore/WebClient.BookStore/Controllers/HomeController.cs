using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.BookStore.Controllers
{
    public class HomeController : Controller
    {
        //http://localhost:65267/
        public ViewResult Index()
        {
            return View();
        }

        //http://localhost:65267/home/AboutUs
        public ViewResult AboutUs()
        {
            return View();
        }

        //http://localhost:65267/home/contactus
        public ViewResult ContactUs()
        {
            return View();
        }

        //http://localhost:65267/home/mypage
        public ViewResult MyPage()
        {
            //full path
            // return View("TempView/MyPage.cshtml");
            // or
            //return View("~/TempView/MyPage.cshtml");

            //relative path
            return View("../../TempView/MyPage");
        }
    }
}
