using assecor_assessment_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace assecor_assessment_backend.Tests
{
    public class AssessmentControllerTest
    {
        private readonly IEnumerable<Persons> _Persons;
        private readonly Persons _Person;
        private readonly IEnumerable<Persons> _ColorPersons;
        private readonly IActionResult _Result;
        private readonly assecor_assessment_backend.Controllers.AssessmentController _Controller;

        public AssessmentControllerTest() {
            _Controller = new assecor_assessment_backend.Controllers.AssessmentController();
            _Persons = new List<Persons> {};
            _Person = new Persons();
            _ColorPersons = new List<Persons> {};
            _Result = new OkResult();
        }
        [Fact]
        public void GetPersons_ShouldReturnAllPersons()
        {
            var result = _Controller.Get();
            Assert.Equal(_Persons, result);
        }
        [Fact]
        public void GetPersonById_ShouldReturnPerson()
        {
            var result = _Controller.Get(1);
            Assert.Equal(_Person, result);
        }
        [Fact]
        public void GetPersonsByColor_ShouldReturnPersons()
        {
            var result = _Controller.Get("red");
            Assert.Equal(_ColorPersons, result);
        }
    }
}
