using Abstraction.Context;
using Application.Abstraction.Services;
using Application.Functions;
using Application.Mapper;
using Application.Models.Requests;
using Application.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IMyDbContext _context;
        private readonly JwtOptions _jwtOptions;
        //private readonly ILogger _logger;
        public PasswordService(
            //ILogger<AcquirenteService> logger,
            IOptions<JwtOptions> options,
            IMyDbContext context)
        {
            // _logger = logger;
            _context = context;
            _jwtOptions = options.Value;
        }

        public async Task<string> HashPassword(string password)
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(GetPepperedPassword(password)));
        }

        public async Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.Verify(GetPepperedPassword(password), hashedPassword));
        }

        private string GetPepperedPassword(string password)
        {
            return $"{password}|{_jwtOptions.Key}";
        }
    }
}
