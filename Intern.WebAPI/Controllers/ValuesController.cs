using DataAccessLayer.Contect;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intern.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly Context _context;

        public ValuesController(Context context)
        {
            _context = context;
        }
        [HttpGet("getDataFromAPI")]
        public IActionResult GetDatFromDatabase()
        {

            List<RandomTemperature> data = _context.RandomTemperatures.ToList();
            return Ok(data);
            
        }
    }
}
    

