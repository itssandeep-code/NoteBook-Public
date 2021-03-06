﻿using NoteBook.Web.Models;
using NoteBook.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteBook.Web.ServiceClient
{
    public class NoteClient : ApiClient
    {
        public NoteClient(Uri noteApiEndPoint) : base(noteApiEndPoint)
        {

        }
        public async Task<List<NoteViewModel>> GetNotes(string token)
        {            
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Note/GetNotes"));
            return await GetAsync<List<NoteViewModel>>(requestUrl,token);
        }
        public async Task<NoteViewModel> GetNote(string token,int Id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Note/GetNote"),Convert.ToString(string.Format("?Id={0}",Id)));
            return await GetAsync<NoteViewModel>(requestUrl, token);
        }
        public async Task<Message<NoteViewModel>> SaveNote(NoteViewModel model, string token)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Note/SaveNote"));
            return await PostAsync<NoteViewModel>(requestUrl, model,token);
        }
        public async Task<Message<NoteViewModel>> DeleteNote( string token, NoteViewModel model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Note/DeleteNote"));
            return await PostAsync<NoteViewModel>(requestUrl, model,token);
        }
    }
}
