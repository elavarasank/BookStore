using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Models;
using WebClient.BookStore.Repository;

namespace WebClient.BookStore.Controllers
{
    public class BookController : Controller
    {
        /*
        
        // http://localhost:65267/book/getallbooks
        public string GetAllBooks() { 
            return "All books";
        }

        // http://localhost:65267/book/GetBook/2
        public string GetBook(int id) {
            return $"Book {id}";
        }

        //http://localhost:65267/book/searchbook?bookName=Indiaglitz&authorName=elavarasan
        public string SearchBook(string bookName,string authorName) {
            return $"Book with the name:{bookName} and the author is: {authorName}";
        }

        */

        //Constructor
        public readonly BookRepository _bookRepository=null;
        public BookController() {
            _bookRepository = new BookRepository();
        }

        /*
        //http://localhost:65267/book/getallbooks
        public List<BookModel> GetAllBooks() {
            return _bookRepository.GetAllBooks();
        }

        //http://localhost:65267/book/getAllbookByid?id=3
        public BookModel GetAllBookById(int id)
        {
            return _bookRepository.GetAllBookById(id);
        }

        //http://localhost:65267/book/SearchBooks?title=Java&authorName=P
        public List<BookModel> SearchBooks(string title,string authorName)
        {
            return _bookRepository.SearchBooks(title, authorName);
        }*/

        public ViewResult GetAllBooks()
        {
            var data=_bookRepository.GetAllBooks();
            return View();
        }

        

    }
}
