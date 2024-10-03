using Administration.Dtos;
using Administration.Models;
using Administration.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllServicesAsync()
        {
            var services = await _service_Service.GetAllServices();
            return Ok(services);

        }

        [Authorize]
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

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> CreateServiceAsync(ServiceDto serviceDto)
        {
            var service = new Service
            {
                Designation_Service = serviceDto.Designation_Service,
                PrixHT = serviceDto.PrixHT,
                PrixTTC = serviceDto.PrixTTC,
                TVA = serviceDto.TVA,
            };

            await _service_Service.AddService(service);
            return Ok(service);
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceAsync(byte id, ServiceDto serviceDto)
        {
            var service = await _service_Service.GetServiceById(id);
            if (service == null)
            {
                return NotFound(" Service Introuvable ");
            }
            service.TVA = serviceDto.TVA;
            service.Designation_Service = serviceDto.Designation_Service;
            service.PrixHT = serviceDto.PrixHT;
            service.PrixTTC = serviceDto.PrixTTC;

            _service_Service.UpdateService(service);
            return Ok(service);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceAsync(byte id)
        {
            var service = await _service_Service.GetServiceById(id);
            if (service == null)
            {
                return NotFound("Service introuvale pour supprimer");
            }
            _service_Service.DeleteService(service);
            return Ok(service);
        }

    }
}
