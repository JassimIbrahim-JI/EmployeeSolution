

using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class VacationRepo(ApplicationDbContext _context) : IGenaricRepository<Vaction>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var vacDb = await _context.Vactions.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
            if (vacDb == null) return NotFound();

            _context.Vactions.Remove(vacDb);
            await Commit();
            return Success();
        }

        public async Task<List<Vaction>> GetAll()
        {
            return await _context.Vactions.AsNoTracking().Include(vt => vt.VactionType)
                  .ToListAsync();
        }

        public async Task<Vaction> GetById(int id)
        {
            return await _context.Vactions.FirstOrDefaultAsync(o => o.EmployeeId == id);
        }

        public async Task<GeneralResponse> Insert(Vaction item)
        {
            _context.Vactions.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Vaction item)
        {
            var vacDb = await _context.Vactions.FirstOrDefaultAsync(eid => eid.EmployeeId == item.EmployeeId);
            if (vacDb == null) return NotFound();

            vacDb.StartDate = item.StartDate;
            vacDb.NumberOfDays = item.NumberOfDays;
            vacDb.VactionTypeId = item.VactionTypeId;
            await Commit();
            return Success();
        }

        //Helpers
        private static GeneralResponse NotFound() => new(false, "Sorry Vaction not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        public async Task Commit() => await _context.SaveChangesAsync();
    }
}
