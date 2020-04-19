using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NoteBook.Web.ServiceClient;
using NoteBook.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
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

                    var claims = new List<Claim>
            {

        new Claim("UserName", model.Email)
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

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(configuration["BaseApiPath"]);
                //    client.DefaultRequestHeaders.Accept.Clear();
                 //   client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                   // client.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("Access_Token"));

                    // create the URL string.
                    string url = string.Format("" + configuration["BaseApiPath"] + "/Account/Register");

                    var json = JsonConvert.SerializeObject(model);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); //
                    // make the request
                    HttpResponseMessage response1 = await client.PostAsync(url,stringContent);

                    // parse the response and return the data.
                    string jsonString = await response1.Content.ReadAsStringAsync();
                    object responseData = JsonConvert.DeserializeObject(jsonString);
                    var userName = (dynamic)responseData;

                }


                var response =   await apiClient.Register(new NoteBook.Models.User
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
                    return RedirectToAction("Login", "Account");

            }
            return PartialView("_registerUser", model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
