using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class TownRepo(ApplicationDbContext _context) : IGenaricRepository<Town>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var townDb = await _context.Towns.FindAsync(id);
            if (townDb == null) return NotFound();

            _context.Towns.Remove(townDb);
            await Commit();
            return Success();
        }

        public async Task<List<Town>> GetAll()
        {
            return await _context.Towns.AsNoTracking()
                .Include(c=>c.City)
                .ToListAsync();
        }

        public async Task<Town> GetById(int id)
        {
            var townDb = await _context.Towns.FindAsync(id);
            if (townDb == null) return null!;
            return townDb;
        }

        public async Task<GeneralResponse> Insert(Town item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Town already added");

            _context.Towns.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Town item)
        {
            var townDb = await _context.Towns.FindAsync(item.Id);
            if (townDb == null) return NotFound();

            townDb.Name = item.Name;
            townDb.CityId = item.CityId;
            await Commit();
            return Success();
        }

        //Helpers
        private static GeneralResponse NotFound() => new(false, "Sorry Town not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        public async Task Commit() => await _context.SaveChangesAsync();
        public async Task<bool> CheckName(string name)
        {
            var item = await _context.Towns.FirstOrDefaultAsync(d => d.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
