
using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class CountryRepo(ApplicationDbContext _context) : IGenaricRepository<Country>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var countryDb = await _context.Country.FindAsync(id);
            if (countryDb == null) return NotFound();

            _context.Country.Remove(countryDb);
            await Commit();
            return Success();
        }

        public async Task<List<Country>> GetAll()
        {
            return await _context.Country.
                ToListAsync();
        }

        public async Task<Country> GetById(int id)
        {
            var countryDb = await _context.Country.FindAsync(id);
            if (countryDb == null) return null!;
            return countryDb;
        }

        public async Task<GeneralResponse> Insert(Country item)
        {
            var CheckIfNull = await CheckName(item.Name);
            if (!CheckIfNull) return new GeneralResponse(false, "Country already added");

            _context.Country.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Country item)
        {
            var countryDb = await _context.Country.FindAsync(item.Id);
            if (countryDb == null) return NotFound();

            countryDb.Name = item.Name;
            
            await Commit();
            return Success();
        }


        //Helpers
        private static GeneralResponse NotFound() => new(false, "Sorry Country not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        public async Task Commit() => await _context.SaveChangesAsync();
        public async Task<bool> CheckName(string name)
        {
            var item = await _context.Country.FirstOrDefaultAsync(d => d.Name!.ToLower().Equals(name.ToLower()));
            return item is null ? true : false;
        }
    }
}
