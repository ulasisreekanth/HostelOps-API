using Microsoft.AspNetCore.Mvc;

namespace HostelOps_API.Controllers;

[ApiController]
[Route("[controller]")]
public class userApi : ControllerBase
{
    public int userID { get; set; }

    [HttpGet("GetUsers")]
    public IActionResult getUsers()
    {
        return Ok(userID);
    }
}
