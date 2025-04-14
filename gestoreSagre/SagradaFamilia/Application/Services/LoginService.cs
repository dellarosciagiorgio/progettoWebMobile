using Abstraction.Context;
using Application.Abstraction.Services;
using Application.Functions;
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
        private readonly IPasswordService _passwordService;
        //private readonly ILogger _logger;
        public LoginService(
            //ILogger<AcquirenteService> logger,
            IPasswordService passwordService,
            IMyDbContext context)
        {
            // _logger = logger;
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<UserGenerico?> GetUserInformation(User ut)
        {
            ArgumentNullException.ThrowIfNull(ut, nameof(ut));

            return ut.Ruolo switch
            {
                Ruolo.Acquirente => await _context.Acquirenti
                    .Where(x => x.IdUser == ut.IdUser)
                    .FirstOrDefaultAsync(),
                Ruolo.Organizzatore => await _context.Organizzatori
                    .Where(x => x.IdUser == ut.IdUser)
                    .FirstOrDefaultAsync(),
                Ruolo.Admin => await _context.Admins
                    .Where(x => x.IdUser == ut.IdUser)
                    .FirstOrDefaultAsync(),
                _ => throw new ArgumentException("Ruolo non valido", nameof(ut.Ruolo))
            };
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

            if (!_passwordService.VerifyPassword(request.Password, user.Password).Result) 
            {
                throw new Exception("Password errata");
            }

            //Login corretto

            return user;
        }
    }
}
