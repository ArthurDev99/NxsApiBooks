using System.Collections.Generic;

namespace ApiBooks.Models.DataModels.Books
{
    public class ListResponseBook
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<BookItemListDM> data { get; set; }
    }
}
