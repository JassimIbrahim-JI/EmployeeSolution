

using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class BranchRepo(ApplicationDbContext _context) : IGenaricRepository<Branch>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var branchDb = await _context.Branches.FindAsync(id);
            if (branchDb == null) return NotFound();

            _context.Branches.Remove(branchDb);
            await Commit();
            return Success();
        }

        public async Task<List<Branch>> GetAll()
        {
            return await _context.Branches.AsNoTracking().Include(d=>d.Department).ToListAsync();
        }

        public async Task<Branch> GetById(int id)
        {
            var branchDb = await _context.Branches.FindAsync(id);
            if (branchDb == null) return null!;
            return branchDb;
        }

        public async Task<GeneralResponse> Insert(Branch item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "City already added");

            _context.Branches.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Branch item)
        {
            var branchDb = await _context.Branches.FindAsync(item.Id);
            if (branchDb == null) return NotFound();

            branchDb.Name = item.Name;
            branchDb.DepartmentId = item.DepartmentId;
            await Commit();
            return Success();
        }

        //Helpers
        private static GeneralResponse NotFound() => new(false, "Sorry Branch not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        public async Task Commit() => await _context.SaveChangesAsync();
        public async Task<bool> CheckName(string name)
        {
            var item = await _context.Branches.FirstOrDefaultAsync(d => d.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
