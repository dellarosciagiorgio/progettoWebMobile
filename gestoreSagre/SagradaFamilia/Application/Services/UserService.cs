using Abstraction.Context;
using Application.Abstraction.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMyDbContext _context;
        private readonly IPasswordService _passwordService;
        //private readonly ILogger _logger;
        public UserService(
            //ILogger<AcquirenteService> logger,
            IPasswordService passwordService,
            IMyDbContext context)
        {
            // _logger = logger;
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<int> GetAcqIdByUserIdAsync(int userId)
        {
            var acq = await _context.Acquirenti
                .Where(p => p.IdUser == userId)
                .FirstOrDefaultAsync();
            return acq?.Id ?? 0;
        }


        public async Task<int> GetOrgIdByUserIdAsync(int? userId)
        {
            var org = await _context.Organizzatori
                .Where(p => p.IdUser == userId)
                .FirstOrDefaultAsync();
            return org?.Id ?? 0;
        }
    }
}
