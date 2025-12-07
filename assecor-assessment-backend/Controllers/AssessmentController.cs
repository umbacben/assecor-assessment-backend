using assecor_assessment_backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("assecor-assessment-backend.Tests")]
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
        internal bool OverrideFilePath(string filePath)
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
        public async Task<ActionResult<IEnumerable<Persons>>> Get()
        {
            if (_Persons == null || !_Persons.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "No Persons Found");
            }
            return Ok(_Persons);
        }

        [Route("persons/{id:int:min(0)}")]
        [HttpGet]
        public async Task<ActionResult<Persons>> Get(int id)
        {
            if (!_Persons.Any(Person => Person.Id == id))
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
            return Ok(_Persons.First(Person => Person.Id == id));
        }

        [Route("color/{color:maxlength(10)}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persons>>> Get(string color)
        {
            var filteredPersons = _Persons.Where(Person => Person.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
            if (filteredPersons == null || !filteredPersons.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"No Persons with {color} as their favorite color Found");
            }
            return Ok(filteredPersons);
        }

        [Route("persons")]
        [HttpPost]
        public async Task<ActionResult> Post(Persons person)
        {
            try
            {
                if (person == null || _Persons.Any(persons => persons.Id == person.Id) || !person.DoesColorExist(out int colorkey))
                {
                    return BadRequest();
                }

                int newId = _Persons.Max(p => p.Id) + 1;
                person.Id = newId;
                var didCreatePerson = _CSVAccess.AddPersons(person);
                if (didCreatePerson)
                {
                    _Persons = _Persons.Concat(new List<Persons> { person });
                    return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Persons record");
                }


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new Persons record");
            }
        }

        [NonAction]
        private bool IsInvalidColor(string color)
        {
            throw new NotImplementedException();
        }
    }
}
