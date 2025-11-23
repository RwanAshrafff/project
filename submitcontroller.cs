// SubmitController.cs (ASP.NET Core MVC or Minimal API handler)
[ApiController]
[Route("[controller]")]
public class SubmitController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromForm] string username, [FromForm] string email)
    {
        var safeUsername = SafeInput.SanitizeUsername(username);
        var safeEmail = SafeInput.SanitizeEmail(email);

        if (string.IsNullOrWhiteSpace(safeUsername) || string.IsNullOrWhiteSpace(safeEmail))
            return BadRequest("Invalid input.");

        // Store using parameterized SQL shown below
        return Ok(new
        {
            username = SafeInput.HtmlEscape(safeUsername),
            email = SafeInput.HtmlEscape(safeEmail)
        });
    }
}
