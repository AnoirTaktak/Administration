using Administration.Dtos;
using Administration.Models;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IService_Service _service_Service;

        public ServicesController(IService_Service service_Service)
        {
            _service_Service = service_Service;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllServicesAsync()
        {
            var services = await _service_Service.GetAllServices();
            return Ok(services);

        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceByIdAsync(int id)
        {
            var service = await _service_Service.GetServiceById(id);
            return Ok(service);
        }

        [HttpGet("designation/{designation}")]
        public async Task<IActionResult> GetServiceByDesAsync(string designation)
        {
            var service = await _service_Service.GetServiceByDes(designation);
            if (service == null)
            {
                return NotFound("Service introuvable");
            }
            return Ok(service);
        }

        

        [HttpPost]
        public async Task<IActionResult> CreateServiceAsync(ServiceDto serviceDto)
        {
            var service = new Service
            {
                Designation_Service = serviceDto.Designation_Service,
                PrixTTC = serviceDto.PrixTTC,
                TVA = serviceDto.TVA,
            };

            var result = await _service_Service.AddService(service);
            if (result.StartsWith("Erreur"))
            {
                return BadRequest(result);
            }
            return Ok();

        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceAsync(int id, [FromBody] ServiceDto serviceDto)
        {
            if (serviceDto == null)
            {
                return BadRequest("Service data is null.");
            }

            var service = await _service_Service.GetServiceById(id);
            if (service == null)
            {
                return NotFound($"Service with ID {id} not found.");
            }

            
            service.Designation_Service = serviceDto.Designation_Service;
            service.PrixTTC = serviceDto.PrixTTC;
            service.TVA = serviceDto.TVA;

            var result = await _service_Service.UpdateService(service);
            if (result.StartsWith("Erreur"))
            {
                return BadRequest(result);
            }
            return Ok();

        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceAsync(byte id)
        {
            var service = await _service_Service.GetServiceById(id);
            if (service == null)
            {
                return NotFound("Service introuvale pour supprimer");
            }
            await _service_Service.DeleteService(service);
            return Ok();
        }

    }
}
