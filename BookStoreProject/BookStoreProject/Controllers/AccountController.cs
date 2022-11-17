using BookStoreProject.Model;
using BookStoreProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    [Route("accounts")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUp model, [FromServices] IAccountRepository repository)
        {
            var response = await repository.SignUpAsync(model);

            if (response.Succeeded)
            {
                return Ok(response.Succeeded);
            }
            return Unauthorized();
        }
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn model, [FromServices] IAccountRepository repository)
        {
            var response = await repository.SignIn(model);

            if (string.IsNullOrEmpty(response))
            {
                return Unauthorized();
            }
            return Ok(response);
        }
    }
}
