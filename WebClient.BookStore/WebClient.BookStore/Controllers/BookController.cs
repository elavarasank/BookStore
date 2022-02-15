using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Models;
using WebClient.BookStore.Repository;

namespace WebClient.BookStore.Controllers
{
    public class BookController : Controller
    {
        

        //Constructor
        public readonly IBookRepository _bookRepository = null;
        public readonly ILanguageRepository _languageRepository = null;
        public readonly IWebHostEnvironment _webHostEnvironment = null;
        public BookController(IBookRepository bookRepository,ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ViewResult> GetAllBooks()
        {

            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }
        [Route("book-details/{id}", Name = "bookDetailsRoute")]
        public async Task<ViewResult> GetBookById(int id)
        {

            var dateString1 = DateTime.Now.ToString("yyyyMMddHH");
            //dynamic data = new ExpandoObject();
            //data.book = await _bookRepository.GetBookById(id);
            //data.developer = "elavarasan.k";
            var book = await _bookRepository.GetBookById(id);
            return View(book);
        }

        [Authorize]
        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {

            //ViewBag.Languages = new SelectList(await _languageRepository.GetLanguages(),"LanguageId","Name");

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
            //return View();
        }
        [HttpPost]
        //if we use the same name for submit the form there is no need to set in form tag.
        //if we use different name we have to set asp-action in form tag
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            bookModel.Language = "English";
            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {

                    bookModel.CoverPhotoUrl=await UploadImage("books/photos/",bookModel.CoverPhoto);
                }

                bookModel.Gallery = new List<GalleryModel>();
                if (bookModel.GalleryFiles != null)
                {
                    foreach(var file in bookModel.GalleryFiles) {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage("books/gallery/", file)
                        };
                        bookModel.Gallery.Add(gallery);
                        
                    }
                    
                }
                if (bookModel.BookPdf != null)
                {

                    bookModel.BookPdfUrl = await UploadImage("books/pdf/", bookModel.BookPdf);
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });

                }
            }
            ViewBag.IsSuccess = false;
            ViewBag.BookId = 0;
            //ViewBag.Languages = new SelectList(await _languageRepository.GetLanguages(), "LanguageId", "Name");
            return View();
        }

        private async Task<string> UploadImage(string folderPath,IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() +"_"+ file.FileName;
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(fullPath, FileMode.Create));



            return "/" + folderPath;
        }

    }

}

