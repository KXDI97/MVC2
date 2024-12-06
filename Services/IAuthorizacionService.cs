using MVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2.Services
{
    public interface IAuthorizationService
    {
        AuthResults Auth(string user, string password, out User users);
    }

    public enum AuthResults
    {
        Success,
        PasswordNotMatch,
        NotExists,
        Error
    }
}
