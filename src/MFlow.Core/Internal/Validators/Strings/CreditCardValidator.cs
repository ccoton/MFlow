using MFlow.Core.Validation.Validators.Strings;
namespace MFlow.Core.Internal.Validators.Strings
{
    class CreditCardValidator : ICreditCardValidator
    {
        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            input = input.Replace("-", "").Replace(" ", "");

            var DELTAS = new int[] { 0, 1, 2, 3, 4, -4, -3, -2, -1, 0 };
            var checksum = 0;
            var chars = input.ToCharArray();
            
            for (int i = chars.Length - 1; i > -1; i--)
            {
                var j = ((int)chars [i]) - 48;
                checksum += j;
                if (((i - chars.Length) % 2) == 0)
                    checksum += DELTAS [j];
            }

            return ((checksum % 10) == 0);
        }
    }
}
