using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class SanctionRepo(ApplicationDbContext _context) : IGenaricRepository<Sanction>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var sanDb = await _context.Sanctions.FirstOrDefaultAsync(s=>s.EmployeeId == id);
            if (sanDb == null) return NotFound();

            _context.Sanctions.Remove(sanDb);
            await Commit();
            return Success();
        }

        public async Task<List<Sanction>> GetAll()
        {
          return await _context.Sanctions.AsNoTracking().Include(st=>st.SanctionType).ToListAsync();
        }

        public async Task<Sanction> GetById(int id)
        {
            return await _context.Sanctions.FirstOrDefaultAsync(s => s.EmployeeId == id);
        }

        public async Task<GeneralResponse> Insert(Sanction item)
        {
            _context.Sanctions.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Sanction item)
        {
          var sanDb = await _context.Sanctions.FirstOrDefaultAsync(s=>s.EmployeeId == item.EmployeeId);
            if (sanDb == null) return NotFound();

            sanDb.Punishment = item.Punishment;
            sanDb.PunishmentDate = item.PunishmentDate;
            sanDb.Date = item.Date;
            sanDb.SanctionTypeId = item.SanctionTypeId;
            await Commit();
            return Success();
        }

        public static GeneralResponse NotFound() => new(false, "Sorry Sanction Not Found");
        public static GeneralResponse Success() => new(true, "Success Operation");
        public async Task Commit() => await _context.SaveChangesAsync();
    }
}
