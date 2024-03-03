using ContactApp.DataAccess.Data;
using ContactApp.DataAccess.IRepository;
using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersEntry _users;
        private readonly ApplicationDbContext _db;
        public UsersController(IUsersEntry users,ApplicationDbContext db)
        {
            _users = users;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAlluser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Users users)
        {
            if (ModelState.IsValid)
            {
                var result = await _users.Save(users);
                if (result.IsSuccess)
                {
                    TempData["success"] = "Successfully Saved Record";
                    ModelState.Clear();
                }
                else
                {
                    TempData["error"] = result.Message;
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] CustomSearchUsers? UserData)
        {
            List<Users> result = new List<Users>();
            IEnumerable<Users> query =await _users.GetAll(UserData);
            foreach(var item in query)
            {
                item.FirstName = item.FirstName;
                item.LastName = item.LastName;
                if(item.isAdmin==false && item.isActive == false)
                {
                    item.Email = $"{item.Email} <span style='color:red;'>(Free Trial)<span>";
                }
                else
                {
                    item.Email=item.Email;
                }
                item.isActive = item.isActive;
                item.isAdmin = item.isAdmin;
                item.password = item.password;
                item.ExportTodayLimit = item.ExportTodayLimit;
                item.PerExportLimit = item.PerExportLimit;
                result.Add(item);
            }
            return Json(new {data=result});
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _users.Delete(id);
            if (result.IsSuccess)
            {
                return Json(new { issuccess = true, message = result.Message });
            }

            return Json(new { issuccess = false, message = result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result=await _users.Get(id);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Update(Users userdata)
        {
            if (ModelState.IsValid)
            {
                var result = await _users.Update(userdata);
                if (result.IsSuccess)
                {
                    TempData["success"] = "Successfully Update Record";
                    return RedirectToAction("ViewAlluser");
                }
                else
                {
                    TempData["error"] = result.Message;
                }
            }
            return View();

        }


    }
}
