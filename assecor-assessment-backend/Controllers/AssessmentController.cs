using assecor_assessment_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace assecor_assessment_backend.Controllers
{
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly ICSVAccess _CSVAccess;
        private IEnumerable<Persons> _Persons;
        private readonly string _DefaultFilePath = "sample-input.csv";

        
        public AssessmentController(ICSVAccess cSVAccess) : base()
        {
            _CSVAccess = cSVAccess;
            ((CSVHandler)_CSVAccess).FilePath = _DefaultFilePath;
            _CSVAccess.ReadPersons(out _Persons);
        }

        [NonAction]
        public bool OverrideFilePath(string filePath)
        {
            try
            {
                ((CSVHandler)_CSVAccess).FilePath = filePath;
                _CSVAccess.ReadPersons(out _Persons);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [Route("persons")]
        [HttpGet]
        public IEnumerable<Persons> Get()
        {
            return _Persons;
        }

        [Route("persons/{id:int:min(0)}")]
        [HttpGet]
        public Persons Get(int id)
        {
            return _Persons.First(Person => Person.Id == id);
        }

        [Route("color/{color:maxlength(10)}")]
        [HttpGet]
        public IEnumerable<Persons> Get(string color)
        {
            var filteredPersons = _Persons.Where(Person => Person.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
            return filteredPersons;
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
