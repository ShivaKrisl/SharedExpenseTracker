using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpenseTracker.Core.ValidationHelpers
{
    public static class ValidateModel
    {
        public static string? Errors = string.Empty;
        public static bool IsModelValid(Object? obj)
        {
            if(obj == null) {
                return false;
            }

            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            if (!isValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var validationResult in validationResults)
                {
                    sb.AppendLine(validationResult.ErrorMessage);
                }
                Errors = sb.ToString();
            }
            return true;
        }
    }
}
