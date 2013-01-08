namespace MFlow.Core.Internal.Validators
{
    class CreditCardValidator : IValidator<string>
    {
        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            input = input.Replace("-", "").Replace(" ", "");

            int[] DELTAS = new int[] { 0, 1, 2, 3, 4, -4, -3, -2, -1, 0 };
            int checksum = 0;
            char[] chars = input.ToCharArray();
            for (int i = chars.Length - 1; i > -1; i--)
            {
                int j = ((int)chars[i]) - 48;
                checksum += j;
                if (((i - chars.Length) % 2) == 0)
                    checksum += DELTAS[j];
            }

            return ((checksum % 10) == 0);
        }
    }
}
