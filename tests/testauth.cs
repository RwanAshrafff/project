// Tests/TestAuth.cs
using NUnit.Framework;

[TestFixture]
public class TestAuth
{
    [Test]
    public void PasswordHashAndVerify()
    {
        var auth = new AuthService(new DataAccess("conn"));
        var hash = auth.HashPassword("S3cureP@ss!");
        Assert.IsTrue(auth.VerifyPassword("S3cureP@ss!", hash));
        Assert.IsFalse(auth.VerifyPassword("WrongPassword", hash));
    }
}
