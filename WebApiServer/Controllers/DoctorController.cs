using BaseLibrary.Class.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : GenericController<Doctor>
    {
        public DoctorController(IGenaricRepository<Doctor> genaricRepository) : base(genaricRepository)
        {
        }
    }
}
