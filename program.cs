// Program.cs (ASP.NET Core with roles)
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", opts => { opts.LoginPath = "/login"; });

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/admin/dashboard", [Authorize(Policy = "AdminOnly")] () =>
{
    return Results.Ok(new { message = "Welcome, admin." });
});

app.Run();
// Example during login: add role claims when issuing cookie
// (inside your login handler after verifying password)
var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, user.Value.Username),
    new Claim(ClaimTypes.Role, "admin") // assign based on DB lookup
};
var identity = new ClaimsIdentity(claims, "Cookies");
var principal = new ClaimsPrincipal(identity);
// In MVC: await HttpContext.SignInAsync("Cookies", principal);
// In Minimal API (need HttpContext):
// await context.SignInAsync("Cookies", principal);
