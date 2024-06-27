using Newtonsoft.Json;
using Sachin_452.WebHelper;
using Sachin_452_Models.DBContext;
using Sachin_452_Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sachin_452.Controllers
{
    public class LoginController : Controller
    {
        Sachin_452Entities _DBContext = new Sachin_452Entities();
        // GET: Login
        /// <summary>
        /// Login User - Admin / User
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string content = JsonConvert.SerializeObject(login);
                    string response = await WebHelpers.HttpRequestResponce("api/LoginAPI/LoginAccount", content);

                    UserModel loginModel = JsonConvert.DeserializeObject<UserModel>(response);
                    var cookie = new HttpCookie("jwt", loginModel.Token)
                    {
                        HttpOnly = true,
                        // Secure = true, // Uncomment this line if your application is running over HTTPS
                    };
                    Response.Cookies.Add(cookie);

                    var usr = _DBContext.Users.Where(u => u.EmailID == login.EmailID && u.Password == login.Password).FirstOrDefault();
                    if (loginModel != null && loginModel.Role == "Admin")
                    {
                        SessionHelper.SessionHelper.EmailID = loginModel.EmailID;
                        SessionHelper.SessionHelper.UserName = loginModel.USerName;
                        SessionHelper.SessionHelper.UserID = loginModel.UserID;
                        SessionHelper.SessionHelper.Role = loginModel.Role;
                        Session["username"] = loginModel.USerName;
                        TempData["success"] = "Login Successfully";
                        return RedirectToAction("AdminHomepage", "Admin");
                    }
                    else if (loginModel != null && loginModel.Role == "Users")
                    {
                        SessionHelper.SessionHelper.EmailID = loginModel.EmailID;
                        SessionHelper.SessionHelper.UserName = loginModel.USerName;
                        SessionHelper.SessionHelper.UserID = usr.UserID;
                        Session["username"] = loginModel.USerName;
                        SessionHelper.SessionHelper.Role = loginModel.Role;
                        TempData["success"] = "Login Successfully";
                        return RedirectToAction("UserDashboard", "User");
                    }
                    else
                    {
                        TempData["error"] = "something went wrong!";
                        return RedirectToAction("Login", "Login");
                    }
                }
                else
                {
                    TempData["error"] = "something went wrong!";
                    return View(login);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Register user
        /// </summary>
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string url = "api/LoginAPI/RegisterUser";

                    string content = JsonConvert.SerializeObject(userModel);
                    string response = await WebHelpers.HttpRequestResponce(url, content);
                    Users user = JsonConvert.DeserializeObject<Users>(response);

                    if (user.UserID > 0)
                    {
                        TempData["success"] = "Register Successfully";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["error"] = "Email Already exists!";
                        return View(userModel);
                    }
                }
                else
                {
                    TempData["error"] = "something went wrong";
                    return View(userModel);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login" , "Login");
        }


    }
}