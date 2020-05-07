using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NoteBook.Web.ServiceClient;
using NoteBook.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoteBook.Web.Controllers
{
    public class AccountController : Controller
    {
        AccountClient apiClient;
        private readonly IConfiguration configuration;

        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
            apiClient = new AccountClient(new Uri(configuration["BaseApiPath"]));
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await apiClient.Login(new NoteBook.Models.User
                {
                    Email = model.Email,
                    Password = model.Password
                });

                if (result.IsSuccess)
                {
                    var access_token = result.ReturnMessage;
                    HttpContext.Session.SetString("Access_Token", access_token);

                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    IList<Claim> claim = identity.Claims.ToList();
                    //var userName = claim[0].Value;

                    var claims = new List<Claim>{
                                new Claim("UserName", string.Format("{0} {1}",result.Data.FirstName,result.Data.LastName)),
                                new Claim("UserPic",result.Data.ProfilePic)
                    };
                    var principal = new ClaimsPrincipal();
                    principal.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError("LoginViewModel", "Invalid Login Attempt");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return PartialView("_registerUser", model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await apiClient.Register(new NoteBook.Models.User
                {
                    Email = model.Email,
                    Password = model.Password
                });
                if (response.IsSuccess == false)
                {
                    foreach (var error in response.ReturnMessage.Split(','))
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                else
                    return Content("OK");

            }
            return PartialView("_registerUser", model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            var userModel = await apiClient.GetUserDetails(HttpContext.Session.GetString("Access_Token"));
            return View(userModel);
        }
        [HttpPost]
        public async Task<IActionResult> UserProfile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProfileImage!=null)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.ProfileImage.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        model.File = fileBytes;
                        model.FileName = model.ProfileImage.FileName;
                        model.ProfileImage = null;
                        // act on the Base64 data
                    }
                }

                var userModel = await apiClient.UpdateUser(model, HttpContext.Session.GetString("Access_Token"));
                ModelState.Clear();
                return View(userModel.Data);
            }
            return View(model);
        }
    }
}
