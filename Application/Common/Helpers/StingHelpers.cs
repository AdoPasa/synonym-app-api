namespace Application.Common.Helpers
{
    public static class StingHelpers
    {
        public static string NormalizeValue(this string? value) 
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return value.Trim().ToUpper();
        }
    }
}
