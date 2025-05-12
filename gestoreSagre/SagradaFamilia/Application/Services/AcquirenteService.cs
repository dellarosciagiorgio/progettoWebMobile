using Abstraction.Context;
using Application.Abstraction.Services;
using Application.Functions;
using Application.Mapper;
using Application.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Application.Services
{
    public class AcquirenteService : IAcquirenteService
    {
        private readonly IMyDbContext _context;
        private readonly IPasswordService _passwordService;
        //private readonly ILogger _logger;
        public AcquirenteService(
            //ILogger<AcquirenteService> logger,
            IPasswordService passwordService,
            IMyDbContext context)
        {
           // _logger = logger;
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<Acquirente> AddAcquirenteAsync(AddAcquirenteRequest request)
        {
            var entity = AcquirenteMapper.ToEntity(request);


            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == entity.User.Email);
            if (user != null)
            {
                throw new ArgumentException("Utente già esistente");
            }
            if(entity.User.Password.Length < 8)
            {
                throw new ArgumentException("Password troppo corta");
            }
            entity.User.Password = await _passwordService.HashPassword(entity.User.Password);
            await _context.Users.AddAsync(entity.User);
            entity.IdUser = entity.User.IdUser;

            await _context.Acquirenti.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Acquirente> EditAcquirenteAsync(EditAcquirenteRequest request)
        {
            var entity = AcquirenteMapper.ToEntity(request);
            var entry = _context.Entry<Acquirente>(entity);
            entry.Property(x => x.Nome).IsModified = true;
            entry.Property(x => x.Cognome).IsModified = true;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Acquirente>> GetAcquirentiAsync()
        {
            return await _context.Acquirenti
                .Include(a => a.BigliettiComprati)
                .ToListAsync();
        }
    }
}