using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Models;
using System.Dynamic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebClient.BookStore.Repository;
using WebClient.BookStore.Service;
using Microsoft.AspNetCore.Authorization;

namespace WebClient.BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration = null;
        private readonly NewBookAlertMessageConfig _newBookIOption = null;
        private readonly NewBookAlertMessageConfig _externalBookIOption = null;
        private readonly IMessageRepository _messageRepository = null;

        private readonly IEmailService _emailService = null;

        private readonly IUserService _userService;
        public HomeController(
            IOptionsSnapshot<NewBookAlertMessageConfig> newBookIOption,
            IConfiguration configuration,
            IMessageRepository messageRepository,
            IUserService userService,
            IEmailService emailService)
        {
            _configuration = configuration;
            _newBookIOption = newBookIOption.Get("NewBook");
            _externalBookIOption = newBookIOption.Get("ExternalBook");
            _messageRepository = messageRepository;
            _userService = userService;
            _emailService = emailService;
        }
        //http://localhost:65267/
        public async Task<ViewResult> Index()
        {

            //UserEmailOptions options = new UserEmailOptions()
            //{
                //ToMails = new List<string>() { "ela.ars@gmail.com" },
                //PlaceHolders = new List<KeyValuePair<string, string>>()
                //{
                    //new KeyValuePair<string,string>("{{UserName}}","Elavarsan Kannayiram")
                //}
            //};
            //await _emailService.SendTestEmail(options);



            //Getting login details from service
            var userId = _userService.GetUserId();
            var isAuthenticated = _userService.IsAuthenticated();

            ViewBag.Title = "Welcome to Bookstore";
            ViewBag.SampleModel = new BookModel()
            {
                Id = 7,
                Author = "Rabindranath Tagore",
                Description = "National Anthem"
            };
            dynamic data = new ExpandoObject();
            data.Name = "Sachin";
            data.Sports = "Cricket";
            data.Country = "India";
            ViewBag.Data = data;
            return View();
        }

        //http://localhost:65267/home/AboutUs
        public ViewResult AboutUs()
        {
            //Using IOptions Snapshot with named Config
            bool newBookShowMessage = _newBookIOption.ShowMessage;
            string newBookAlertMessage = _newBookIOption.AlertMessage;

            bool externalBookShowMessage = _externalBookIOption.ShowMessage;
            string externalBookAlertMessage = _externalBookIOption.AlertMessage;

            //Using IOption Monitor method configuration
            var getName = _messageRepository.GetName();

            //Using IOption method configuration

            bool showMsg = _newBookIOption.ShowMessage;
            string alertMsg = _newBookIOption.AlertMessage;


            var key1 = _configuration["Keys:Key1"];
            var key2 = _configuration["Keys:Key2"];
            var key3 = _configuration["Keys:Key3:Name"];

            var ShowMessage = _configuration.GetValue<bool>("NewBookAlertMessage:ShowMessage");
            var AlertMessage = _configuration.GetValue<string>("NewBookAlertMessage:AlertMessage");

            var newBook = _configuration.GetSection("NewBookAlertMessage");
            var sMessage = newBook.GetValue<bool>("ShowMessage");
            var aMessage = newBook.GetValue<string>("AlertMessage");

            //or
            var showM = _configuration.GetSection("NewBookAlertMessage").GetValue<bool>("ShowMessage");

            //Binding configuration

            /*var newBookAlert = new NewBookAlertMessageConfig();
            _configuration.Bind("NewBookAlertMessage", newBookAlert);
            bool sm = newBookAlert.ShowMessage;
            string am = newBookAlert.AlertMessage;*/





            ViewBag.Title = "Welcome to About Us";
            ViewData["myData"] = new BookModel() { Id = 10, Title = "Sample Book", Description = "Sample Description for View Data", Author = "Vijay" };
            return View();
        }

        //http://localhost:65267/home/contactus


        //multiple roles can be assigned as like below
        //[Authorize(Roles = "Admin,User,Manager")]
        [Authorize(Roles ="Admin")]
        public ViewResult ContactUs()
        {
            ViewData["Name"] = "Elavarasan Kannayiram";

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

        //Specific data
        [ViewData]
        public string viewDataString { get; set; }

        //Complex data
        [ViewData]
        public BookModel book { get; set; }

        [ViewData]
        public string BrowserTitle { get; set; }

        //http://localhost:65267/home/branches
        public ViewResult Branches()
        {
            viewDataString = "This string is comming from action as viewdataattribute";
            book = new BookModel()
            {
                Id = 33,
                Title = "My Sample book",
                Description = "This is created for sample book to demonstrate viewdataattribute complex data"
            };
            BrowserTitle = "This title action to layout";
            return View();
        }
    }
}
