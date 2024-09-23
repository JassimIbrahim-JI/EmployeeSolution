

using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class CityRepo(ApplicationDbContext _context) : IGenaricRepository<City>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var cityDb = await _context.Citys.FindAsync(id);
            if (cityDb == null) return NotFound();

            _context.Citys.Remove(cityDb);
            await Commit();
            return Success();
        }

        public async Task<List<City>> GetAll()
        {
            return await _context.Citys.AsNoTracking().Include(c=>c.Country).ToListAsync();
        }

        public async Task<City> GetById(int id)
        {
            var cityDb = await _context.Citys.FindAsync(id);
            if (cityDb == null) return null!;
            return cityDb;
        }

        public async Task<GeneralResponse> Insert(City item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "City already added");

            _context.Citys.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(City item)
        {
            var cityDb = await _context.Citys.FindAsync(item.Id);
            if (cityDb == null) return NotFound();

            cityDb.Name = item.Name;
            cityDb.CountryId = item.CountryId;
            await Commit();
            return Success();
        }

        //Helpers
        private static GeneralResponse NotFound() => new(false, "Sorry City not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        public async Task Commit() => await _context.SaveChangesAsync();
        public async Task<bool> CheckName(string name)
        {
            var item = await _context.Citys.FirstOrDefaultAsync(d => d.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
