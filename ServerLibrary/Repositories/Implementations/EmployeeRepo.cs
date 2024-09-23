using BaseLibrary.Class.Entities;
using BaseLibrary.Class.Response;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class EmployeeRepo(ApplicationDbContext _context) : IGenaricRepository<Employee>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var empDb = await _context.Employees.FindAsync(id);
            if (empDb == null) return NotFound();

            _context.Employees.Remove(empDb);
            await Commit();
            return Success();
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(t => t.town).ThenInclude(b => b.City).ThenInclude(c => c.Country)
                .Include(b => b.branch).ThenInclude(d => d.Department).ThenInclude(gd => gd.GeneralDepartment)
                .ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            var empDb = await _context.Employees.FindAsync(id);
            if (empDb == null) return null!;
            return empDb;
        }

        public async Task<GeneralResponse> Insert(Employee item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, "Employee already added");

            _context.Employees.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Employee item)
        {
            var empDb = await _context.Employees.FirstOrDefaultAsync(e=>e.Id == item.Id);
            if (empDb == null) return NotFound();

            empDb.Name = item.Name;
            empDb.Other = item.Other;
            empDb.Address = item.Address;
            empDb.TelephoneNumber = item.TelephoneNumber;
            empDb.BranchId = item.BranchId;
            empDb.TownId = item.TownId;
            empDb.CivilId = item.CivilId;
            empDb.FileNumber = item.FileNumber;
            empDb.JobName = item.JobName;
            empDb.PhotoUrl = item.PhotoUrl;

            await _context.SaveChangesAsync();

            await Commit();
            return Success();
        }

        //Helpers
        private static GeneralResponse NotFound() => new(false, "Sorry Employee does not exist");
        private static GeneralResponse Success() => new(true, "Process Completed");
        public async Task Commit() => await _context.SaveChangesAsync();
        public async Task<bool> CheckName(string name)
        {
            var item = await _context.Employees.FirstOrDefaultAsync(d => d.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
