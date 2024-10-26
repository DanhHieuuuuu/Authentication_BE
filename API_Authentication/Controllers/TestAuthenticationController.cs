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
        private readonly IHttpContextAccessor _contextAccessor;
        public TestAuthenticationController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        [Authorize]
        [AuthorizationFilter("Admin")]
        [HttpGet("/admin")]
        public IActionResult HelloAdmin()
        {
            try
            {
                string user = CommonUtils.GetCurrentUserId(_contextAccessor);
                return Ok(user);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
