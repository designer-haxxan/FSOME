using ContactApp.DataAccess.IRepository;
using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthentication _authentication;
        public LoginController(IAuthentication authentication)
        {
            _authentication = authentication;
        }
        public IActionResult Index()
        {
            //var userAuthenticate = Request.Cookies["userAuthenticate"];
            //if (!string.IsNullOrEmpty(userAuthenticate))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("userAuthenticate");
            Response.Cookies.Delete("userEmail");
            return RedirectToAction("Index", "Login");
        }


        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { success = false, message = "Email and password are Required" });
            }
            var result = await _authentication.LoginUser(data);
            if (result.IsSuccess)
            {
                return Ok(new { success = true, message = result.Message });
            }

            return Ok(new { success = false, message = result.Message });

        }


        [HttpPost]
        public async Task<IActionResult> RegUser([FromBody] Users data)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { success = false, message = "Email and password are Required" });
            }
            var result = await _authentication.RegisterUser(data);
            if (result.IsSuccess)
            {
                return Ok(new { success = true, message = result.Message });
            }

            return Ok(new { success = false, message = result.Message });

        }


    }
}
