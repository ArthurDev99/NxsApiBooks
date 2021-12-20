using ApiBooks.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBooks.Models.DataModels.Books
{
    public class BookItemListDM
    {
        public decimal BookId { get; set; }

        public string Title { get; set; }

        public decimal PublicationYear { get; set; }

        public string Gender { get; set; }

        public decimal NumberPages { get; set; }
       
        public decimal AuthorId { get; set; }

        public string AuthorName { get; set; }
    }
}
