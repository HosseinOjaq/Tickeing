namespace Ticketing.Common.Extensions;

public static class StringExtension
{
    public static bool HasValue(this string? value, bool ignoreWhiteSpace = true)
    {
        return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
    }
}