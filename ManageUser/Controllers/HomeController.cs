using ManageUser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;

namespace ManageUser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<UserModel> users = new List<UserModel>();
            if (HttpContext.Session.GetString("User") != null)
            {
                var getUsers = JsonConvert.DeserializeObject<List<UserModel>>(HttpContext.Session.GetString("User").ToString());
                users.AddRange(getUsers);
                return View(users);
            }
            else
            {
                return View(users);

            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserModel manageUser)
        {
            if (manageUser != null)
            {
                //TempData["User"] = null;
                if (HttpContext.Session.GetString("User") != null)
                {
                    var getUsers = JsonConvert.DeserializeObject<List<UserModel>>(HttpContext.Session.GetString("User").ToString());
                    List<UserModel> users = new List<UserModel>();
                    manageUser.Id = getUsers.Count() + 1;
                    users.Add(manageUser);
                    users.AddRange(getUsers);
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(users));
                }
                else
                {
                    List<UserModel> users = new List<UserModel>();
                    manageUser.Id = 1;
                    users.Add(manageUser);
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(users));
                }
                return RedirectToAction("Index");

            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var getUsers = JsonConvert.DeserializeObject<List<UserModel>>(HttpContext.Session.GetString("User").ToString());
            var user = getUsers.Where(x => x.Id == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(UserModel userDetail)
        {
            var getUsers = JsonConvert.DeserializeObject<List<UserModel>>(HttpContext.Session.GetString("User").ToString());
            foreach (var user in getUsers.Where(u =>u.Id  == userDetail.Id))
            {
                user.Name= userDetail.Name;
                user.Email= userDetail.Email;
                user.Address= userDetail.Address;
            }
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(getUsers));
           return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var getUsers = JsonConvert.DeserializeObject<List<UserModel>>(HttpContext.Session.GetString("User").ToString());
            var user = getUsers.Where(x => x.Id == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public IActionResult Delete(UserModel userDetail)
        {
            var getUsers = JsonConvert.DeserializeObject<List<UserModel>>(HttpContext.Session.GetString("User").ToString());
            var userToRemove = getUsers.Single(x => x.Id == userDetail.Id);
            getUsers.Remove(userToRemove);
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(getUsers));
            return RedirectToAction("Index");
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