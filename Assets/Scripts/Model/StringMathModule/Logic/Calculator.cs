using StringMath.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringMath
{
    public class Calculator
    {
        private const string ALLOWED_CHARACTERS = "+ 0123456789";

        public Result Calculate(string input)
        {
            if (Validate(input) == ResultType.Error) return Result.Error;

            var numbers = GetNumbers(input);
            if(numbers == null || numbers.Length == 0) return Result.Error;

            return new Result(ResultType.Success, 
                numbers.Sum(s => int.TryParse(s, out var n) ? n : 0).ToString());
        }

        private ResultType Validate(string input)
        {
            if (string.IsNullOrEmpty(input)) return ResultType.Error;

            foreach (char c in input)
            {
                if (!ALLOWED_CHARACTERS.Contains(c.ToString()))
                    return ResultType.Error;
            }

            return ResultType.Success;
        }

        private string[] GetNumbers(string input)
        {
            string trimmed = Regex.Replace(
                Regex.Replace(input, @"\s+", ""),
                @"\++",
                "+");

            return trimmed.Split('+');
        }
    }
}
