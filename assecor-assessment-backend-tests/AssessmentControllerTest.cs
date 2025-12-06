using assecor_assessment_backend.Models;
using assecor_assessment_backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Text.Json.Serialization;
using FluentAssertions;

namespace assecor_assessment_backend.Tests
{
    public class AssessmentControllerTest
    {
        private readonly IEnumerable<Persons> _Persons;
        private readonly Persons _Person;
        private readonly IEnumerable<Persons> _ColorPersons;
        private readonly IActionResult _Result;
        private readonly AssessmentController _Controller;
        private readonly string _BackupTestFile = "backup-test-sample-input.csv";
        private readonly string _TestInputFile = "test-sample-input.csv";
        private readonly ICSVAccess _CSVAccess;

        public AssessmentControllerTest() {
            _CSVAccess = new CSVHandler();
            _Controller = new AssessmentController(_CSVAccess);
            _Controller.OverrideFilePath(_TestInputFile);
            _Persons = new List<Persons> {
                new Persons(1, "Hans", "Müller", "67742 Lauterecken", 1),
                new Persons(2, "Peter", "Petersen", "18439 Stralsund", 2)
            };
            _Person = new Persons(1, "Hans", "Müller", "67742 Lauterecken", 1);
            _ColorPersons = new List<Persons> {
                new Persons(1, "Hans", "Müller", "67742 Lauterecken", 1)
            };
            _Result = new OkResult();
        }
        [Fact]
        public void GetPersons_ShouldReturnAllPersons()
        {
            var result = _Controller.Get();
            result.Should().BeEquivalentTo(_Persons, options => options.IncludingAllRuntimeProperties());
        }
        [Fact]
        public void GetPersonById_ShouldReturnPerson()
        {
            var result = _Controller.Get(1);
            result.Should().BeEquivalentTo(_Person, options => options.IncludingAllRuntimeProperties());
        }
        [Fact]
        public void GetPersonsByColor_ShouldReturnPersons()
        {
            var result = _Controller.Get("blau");
            result.Should().BeEquivalentTo(_ColorPersons, options => options.IncludingAllRuntimeProperties());
        }
    }
}
