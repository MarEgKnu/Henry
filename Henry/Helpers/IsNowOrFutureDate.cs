using System.ComponentModel.DataAnnotations;

namespace Henry.Helpers
{
    public class IsNowOrFutureDate : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value != null && (DateTime)value >= DateTime.Now;
        }
    }
}
