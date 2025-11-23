// SafeInput.cs
using System.Text.RegularExpressions;
using System.Web;

public static class SafeInput
{
    // Basic whitelist validation: letters, digits, underscore, dash, dot, space
    public static string SanitizeUsername(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var trimmed = input.Trim();
        var cleaned = Regex.Replace(trimmed, @"[^a-zA-Z0-9_\-\. ]", "");
        return cleaned;
    }

    public static string SanitizeEmail(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var trimmed = input.Trim();
        // Allow email-safe characters only
        var cleaned = Regex.Replace(trimmed, @"[^a-zA-Z0-9@\.\-_+]", "");
        return cleaned;
    }

    // Server-side HTML escaping to mitigate XSS on rendering
    public static string HtmlEscape(string? input)
    {
        return HttpUtility.HtmlEncode(input ?? string.Empty);
    }
}
