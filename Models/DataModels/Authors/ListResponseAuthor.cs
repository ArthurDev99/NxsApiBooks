using System.Collections.Generic;

namespace ApiBooks.Models.DataModels.Authors
{
    public class ListResponseAuthor
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<AuthorDM> data { get; set; }
    }
}
