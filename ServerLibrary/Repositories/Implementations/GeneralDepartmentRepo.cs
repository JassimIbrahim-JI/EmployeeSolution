using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;


namespace ServerLibrary.Repositories.Implementations
{
    public class GeneralDepartmentRepo(ApplicationDbContext _context) : IGenaricRepository<GeneralDepartment>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var depDb = await _context.GeneralDepartments.Where(d => d.Id == id)
                 .FirstOrDefaultAsync();
            if (depDb == null) 
            {
                return NotFound();
            }
            _context.GeneralDepartments.Remove(depDb);
            await Commit();
            return Success();
        }

        public async Task<List<GeneralDepartment>> GetAll()
        => await _context.GeneralDepartments.ToListAsync();

        public async Task<GeneralDepartment> GetById(int id)
        {
            var deptDb = await _context.GeneralDepartments.FirstOrDefaultAsync(d => d.Id == id);
            if (deptDb == null) return null!;
            return deptDb;
        }

        public async Task<GeneralResponse> Insert(GeneralDepartment item)
        {
            var CheckIfNull = await CheckName(item.Name);
           if(!CheckIfNull) return new GeneralResponse(false,"General Department already added");
           
            _context.GeneralDepartments.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(GeneralDepartment item)
        {
            var depDb = await _context.GeneralDepartments.FindAsync(item.Id);
            if (depDb == null) return NotFound();

            depDb.Name = item.Name;
            await Commit();
            return Success();
        }


        //Helpers
        private static GeneralResponse NotFound() => new(false, "Sorry General Department not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        public async Task Commit() => await _context.SaveChangesAsync();
        
        public async Task<bool> CheckName(string name) 
        {
            var item = await _context.GeneralDepartments.FirstOrDefaultAsync(d => d.Name!.ToLower().Equals(name.ToLower()));
            return item is null ? true : false;
        }
       
    }
}
