using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;


namespace ServerLibrary.Repositories.Implementations
{
    public class OvertimeRepo(ApplicationDbContext _context) : IGenaricRepository<OverTime>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var overDb = await _context.OverTime.FirstOrDefaultAsync(eid => eid.EmployeeId == id);
            if (overDb == null) return NotFound();

            _context.OverTime.Remove(overDb);
            await Commit();
            return Success();
        }

        public async Task<List<OverTime>> GetAll()
        {
           return await _context.OverTime.AsNoTracking().
                Include(ot=>ot.OverTimeType).ToListAsync();
        }

        public async Task<OverTime> GetById(int id)
        {
            return await _context.OverTime.FirstOrDefaultAsync(o => o.EmployeeId == id);
        }

        public async Task<GeneralResponse> Insert(OverTime item)
        {
           _context.OverTime.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(OverTime item)
        {
            var overDb = await _context.OverTime.FirstOrDefaultAsync(eid => eid.EmployeeId == item.EmployeeId);
            if (overDb == null) return NotFound();

            overDb.StartDate = item.StartDate;
            overDb.EndDate = item.EndDate;
            overDb.OverTimeTypeId = item.OverTimeTypeId;
            await Commit();
            return Success();
        }
        //Helpers
        private static GeneralResponse NotFound() => new(false, "Sorry Type not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        public async Task Commit() => await _context.SaveChangesAsync();
    }
}
