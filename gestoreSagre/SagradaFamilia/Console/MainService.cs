using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Unicam.Libreria.Console
{
    public class MainService
    {
        private MyDbContext _context;
        public MainService(MyDbContext context)
        {
            _context = context;
        }

        private void EagerLoading()
        {
            var libriConAutori = _context.Libri
                          .AsNoTracking()
                          .Include(i => i.Autore)
                          .ToList();
        }

        private async Task LazyLoadingAsync()
        {
            var libri = await _context.Libri
                .AsNoTracking()
                .ToListAsync();
            foreach (var libro in libri)
            {
                System.Console.WriteLine(libro.Autore.Nome);
            }
        }

        private async Task ExplicitLoadingAsync()
        {
            var libri = await _context.Libri
                .AsNoTracking()
                .ToListAsync();
            foreach (var libro in libri)
            {
                _context.Entry(libro).Reference(x => x.Autore).Load();
            }

        }
        public async Task ExecuteAsync()
        {
            await GetAcquirenteAsync();
            await LazyLoadingAsync();
            await ExplicitLoadingAsync();

            await AggiungiLibroAsync();
            await _context.SaveChangesAsync();
        }

        private async Task GetAcquirenteAsync()
        {
            var libri = await _context.Acquirenti
                .AsNoTracking()
                .ToListAsync();
        }

        private void EditLibroGiaTracciato(Libro libro)
        {
            libro.Titolo = "TItolo Modificato";
        }

        private void EditDiAlcuneProprieta()
        {
            var libroDaModificare = new Libro();
            libroDaModificare.Id = 3;
            libroDaModificare.Isbn = "NUOVOISBN";
            libroDaModificare.Descrizione = "DESC";

            var entry = _context.Entry(libroDaModificare);
            entry.Property(p => p.Isbn).IsModified = true;
            entry.Property(p => p.Descrizione).IsModified = true;
        }

        private void DeleteLibro()
        {
            var libroToDelete = new Libro();
            libroToDelete.Id = 4;

            _context.Entry(libroToDelete).State = EntityState.Deleted;
        }

        private async Task AggiungiLibroAsync()
        {
            var libro = new Libro();
            libro.IdAutore = 1;
            libro.IdCategoria = 1;
            libro.Titolo = "Aggiunta libro";
            libro.Descrizione = "Descrizione test";
            libro.Isbn = "123456";

            await _context.Libri.AddAsync(libro);
            //Il comando di add sul DbSet è analogo ad effettuare l'operazione qui sotto
            //_context.Entry(libro).State = EntityState.Added;
        }


    }
}
