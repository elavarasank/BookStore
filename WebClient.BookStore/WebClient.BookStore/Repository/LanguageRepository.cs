using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Data;
using WebClient.BookStore.Models;

namespace WebClient.BookStore.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreContext _context = null;
        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<LanguageModel>> GetLanguages()
        {
            return await _context.Language.Select(
                x => new LanguageModel()
                {
                    Name = x.Name,
                    LanguageId = x.LanguageId,
                    Description = x.Description
                }
            ).ToListAsync();

        }
    }
}
