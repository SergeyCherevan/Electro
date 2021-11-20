using Electro.Models;
using Electro.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Electro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _manager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly ElectroDbContext _context;
        public AccountController
            (UserManager<User> userMgr, SignInManager<User> signinMgr, IWebHostEnvironment env, ElectroDbContext context)
        {
            _manager = userMgr;
            _signInManager = signinMgr;
            _env = env;
            _context = context;
        }



        
        /*let data = { email: "serhii.cherevan@nure.ua", fullname: "Sergey Cherevan", userName: "Ageris", password: "12345", confirmpassword: "12345" }
        let resp = await fetch("/api/account/registration", {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(data)
        } )*/
        //await resp.json()
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            /*if (User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not anonymous");
            }*/

            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    UserName = model.UserName,
                };

                var result = await _manager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);

                    user.PasswordHash = null;
                    return Ok(user);
                }
                else
                {
                    if (_env.IsDevelopment())
                    {
                        return StatusCode(500, result.Errors);
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
            }

            return BadRequest(ModelState);
        }

        /*let data = { UserName: "Ageris", password: "12345" }
        let resp = await fetch("/api/account/login", {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(data)
        } )*/
        //await resp.json()
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            /*if (User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not anonymous");
            }*/

            if (ModelState.IsValid)
            {
                User user = await _manager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult result
                        = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        user.PasswordHash = null;
                        return Ok(user);
                    }
                }

                return BadRequest("Wrong login or password");
            }

            return BadRequest(ModelState);
        }

        /*let resp = await fetch("/api/account/logout", {
          method: 'GET',
          headers: { 'Content-Type': 'application/json' }
        } )*/
        //await resp.text()
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok("You are logged out");
        }

        public async Task<IActionResult> CurrentUser()
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized();
            }

            User user = await _manager.FindByNameAsync(User.Identity.Name);
            user.PasswordHash = null;

            return Ok(user);
        }

        [Authorize]
        [HttpGet("{nickname}")]
        public async Task<IActionResult> GetUser(string nickname)
        {
            User user = await _manager.FindByNameAsync(nickname);
            user.PasswordHash = null;

            return Ok(user);
        }
    }
}
