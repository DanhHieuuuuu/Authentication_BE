using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Authentication.Dtos;
namespace API_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuthenticationController : ControllerBase
    {
        [Authorize]
        [AuthorizationFilter("Admin")]
        [HttpGet("/admin")]
        public IActionResult HelloAdmin()
        {
            return Ok("this account admin!!!");
        }

        //[Authorize(Roles = "Customer")]
        [Authorize]
        [AuthorizationFilter("Customer")]
        [HttpGet("/customer")]
        public IActionResult HelloCustomer()
        {
            return Ok("this account customer!!!");
        }
    }
}
