using Microsoft.AspNetCore.Mvc;
using Server.WebAPI.AOP;
using Server.WebAPI.Context;
using Server.WebAPI.Models;

namespace Server.WebAPI.Controllers;
[Route("/api/[controller]/[action]")]
[ApiController]
public class UsersController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    [EnableQueryWithMetadata]
    public IActionResult GetAll()
    {
        IQueryable<User> users = context.Users.AsQueryable();

        return Ok(users);
    }
}
