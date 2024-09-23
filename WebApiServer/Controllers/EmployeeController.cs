using BaseLibrary.Class.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IGenaricRepository<Employee> genaricRepository) : GenericController<Employee>(genaricRepository)
    {

    }
}
