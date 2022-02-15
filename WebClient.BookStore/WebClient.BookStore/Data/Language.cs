using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebClient.BookStore.Data;

namespace WebClient.BookStore.Data
{
    //[Table("Language")]
    public class Language
    {

        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Mapping Book table to Laguage table
        public ICollection<Books> Books { get; set; }
    }
}
