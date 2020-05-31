using NoteBook.Web.Models;
using NoteBook.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteBook.Web.ServiceClient
{
    public class ContactClient : ApiClient
    {
        public ContactClient(Uri noteApiEndPoint) : base(noteApiEndPoint)
        {

        }
        public async Task<List<Tuple<string, string>>> GetTitles(string token)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Contact/GetTitles"));
            return await GetAsync<List<Tuple<string, string>>>(requestUrl, token);
        }
        public async Task<List<Tuple<string, string>>> GetContactTypes(string token)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Contact/GetContactTypes"));
            return await GetAsync<List<Tuple<string, string>>>(requestUrl, token);
        }
        public async Task<List<ContactViewModel>> GetContacts(string token)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Contact/GetContacts"));
            return await GetAsync<List<ContactViewModel>>(requestUrl, token);
        }
        public async Task<ContactViewModel> GetContact(string token, int Id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Contact/GetContact"), Convert.ToString(string.Format("?Id={0}", Id)));
            return await GetAsync<ContactViewModel>(requestUrl, token);
        }
        public async Task<Message<ContactViewModel>> SaveContact(ContactViewModel model, string token)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Contact/SaveContact"));
            return await PostAsync<ContactViewModel>(requestUrl, model, token);
        }
        public async Task<Message<ContactViewModel>> DeleteContact(string token, ContactViewModel model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Contact/DeleteNote"));
            return await PostAsync<ContactViewModel>(requestUrl, model, token);
        }
    }
}
