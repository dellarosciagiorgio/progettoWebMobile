using Abstraction.Context;
using Application.Abstraction.Services;
using Application.Mapper;
using Application.Models.Request;
using Application.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMyDbContext _context;
        //private readonly ILogger _logger;
        public LoginService(
            //ILogger<AcquirenteService> logger,
            IMyDbContext context)
        {
            // _logger = logger;
            _context = context;
        }

        public async Task<UserGenerico> GetUserInformation(User ut)
        {
            if(ut == null)
            {
                throw new ArgumentNullException("Utente non valido");
            }

            switch (ut.Ruolo)
            {
                case Ruolo.Acquirente:
                    return await _context.Acquirenti
                        .Where(x => x.IdUser == ut.IdUser)
                        .FirstOrDefaultAsync();
                case Ruolo.Organizzatore:
                    return await _context.Organizzatori
                        .Where(x => x.IdUser == ut.IdUser)
                        .FirstOrDefaultAsync();
                case Ruolo.Admin:
                    return await _context.Admins
                        .Where(x => x.IdUser == ut.IdUser)
                        .FirstOrDefaultAsync();
                default:
                    throw new ArgumentException("Ruolo non valido");
            }
        }


        public async Task<User> GetUtenteByEmailAsync(string email)
        {
            return await _context.Users
                 .Where(x => x.Email == email)
                 .FirstOrDefaultAsync();
        }
        public async Task<User> LoginAsync(LoginRequest request)
        {
            User user = await GetUtenteByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("Utente non trovato");
            }
            if (user.Password != request.Password)
            {
                throw new Exception("Password errata");
            }

            //Login corretto

            return user;
        }
    }
}
