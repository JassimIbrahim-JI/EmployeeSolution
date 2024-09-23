using BaseLibrary.Class.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : GenericController<Country>
    {
        public CountryController(IGenaricRepository<Country> genaricRepository) : base(genaricRepository)
        {
        }
    }
}
