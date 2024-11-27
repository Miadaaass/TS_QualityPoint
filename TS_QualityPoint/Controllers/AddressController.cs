using TS_QualityPoint.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TS_QualityPoint.Models;

namespace TS_QualityPoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStandardizedAddress([FromQuery] string address)
        {
            var result = await _addressService.StandardizeAddressAsync(address);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}

