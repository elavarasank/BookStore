using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.BookStore.Models;

namespace WebClient.BookStore.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel model);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task<List<BookModel>> GetTopBooksAsync(int bookCount);
        List<BookModel> SearchBooks(string title, string authorName);

        string ApplicationTitle();
    }
}