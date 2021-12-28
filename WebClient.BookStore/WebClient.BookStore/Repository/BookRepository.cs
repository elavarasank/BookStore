using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Models;

namespace WebClient.BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetAllBookById(int id)
        {
            return DataSource().Where(d => d.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBooks(string title, string authorName)
        {
            return DataSource().Where(d => d.Title.Contains(title) && d.Author.Contains(authorName)).ToList();
        }


        private List<BookModel> DataSource()
        {
            return new List<BookModel>(){
                new BookModel() { Id = 1, Title="MVC",       Author="Nagaraj"},
                new BookModel() { Id = 2, Title = "Java",    Author = "Praveen" },
                new BookModel() { Id = 3, Title = "Oracle",  Author = "Parthiban" },
                new BookModel() { Id = 4, Title = "Angular", Author = "Elavarasan" },
                new BookModel() { Id = 5, Title = "Python",  Author = "Saroja" }
            };
        }
    }
}
