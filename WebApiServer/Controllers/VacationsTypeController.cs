using BaseLibrary.Class.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsTypeController : GenericController<VactionType>
    {
        public VacationsTypeController(IGenaricRepository<VactionType> genaricRepository) : base(genaricRepository)
        {
        }
    }
}
