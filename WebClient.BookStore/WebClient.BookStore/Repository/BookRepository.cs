using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Data;
using WebClient.BookStore.Models;

namespace WebClient.BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IConfiguration _configuration;

        public BookRepository(BookStoreContext context,IConfiguration configuration )
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var allBooks = await _context.Books.ToListAsync();
            var books = new List<BookModel>();
            if (allBooks?.Any() == true)
            {
                foreach (var book in allBooks)
                {
                    books.Add(
                        new BookModel()
                        {
                            Id = book.Id,
                            Title = book.Title,
                            Category = book.Category,
                            Author = book.Author,
                            Description = book.Description,
                            PageCount = book.PageCount,
                            LanguageId = book.LanguageId,
                            CoverPhotoUrl = book.CoverPhotoUrl
                            //Language=book.Language.Name
                        }
                    );
                }
            }
            return books;
        }

        public async Task<List<BookModel>> GetTopBooksAsync(int bookCount)
        {
            //var book = await _context.Books.FindAsync(id);
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Id = book.Id,
                    Category = book.Category,
                    Description = book.Description,
                    LanguageId = book.LanguageId,
                    PageCount = book.PageCount,
                    Title = book.Title,
                    CoverPhotoUrl = book.CoverPhotoUrl
                }).Take(bookCount).ToListAsync();
        }
        public async Task<BookModel> GetBookById(int id)
        {
            //var book = await _context.Books.FindAsync(id);
            return await _context.Books.Where(d => d.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Id = book.Id,
                    Category = book.Category,
                    Description = book.Description,
                    LanguageId = book.LanguageId,
                    PageCount = book.PageCount,
                    Title = book.Title,
                    CoverPhotoUrl = book.CoverPhotoUrl,
                    BookPdfUrl = book.BookPdfUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList()
                }).FirstOrDefaultAsync();
        }
        public List<BookModel> SearchBooks(string title, string authorName)
        {
            //return DataSource().Where(d => d.Title.Contains(title) && d.Author.Contains(authorName)).ToList();
            return null;
        }
        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>(){
        //        new BookModel() { Id = 1, Title="MVC",       Author="Nagaraj"       ,Category="Action",Language="English",PageCount=1056 ,Description="MVC means Model View Controller which used to handle code separtion"},
        //        new BookModel() { Id = 2, Title = "Java",    Author = "Praveen"     ,Category="Suspense",Language="French",PageCount=564  ,Description="Open Source High level programming language which supports more than 8 Million Devices"},
        //        new BookModel() { Id = 3, Title = "Oracle",  Author = "Parthiban"   ,Category="Thriller",Language="Spanish",PageCount=890  ,Description="Relational database to handle large amount of data"},
        //        new BookModel() { Id = 4, Title = "Angular", Author = "Elavarasan"  ,Category="Adventure",Language="Arabic",PageCount=980  ,Description="Client side scripting framework which written on top of javascript"},
        //        new BookModel() { Id = 5, Title = "Python",  Author = "Saroja"      ,Category="Discovery",Language="Hindi",PageCount=450  ,Description="High level language which is used to write the codes for Machine language & IOTs"},
        //        new BookModel() { Id = 6, Title = "Nodejs",  Author = "Saroja"      ,Category="Fantacy",Language="Tamil",PageCount=782  ,Description="Server Side Scripting Language"}
        //    };
        //}

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Title = model.Title,
                PageCount = model.PageCount.HasValue ? model.PageCount.Value : 0,
                Description = model.Description,
                LanguageId = model.LanguageId,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CoverPhotoUrl = model.CoverPhotoUrl,
                BookPdfUrl = model.BookPdfUrl
            };
            newBook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(
                    new BookGallery()
                    {
                        Name = file.Name,
                        URL = file.URL
                    }
                );
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;

        }


        public string ApplicationTitle() { ;
            return _configuration["AppName"];
        }

    }
}
