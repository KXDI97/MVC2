using MVC2.Context;
using MVC2.Models;
using MVC2.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC2.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        
        private readonly WebMVC2Context db = new WebMVC2Context();
        private readonly IPasswordEncripter _passwordEncripter = new PasswordEncripter();

        public AuthResults Auth(string username, string password, out User users)
        {
            // Busca al usuario en la base de datos por su nombre de usuario
            users = db.Users.FirstOrDefault(x => x.Username.Equals(username));

            // Si el usuario no existe, retorna el resultado correspondiente
            if (users == null)
            {
                return AuthResults.NotExists;
            }

            // Desencriptar la contraseña usando PasswordEncripter
            string decryptedPassword;

            try
            {
                decryptedPassword = _passwordEncripter.Decrypt(users.Password, new List<byte[]> { users.HashKey, users.HashIV });
            }
            catch
            {
                return AuthResults.Error; // Error al desencriptar
            }

            // Comparar la contraseña desencriptada con la ingresada
            if (decryptedPassword == password)
            {
                return AuthResults.Success;
            }

            // Si las contraseñas no coinciden
            return AuthResults.PasswordNotMatch;
        }
    }


    
}