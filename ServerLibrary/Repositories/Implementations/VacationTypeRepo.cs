using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class VacationTypeRepo(ApplicationDbContext _context) : IGenaricRepository<VactionType>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var vacDb = await _context.VactionsType.FirstOrDefaultAsync(ot => ot.Id == id);
            if (vacDb == null) return NotFound();

            _context.VactionsType.Remove(vacDb);
            await Commit();
            return Success();
        }

        public async Task<List<VactionType>> GetAll()
        {
            return await _context.VactionsType.AsNoTracking().ToListAsync();
        }

        public async Task<VactionType> GetById(int id)
        {
            return await _context.VactionsType.FirstOrDefaultAsync(ot => ot.Id == id);
        }

        public async Task<GeneralResponse> Insert(VactionType item)
        {
            if (await CheckName(item.Name))
                return new GeneralResponse(false, "vaction type already added");

            _context.VactionsType.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(VactionType item)
        {
            var vacDb = await _context.VactionsType.FirstOrDefaultAsync(ot => ot.Id == item.Id);
            if (vacDb == null) return NotFound();

            vacDb.Name = item.Name;
            await Commit();
            return Success();
        }

        public static GeneralResponse NotFound() => new(false, "Sorry SnactionType not found");
        public static GeneralResponse Success() => new(true, "Success Operation");
        public async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await _context.VactionsType.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            if (item == null) return false;

            return true;
        }
    }
}
