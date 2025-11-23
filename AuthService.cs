// AuthService.cs
using BCrypt.Net;

public class AuthService
{
    private readonly DataAccess _db;
    public AuthService(DataAccess db) => _db = db;

    public string HashPassword(string password)
    {
        // Work factor 11–12 recommended for web apps (tune per environment)
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 11);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
// Minimal API setup for login (ASP.NET Core)
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/auth/login", async (LoginRequest req, DataAccess db) =>
{
    // Example: you have a UsersAuth table with PasswordHash
    // Always use parameterized queries to get the hash by username/email
    var user = await db.GetUserByEmailAsync(SafeInput.SanitizeEmail(req.Email));
    if (user is null) return Results.Unauthorized();

    // Fetch hash (example code, adapt to your schema)
    var passwordHash = await GetPasswordHashByEmailAsync(req.Email, db);
    var auth = new AuthService(db);
    if (!auth.VerifyPassword(req.Password, passwordHash))
        return Results.Unauthorized();

    // Issue auth cookie or JWT; here, cookie-based example:
    // In production, configure CookieAuth/JWT in Program.cs
    return Results.Ok(new { message = "Authenticated" });
});

record LoginRequest(string Email, string Password);

async Task<string> GetPasswordHashByEmailAsync(string email, DataAccess db)
{
    // Replace with a real query to UsersAuth table
    return "$2a$11$exampleexampleexampleexampleexampleexampleexampleexample"; // placeholder
}

app.Run();
