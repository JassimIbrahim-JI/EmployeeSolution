
using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class SanctionTypeRepositries(ApplicationDbContext _context) : IGenaricRepository<SanctionType>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var sancDb = await _context.SanctionTypes.FirstOrDefaultAsync(ot => ot.Id == id);
            if (sancDb == null) return NotFound();

            _context.SanctionTypes.Remove(sancDb);
            await Commit();
            return Success();
        }

        public async Task<List<SanctionType>> GetAll()
        {
            return await _context.SanctionTypes.AsNoTracking().ToListAsync();   
        }

        public async Task<SanctionType> GetById(int id)
        {
            return await _context.SanctionTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GeneralResponse> Insert(SanctionType item)
        {
            if (await CheckName(item.Name))
                return new GeneralResponse(false, "sanction type already added");

            _context.SanctionTypes.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(SanctionType item)
        {
            var sancDb = await _context.SanctionTypes.FirstOrDefaultAsync(ot => ot.Id == item.Id);
            if (sancDb == null) return NotFound();

            sancDb.Name = item.Name;
            await Commit();
            return Success();
        }

        public static GeneralResponse NotFound() => new(false, "Sorry SnactionType not found");
        public static GeneralResponse Success() => new(true, "Success Operation");
        public async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await _context.SanctionTypes.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            if (item == null) return false;

            return true;
        }
    }
}
