
using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class DepartmentRepo(ApplicationDbContext _context) : IGenaricRepository<Department>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
           var depDb = await _context.Departments.FindAsync(id);
            if(depDb == null) return NotFound();

            _context.Departments.Remove(depDb);
            await Commit();
            return Success();
        }

        public async Task<List<Department>> GetAll()
        {
            return await _context.Departments
                .AsNoTracking().
                Include(gd=>gd.GeneralDepartment).
                ToListAsync();
        }

        public async Task<Department> GetById(int id)
        {
            var depDb = await _context.Departments.FindAsync(id);
            if (depDb == null) return null!;
            return depDb;
        }

        public async Task<GeneralResponse> Insert(Department item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Department already added");

            _context.Departments.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Department item)
        {
            var depDb = await _context.Departments.FindAsync(item.Id);
            if (depDb == null) return NotFound();

            depDb.Name = item.Name;
            depDb.GeneralDepartmentId = item.GeneralDepartmentId;
            await Commit();
            return Success();
        }

        //Helpers
        private static GeneralResponse NotFound() => new(false, "Sorry department not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        public async Task Commit() => await _context.SaveChangesAsync();
        public async Task<bool> CheckName(string name)
        {
            var item = await _context.Departments.FirstOrDefaultAsync(d => d.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }

    }
}
