using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class DoctorRepo(ApplicationDbContext _context) : IGenaricRepository<Doctor>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var docDb = await _context.Doctors.FirstOrDefaultAsync(eid=>eid.EmployeeId == id);
            if (docDb == null) return NotFound();

            _context.Doctors.Remove(docDb);
            await Commit();
            return Success();
        }

        public async Task<List<Doctor>> GetAll()
        {
            return await _context.Doctors.AsNoTracking()
                 .ToListAsync();
        }

        public async Task<Doctor> GetById(int id)
        {
          return await _context.Doctors.FirstOrDefaultAsync(eid=>eid.EmployeeId==id);
        }

        public async Task<GeneralResponse> Insert(Doctor item)
        {
            _context.Doctors.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Doctor item)
        {
            var docDb = await _context.Doctors.FirstOrDefaultAsync(eid=>eid.EmployeeId==item.EmployeeId);
            if (docDb == null) return NotFound();

            docDb.MedicalRecommendation = item.MedicalRecommendation;
            docDb.MedicalDiagonse = item.MedicalDiagonse;
            docDb.Date = item.Date;
            await Commit();
            return Success();
        }

        //Helpers
        private static GeneralResponse NotFound() => new(false, "Sorry Town not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        public async Task Commit() => await _context.SaveChangesAsync();
    
    }
}
