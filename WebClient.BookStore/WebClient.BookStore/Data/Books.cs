using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Data;

namespace WebClient.BookStore.Data
{   
    //[Table("Books")]
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        
        public int PageCount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public int LanguageId { get; set; }
        //Mapping Laguage table to Book table
        public Language Language { get; set; }

        public string CoverPhotoUrl { get; set; }

        public ICollection<BookGallery> bookGallery { get; set; }

        public string BookPdfUrl { get; set; }
    }
}
