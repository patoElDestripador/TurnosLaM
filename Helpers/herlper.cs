using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TurnosLaM.Data;
using TurnosLaM.Models;
using TurnosLaM.Helpers;
using Newtonsoft.Json;
using BCryptNet = BCrypt.Net.BCrypt;



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
        // 1.MÉTODO: Encripta la contraseña:
        public static string EncryptPassword(string password)
        {
            // Se inicializa una variable de la password encriptada con el método (HashPassword):
            string EncryptedPassword = BCryptNet.HashPassword(password, BCryptNet.GenerateSalt());
            // Se retorna la password encriptada:
            return EncryptedPassword;
        }

        // 2.MÉTODO: Compara la contraseña enviada por el usuario en el Login con la contraseña encriptada guardada en la base de datos:
        public static bool VerifyPassword(string dataBasePassword, string passwordProvided)
        {
            // Se iniciaiza una variable para confirmar si la contraseña proporcionada coincide con la guardada en la base de datos con el método (Verify):
            bool passwordMatch = BCryptNet.Verify(passwordProvided, dataBasePassword);
            // Se retorna la variable confirmando si la contraseña coincidió:
            return passwordMatch;
        }

        public void SetObjInSession<T>(string nameToSave, T obj){ 
            _httpContextAccessor.HttpContext.Session.SetString(nameToSave, JsonConvert.SerializeObject(obj));
        }

        public T getObjInSession<T>(string name){ 
            return JsonConvert.DeserializeObject<T>(_httpContextAccessor.HttpContext.Session.GetString(name));
        }
    }
}


