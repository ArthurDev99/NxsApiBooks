using System;
using System.ComponentModel.DataAnnotations;

namespace ApiBooks.Validators
{
    public class RangeValidatorYearAttribute: RangeAttribute
    {
        public RangeValidatorYearAttribute(int minimum) : base(minimum, DateTime.Now.Year)
        {
        }
    }
}
