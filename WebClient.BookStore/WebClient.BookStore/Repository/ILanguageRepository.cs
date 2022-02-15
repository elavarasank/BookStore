using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.BookStore.Models;

namespace WebClient.BookStore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}