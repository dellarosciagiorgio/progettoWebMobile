using Application.Models.Request;
using Application.Models.Requests;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services
{
    public interface ILoginService
    {
        Task<UserGenerico?> GetUserInformation(User ut);
        Task<User> GetUtenteByEmailAsync(string email);
        Task<User> LoginAsync(LoginRequest request);


    }
}
