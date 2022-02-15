using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WebClient.BookStore.Helpers;
using Microsoft.AspNetCore.Http;

namespace WebClient.BookStore.Models
{
    public class BookModel
    {
        

        public int Id { get; set; }

        //[Required(ErrorMessage ="Plese type title for the book.")]
        [StringLength(100,MinimumLength =5)]

        [MyCustomValidationAttribute("book",ErrorMessage ="This Custom error the title should containg abc")]
        public string Title { get; set; }

      
        [Required]
        public string Author { get; set; }

        [StringLength(500,MinimumLength =10)]
        public string Description { get; set; }

        public string Category { get; set; }

        public int LanguageId { get; set; }

        public string Language { get; set; }

        [Required(ErrorMessage ="Please enter the pagecount.")]
        [Display(Name ="Total Page Count")]
        public int? PageCount { get; set; }

        [Display(Name ="Choose cover photo for your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }

        public string CoverPhotoUrl { get; set; }

        [Display(Name = "Choose the gallary photos for your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        [Display(Name = "Upload your book in pdf format")]
        [Required]
        public IFormFile BookPdf { get; set; }


        public string BookPdfUrl { get; set; }
    }
}
