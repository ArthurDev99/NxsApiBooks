using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBooks.Models.DataModels
{
    public class BasicResponse
    {
        public BasicResponse() 
        {

        }
        public BasicResponse(int code = 0,string message ="")
        {
            this.code = code;
            this.message = message;
        }

        // BASIC STRUCT FOR OPERATIONS RESPONSE LIKE INSERT, DELETE AND UPDATE.
        public int code { get; set; }
        public string message { get; set; }
    }
}
