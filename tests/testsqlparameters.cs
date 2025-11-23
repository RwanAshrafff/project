// Tests/TestSqlParameters.cs
using NUnit.Framework;
using System.Threading.Tasks;

[TestFixture]
public class TestSqlParameters
{
    [Test]
    public async Task ParameterizedQueryBlocksInjection()
    {
        var db = new DataAccess("Server=localhost;Database=safevault;Uid=user;Pwd=pass;");
        var injectedEmail = "victim@example.com' OR 1=1 --";
        // The parameterized query should treat this as data, not logic
        var user = await db.GetUserByEmailAsync(injectedEmail);
        // Assuming the real user does not exist:
        Assert.IsNull(user);
    }
}
