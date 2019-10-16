using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using Microsoft.AspNetCore.Http; ///////////////////////added for session
using Microsoft.AspNetCore.Identity; ///////////password hashing
using Microsoft.EntityFrameworkCore; 				//////////entity import

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////Register/////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        [HttpPost("create")]
        public IActionResult CreateUser(User newUser)
        {
            // We can take the User object created from a form submission
            // And pass this object to the .Add() method
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                } else {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    dbContext.Add(newUser);
                    // OR dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetString("Email", newUser.Email);
                    return RedirectToAction("Home");
                }
            } 
            return View("Index");
        }
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////Login/////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        [HttpPost("login")]
        public IActionResult Login(string Email, string Password)
        {
            // If a User exists with provided email
            if(dbContext.Users.Any(u => u.Email == Email))
            {
                User logger = dbContext.Users.FirstOrDefault(User => User.Email == Email);
                if(Password=="" || Password == null){
                    ViewBag.fail = "Incorrect email or password.";
                    return View("Index");
                }
                // Initialize hasher object
                var hasher = new PasswordHasher<User>();
                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(logger, logger.Password, Password);
                if(result != 0){
                    HttpContext.Session.SetString("Email", Email);
                    return RedirectToAction("Home");
                }
            }
            ViewBag.fail = "Incorrect email or password.";
            return View("Index");
        }
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////Logout/////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("Email", "");
            return RedirectToAction("Index");
        }
        [HttpGet("home")]
        public IActionResult Home()
        {
            if(HttpContext.Session.GetString("Email") == "" || HttpContext.Session.GetString("Email")==null){
                ViewBag.fail = "Incorrect email or password.";
                return RedirectToAction("Index");
            }
            IEnumerable<Activitee> allActivities = dbContext.Activitees.Include(a=>a.Users).ThenInclude(b=>b.User).ToList();
            ViewBag.Activities = allActivities;
            foreach(var i in allActivities){
                int x = 1;
                if(i.Users != null){
                    foreach(var s in i.Users){
                        x++;
                    }
                }
                i.GuestCount = x;
            }
            User cUser = dbContext.Users.FirstOrDefault(a=>a.Email == HttpContext.Session.GetString("Email"));
            ViewBag.User = cUser;
            return View("home",allActivities);
        }
        [HttpGet("new")]
        public IActionResult New()
        {
            if(HttpContext.Session.GetString("Email") == "" || HttpContext.Session.GetString("Email")==null){
                ViewBag.fail = "Incorrect email or password.";
                return RedirectToAction("Index");
            }
            User cUser = dbContext.Users.FirstOrDefault(a=>a.Email == HttpContext.Session.GetString("Email"));
            ViewBag.User = cUser;
            return View();
        }
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////Create Activity/////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        [HttpPost("CreateActivity")]
        public IActionResult CreateActivity(Activitee newActivity)
        {
            if(HttpContext.Session.GetString("Email") == "" || HttpContext.Session.GetString("Email")==null){
                ViewBag.fail = "Incorrect email or password.";
                return RedirectToAction("Index");
            }
            if(newActivity.ActivityDate <= DateTime.Now){
                ModelState.AddModelError("ActivityDate", "Activity must be in the future.");
            }else if(ModelState.IsValid){
                User cUser = dbContext.Users.FirstOrDefault(a=>a.Email == HttpContext.Session.GetString("Email"));
                newActivity.Coordinator = cUser.FName;
                dbContext.Add(newActivity);
                Associations newAss = new Associations();
                newAss.UserId = cUser.UserId;
                newAss.ActivityId = newActivity.ActivityId;
                dbContext.Add(newAss);
                dbContext.SaveChanges();
                return RedirectToAction("home");
            }
            return View("new");
        }
        [HttpGet("activity/{Id}")]
        public IActionResult SActivity(int Id)
        {
            if(HttpContext.Session.GetString("Email") == "" || HttpContext.Session.GetString("Email")==null){
                ViewBag.fail = "Incorrect email or password.";
                return RedirectToAction("Index");
            }
            List<User> gs = new List<User>();
            Activitee wed = dbContext.Activitees.Include(a=>a.Users).ThenInclude(b=>b.User).FirstOrDefault(y=>y.ActivityId == Id);
            List<Activitee> cAct = new List<Activitee>();
            cAct.Add(wed);
            ViewBag.Activity = wed;
            if(wed.Users != null){
                foreach(var s in wed.Users){
                    gs.Add(s.User);
                }
            }
            User cUser = dbContext.Users.FirstOrDefault(a=>a.Email == HttpContext.Session.GetString("Email"));
            ViewBag.User = cUser;
            ViewBag.Guests = gs;
            return View("Activity", cAct);
        }
        [HttpGet("join/{ActivityId}")]
        public IActionResult Join(int ActivityId)
        {
            if(HttpContext.Session.GetString("Email") == "" || HttpContext.Session.GetString("Email")==null){
                ViewBag.fail = "Incorrect email or password.";
                return RedirectToAction("Index");
            }
            User cUser = dbContext.Users.FirstOrDefault(a=>a.Email == HttpContext.Session.GetString("Email"));
            Associations newAss = new Associations();
            newAss.UserId = cUser.UserId;
            newAss.ActivityId = ActivityId;
            dbContext.Add(newAss);
            dbContext.SaveChanges();
            return RedirectToAction("SActivity", new {Id=ActivityId});
        }
        [HttpGet("leave/{ActivityId}/{UserId}")]
        public IActionResult Leave(int ActivityId, int UserId)
        {
            if(HttpContext.Session.GetString("Email") == "" || HttpContext.Session.GetString("Email")==null){
                ViewBag.fail = "Incorrect email or password.";
                return RedirectToAction("Index");
            }
            Associations act = dbContext.Associations.FirstOrDefault(y=>y.ActivityId == ActivityId && y.UserId == UserId);
            dbContext.Remove(act);
            dbContext.SaveChanges();
            return RedirectToAction("SActivity", new {Id=ActivityId});
        }
        [HttpGet("delete/{ActivityId}")]
        public IActionResult Delete(int ActivityId)
        {
            if(HttpContext.Session.GetString("Email") == "" || HttpContext.Session.GetString("Email")==null){
                ViewBag.fail = "Incorrect email or password.";
                return RedirectToAction("Index");
            }
            Activitee act = dbContext.Activitees.FirstOrDefault(y=>y.ActivityId == ActivityId);
            dbContext.Remove(act);
            dbContext.SaveChanges();
            return RedirectToAction("home");
        }
    }
}
