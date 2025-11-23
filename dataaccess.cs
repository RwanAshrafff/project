// DataAccess.cs (ADO.NET)
using System.Data;
using MySql.Data.MySqlClient;

public class DataAccess
{
    private readonly string _connString;
    public DataAccess(string connString) => _connString = connString;

    public async Task CreateUserAsync(string username, string email)
    {
        using var conn = new MySqlConnection(_connString);
        await conn.OpenAsync();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO Users (Username, Email) VALUES (@username, @email)";
        cmd.Parameters.Add("@username", MySqlDbType.VarChar, 100).Value = username;
        cmd.Parameters.Add("@email", MySqlDbType.VarChar, 100).Value = email;

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<(int UserID, string Username, string Email)?> GetUserByEmailAsync(string email)
    {
        using var conn = new MySqlConnection(_connString);
        await conn.OpenAsync();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT UserID, Username, Email FROM Users WHERE Email = @email LIMIT 1";
        cmd.Parameters.Add("@email", MySqlDbType.VarChar, 100).Value = email;

        using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
        if (await reader.ReadAsync())
        {
            return (
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2)
            );
        }
        return null;
    }
}
