using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NoteBook.Data.EntityModels;
using NoteBook.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
              UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            IActionResult response = Unauthorized();
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                var token = await GenerateJwtToken(model.Email, appUser);
                appUser.ProfilePic = string.Format("{0}://{1}/UserImages/{2}", HttpContext.Request.Scheme, HttpContext.Request.Host,
            string.IsNullOrEmpty(appUser.ProfilePic) ? "NoImage.png" : appUser.ProfilePic);
                response = Ok(new { ReturnMessage = token.ToString(), IsSuccess = true, Data = appUser });

            }
            else
                response = NotFound(new { ReturnMessage = "Unauthorized", IsSuccess = false });
            return response;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { ReturnMessage = "Register Successfully", IsSuccess = true }); ;
            }

            return NotFound(new { ReturnMessage = result.Errors.FirstOrDefault().Description, IsSuccess = false });
        }

        [Authorize]
        [HttpGet]
        [Route("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            var userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _userManager.FindByIdAsync(userId);
            result.ProfilePic = string.Format("{0}://{1}/UserImages/{2}", HttpContext.Request.Scheme, HttpContext.Request.Host,
                string.IsNullOrEmpty(result.ProfilePic) ? "NoImage.png" : result.ProfilePic);

            return Ok(result);

        }
        [Authorize]
        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User model)
        {
            var userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            string uniqueFileName = null;
            var userDetails = await _userManager.FindByIdAsync(userId);
            if (!string.IsNullOrEmpty(model.ProfilePic) && !string.IsNullOrEmpty(model.FileName))
            {
                string picName = model.ProfilePic.Substring(model.ProfilePic.LastIndexOf('/') + 1);
                if (picName != "NoImage.png")
                {
                    var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\UserImages", picName);
                    if (System.IO.File.Exists(existingFilePath))
                    {
                        // If file found, delete it    
                        System.IO.File.Delete(existingFilePath);
                    }
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\UserImages", uniqueFileName);
                System.IO.File.WriteAllBytes(filePath, model.File);
            }
            else if (!string.IsNullOrEmpty(model.FileName))
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\UserImages", uniqueFileName);
                System.IO.File.WriteAllBytes(filePath, model.File);
            }
            else
            {
                string picName = model.ProfilePic.Substring(model.ProfilePic.LastIndexOf('/') + 1);
                uniqueFileName = picName;
            }
            userDetails.Address = model.Address;
            userDetails.FirstName = model.FirstName;
            userDetails.LastName = model.LastName;
            userDetails.PhoneNumber = model.PhoneNumber;
            userDetails.ProfilePic = uniqueFileName;

            var result = await _userManager.UpdateAsync(userDetails);
            userDetails.ProfilePic = string.Format("{0}://{1}/UserImages/{2}", HttpContext.Request.Scheme, HttpContext.Request.Host,
              string.IsNullOrEmpty(userDetails.ProfilePic) ? "NoImage.png" : userDetails.ProfilePic);
            if (result.Succeeded)
                return Ok(new { ReturnMessage = "Profile updated successully.", IsSuccess = true, Data = userDetails });

            return NotFound(new { ReturnMessage = result.Errors.FirstOrDefault().Description, IsSuccess = false });

        }

        private async Task<object> GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(120);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}