using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class OvertimeTypeRepositries(ApplicationDbContext _context) : IGenaricRepository<OverTimeType>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
           var overDb = await _context.OverTimeTypes.FirstOrDefaultAsync(ot=>ot.Id == id);
            if (overDb == null) return NotFound();
            
            _context.OverTimeTypes.Remove(overDb);
            await Commit();
            return Success();
        }

        public async Task<List<OverTimeType>> GetAll()
        {
            return await _context.OverTimeTypes.AsNoTracking().ToListAsync();
        }

        public async Task<OverTimeType> GetById(int id)
        {
            return await _context.OverTimeTypes.FirstOrDefaultAsync(ot => ot.Id == id);
        }

        public async Task<GeneralResponse> Insert(OverTimeType item)
        {
            if (await CheckName(item.Name))
                return new GeneralResponse(false, "OverType already added");

            _context.OverTimeTypes.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(OverTimeType item)
        {
            var overDb = await _context.OverTimeTypes.FirstOrDefaultAsync(ot => ot.Id == item.Id);
            if (overDb == null) return NotFound();

            overDb.Name = item.Name;
            await Commit();
            return Success();
        }

        public static GeneralResponse NotFound() => new(false, "Sorry OvertimeType not found");
        public static GeneralResponse Success() => new(true, "Success Operation");
        public async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name) 
        {
            var item = await _context.OverTimeTypes.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            if (item == null) return false;

            return true;
        }
    }
}
