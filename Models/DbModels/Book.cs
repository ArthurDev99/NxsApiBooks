using System;
using System.Collections.Generic;

namespace ApiBooks.Models.DbModels
{
    public partial class Book
    {
        public decimal BookId { get; set; }
        public string Title { get; set; }
        public decimal PublicationYear { get; set; }
        public string Gender { get; set; }
        public decimal NumberPages { get; set; }
        public decimal AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
