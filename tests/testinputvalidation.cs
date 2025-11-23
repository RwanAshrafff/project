// Tests/TestInputValidation.cs
using NUnit.Framework;

[TestFixture]
public class TestInputValidation
{
    [Test]
    public void TestForSQLInjectionSanitization()
    {
        var malicious = "Robert'); DROP TABLE Users;--";
        var sanitized = SafeInput.SanitizeUsername(malicious);
        Assert.IsFalse(sanitized.Contains("'"));
        Assert.IsFalse(sanitized.Contains(";"));
        Assert.IsFalse(sanitized.Contains("--"));
    }

    [Test]
    public void TestForXSSHtmlEscape()
    {
        var malicious = "<script>alert('xss')</script>";
        var escaped = SafeInput.HtmlEscape(malicious);
        Assert.IsFalse(escaped.Contains("<script>"));
        Assert.IsTrue(escaped.Contains("&lt;script&gt;"));
    }

    [Test]
    public void TestEmailSanitizationAllowsValidChars()
    {
        var email = "user.name+tag@example-domain.com";
        var sanitized = SafeInput.SanitizeEmail(email);
        Assert.AreEqual(email, sanitized);
    }
}
