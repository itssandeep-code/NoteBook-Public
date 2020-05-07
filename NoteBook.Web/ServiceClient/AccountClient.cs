using NoteBook.Models;
using NoteBook.Web.Models;
using NoteBook.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteBook.Web.ServiceClient
{
    public class AccountClient : ApiClient
    {
        public AccountClient(Uri noteApiEndPoint) : base(noteApiEndPoint)
        {

        }
        public async Task<UserProfileViewModel> GetUserDetails(string token)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
               "Account/GetUserDetails"));
            return await GetAsync<UserProfileViewModel>(requestUrl, token);
        }
        public async Task<Message<UserProfileViewModel>> UpdateUser(UserProfileViewModel model,string token)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
              "Account/UpdateUser"));
            return await PostAsync<UserProfileViewModel>(requestUrl, model,token);
        }
        public async Task<Message<User>> Login(User model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Account/Login"));
            return await PostAsync<User>(requestUrl, model);
        }
        public async Task<Message<User>> Register(User model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Account/Register"));
            return await PostAsync<User>(requestUrl, model);
        }
    }
}
