using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.BookStore.Enums
{
    public enum BookTypeModelEnum
    {
        [Display(Name ="Adventure Book")]
        Adventure=1,
        [Display(Name = "Comic Book")]
        Comic,
        [Display(Name = "Drama Book")]
        Drama,
        [Display(Name = "Suspense Book")]
        Suspense,
        [Display(Name = "Thriller Book")]
        Thriller
    }
}
