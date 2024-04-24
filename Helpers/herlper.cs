using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TurnosLaM.Data;
using TurnosLaM.Models;
using TurnosLaM.Helpers;
using Newtonsoft.Json;




namespace TurnosLaM.Helpers
{
    public  class TheHelpercito
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TheHelpercito(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

       public static string GenerateUserName(string FirstName, string LastName, string Document)
        {
            string cleanFirstName = FirstName.Trim().ToLower();
            string cleanLastName = LastName.Trim().ToLower();
            string cleanDocument = Document.Trim();
            string username = $"{cleanFirstName[0]}{cleanLastName}{cleanDocument.Substring(cleanDocument.Length - 2)}";
            return username;
        }
        public static string Encrypt(string encrypt)
        {
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(encrypt);
            string result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string Decrypt(string decrypt)
        {
            byte[] decryted = Convert.FromBase64String(decrypt);
            string result =  System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        public void SetObjInSession<T>(string nameToSave, T obj){ 
            _httpContextAccessor.HttpContext.Session.SetString(nameToSave, JsonConvert.SerializeObject(obj));
        }

        public T getObjInSession<T>(string name){ 
           return JsonConvert.DeserializeObject<T>(_httpContextAccessor.HttpContext.Session.GetString(name));
        }
    }
}


