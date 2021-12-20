using System;
using System.Collections.Generic;

namespace ApiBooks.Models.DbModels
{
    public partial class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        public decimal AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Hometown { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
