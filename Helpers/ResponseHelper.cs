using ApiBooks.Models.DataModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace ApiBooks.Helpers
{
    public class ResponseHelper
    {
        public static BasicResponse basicResponse(int code = 0, string message = "")
        {
            return new BasicResponse(code, message);
        }
        public static string getFirstErrorFromModelState(ModelStateDictionary model_state) 
        {
            // GET LIST ERRORS
            var errors = model_state.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

            // GET FIRST ERROR MESSAGE FROM LIST ERRORS
            string first_error_message = errors[0].FirstOrDefault().ErrorMessage;

            return first_error_message;
        }
        public static string basicMessageForExceptions(Exception error) 
        {
            if (error != null && error.InnerException != null) return $"{error.Message} ({error.InnerException.Message})";
            if (error != null) return error.Message;

            return "";
        }
    }
}
