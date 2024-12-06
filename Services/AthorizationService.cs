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
        private readonly IPasswordEncripter _passordEncripter = new PasswordEncripter();
        
        public AuthResults Auth(string username, string password, out User users)
        {
            // Busca al usuario en la base de datos por su nombre de usuario
            users = db.Users.Where(x => x.Username.Equals(username)).FirstOrDefault();

            // Si el usuario no existe, retorna el resultado correspondiente
            if (users == null)
                return AuthResults.NotExists;

            // Encripta la contraseña proporcionada usando las claves del usuario
            password = _passordEncripter.Encript(password, new List<byte[]>
            {
                users.HashKey, // Usa 'users' para acceder a las propiedades del usuario
                users.HashIV
            });

            // Compara la contraseña encriptada con la almacenada en la base de datos
            if (password != users.Password)
                return AuthResults.PasswordNotMatch;

            // Si todo está correcto, retorna éxito
            return AuthResults.Success;
        }


    }
}