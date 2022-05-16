using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace QKNWZ1.WpfApp
{
    class CodepointValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;

            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Input was null/empty/whitespace-only.");
            }

            Regex regex = new("[^A-Z0-9]");
            return regex.IsMatch(input)
                ? new ValidationResult(false, "Input contains illegal character(s).")
                : new ValidationResult(true, string.Empty);
        }
    }
}
