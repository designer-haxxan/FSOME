using ContactApp.DataAccess.Data;
using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace ContactApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            string email = Request.Cookies["userEmail"].ToString() ?? "";
            var result = await _db.tblUsers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (result == null)
            {
                Response.Cookies.Delete("userAuthenticate");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (result.isAdmin == false && result.isActive == true)
                {
                    string key = string.Empty;
                    string key2 = string.Empty;
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(1);
                    key = result.Email.Replace("@", "_").Replace(".", "_") + "todaylimit";
                    Response.Cookies.Delete(key);
                    Response.Cookies.Append(key, result.ExportTodayLimit.ToString(), option);
                    key2 = result.Email.Replace("@", "_").Replace(".", "_") + "perlimit";
                    Response.Cookies.Delete(key2);
                    Response.Cookies.Append(key2, result.PerExportLimit.ToString(), option);
                    Response.Cookies.Delete("userNote");
                    return RedirectToAction("Index", "ContactFilter");

                }
                else if (result.isAdmin == true && result.isActive == true)
                {
                    int totalContact = await _db.tblContacts.CountAsync();
                    int totalUser = await _db.tblUsers.CountAsync();
                    ViewBag.Totalcontact = totalContact;
                    ViewBag.Totaluser = totalUser;
                    Response.Cookies.Delete("userNote");
                    return View();
                }
                else if (result.isAdmin == false && result.isActive == false)
                {
                    string key = string.Empty;
                    string key2 = string.Empty;
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(1);
                    key = result.Email.Replace("@", "_").Replace(".", "_") + "todaylimit";
                    Response.Cookies.Delete(key);
                    Response.Cookies.Append(key, result.ExportTodayLimit.ToString(), option);
                    key2 = result.Email.Replace("@", "_").Replace(".", "_") + "perlimit";
                    Response.Cookies.Delete(key2);
                    Response.Cookies.Append(key2, result.PerExportLimit.ToString(), option);
                    Response.Cookies.Delete("userNote");
                    Response.Cookies.Append("userNote", "Please upgrade your plan to export more contacts", option);
                    return RedirectToAction("Index", "ContactFilter");
                }
                else
                {
                    return RedirectToAction("Index", "Users");
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
