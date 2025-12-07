using assecor_assessment_backend.Models;
using assecor_assessment_backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Text.Json.Serialization;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace assecor_assessment_backend.Tests
{
    public class AssessmentControllerTest
    {
        private readonly IEnumerable<Persons> _Persons;
        private readonly Persons _Person;
        private readonly IEnumerable<Persons> _ColorPersons;
        private readonly IEnumerable<Persons> _AddedPersons;
        private readonly AssessmentController _Controller;
        private readonly string _BackupTestFile = "backup-test-controller.csv";
        private readonly string _TestInputFile = "test-controller.csv";
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
            _AddedPersons = new List<Persons> {
                new Persons(1, "Hans", "Müller", "67742 Lauterecken", 1),
                new Persons(2, "Peter", "Petersen", "18439 Stralsund", 2),
                new Persons(3, "Anna", "Schmidt", "10115 Berlin", 3)
            };
            File.Copy(_TestInputFile, _BackupTestFile, true);
        }
        [Fact]
        public async Task GetPersons_ShouldReturnAllPersons()
        {
            var result = await _Controller.Get();
            var okresult = result.Result as ObjectResult;
            Assert.NotNull(okresult);
            Assert.Equal(StatusCodes.Status200OK, okresult.StatusCode);
            var personsResult = okresult.Value as IEnumerable<Persons>;
            personsResult.Should().BeEquivalentTo(_Persons, options => options.IncludingAllRuntimeProperties());
        }
        [Fact]
        public async Task GetPersonById_ShouldReturnPerson()
        {
            var result = await _Controller.Get(1);
            var okresult = result.Result as ObjectResult;
            Assert.NotNull(okresult);
            Assert.Equal(StatusCodes.Status200OK, okresult.StatusCode);
            var personsResult = okresult.Value as Persons;
            personsResult.Should().BeEquivalentTo(_Person, options => options.IncludingAllRuntimeProperties());
        }
        [Fact]
        public async Task GetPersonsByColor_ShouldReturnPersons()
        {
            var result = await _Controller.Get("blau");
            var okresult = result.Result as ObjectResult;
            Assert.NotNull(okresult);
            Assert.Equal(StatusCodes.Status200OK, okresult.StatusCode);
            var colorresult = okresult.Value as IEnumerable<Persons>;
            colorresult.Should().BeEquivalentTo(_ColorPersons, options => options.IncludingAllRuntimeProperties());
        }
        [Fact]
        public async Task GetPersonsById_ShouldReturnNotFound()
        {
            var result = _Controller.Get(999);
            var failedresult = result.Result.Result as ObjectResult;
            Assert.NotNull(failedresult);
            Assert.Equal(StatusCodes.Status500InternalServerError, failedresult.StatusCode);
        }

        [Fact]
        public async Task GetPersonsByColor_ShouldReturnNotFound()
        {
            var result = await _Controller.Get("schwarz");
            var failedresult = result.Result as ObjectResult;
            Assert.NotNull(failedresult);
            Assert.Equal(StatusCodes.Status500InternalServerError, failedresult.StatusCode);
        }

        [Fact]
        public async Task PostPerson_ShouldAddPerson()
        {
            var newPerson = new Persons(0, "Anna", "Schmidt", "10115 Berlin", 3);
            var postResult = await _Controller.Post(newPerson);
            var okresult = postResult as ObjectResult;
            Assert.NotNull(okresult);
            Assert.Equal(StatusCodes.Status201Created, okresult.StatusCode);

            var newPersons = await _Controller.Get();
            var okgetresult = newPersons.Result as ObjectResult;
            Assert.NotNull(okgetresult);
            Assert.Equal(StatusCodes.Status200OK, okgetresult.StatusCode);
            var personsResult = okgetresult.Value as IEnumerable<Persons>;
            Assert.NotNull(personsResult);
            personsResult.Should().BeEquivalentTo(_AddedPersons, options => options.IncludingAllRuntimeProperties());
            File.Copy(_BackupTestFile, _TestInputFile, true);
        }
    }
}
