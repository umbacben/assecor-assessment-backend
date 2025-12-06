using assecor_assessment_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace assecor_assessment_backend.Controllers
{
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        [Route("persons")]
        [HttpGet]
        public IEnumerable<Persons> Get()
        {
            return new List<Persons> { };
        }

        [Route("persons/{id:int:min(0)}")]
        [HttpGet]
        public Persons Get(int id)
        {
            return new Persons();
        }

        [Route("color/{color:maxlength(10)}")]
        [HttpGet]
        public IEnumerable<Persons> Get(string color)
        {
            return new List<Persons> { };
        }

        [Route("persons")]
        [HttpPost]
        public IActionResult Post(Persons person)
        {
            // You can add logic to process the person object here
            return Ok();
        }
    }
}
