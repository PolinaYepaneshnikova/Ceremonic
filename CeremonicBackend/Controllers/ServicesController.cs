using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;
using System.Linq;

namespace CeremonicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        IUnitOfWork _UoW { get; set; }
        public ServicesController(IUnitOfWork uow)
        {
            _UoW = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetServicesList()
        {
            List<GeneralServiceEntity> services = (await _UoW.ServiceRepository.GetByPredicate(e => true)).ToList();

            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            GeneralServiceEntity service = await _UoW.ServiceRepository.GetById(id);

            return Ok(service);
        }

        [HttpGet]
        [Route("names")]
        public async Task<IActionResult> GetServicesNamesList()
        {
            List<string> servicesName =
                (await _UoW.ServiceRepository.GetByPredicate(e => true))
                .Select(e => e.RelationalServiceEntity.Name).ToList();

            return Ok(servicesName);
        }
    }
}
