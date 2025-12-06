using assecor_assessment_backend;
using assecor_assessment_backend.Models;
using FluentAssertions;

namespace assecor_assessment_backend_tests
{
    public class AssessmentCSVHandlerTest
    {

        private readonly IEnumerable<Persons> _ExpectedReadPersons;
        private readonly IEnumerable<Persons> _ExpectedWritePersons;
        private readonly string _BackupTestFile = "backup-test-sample-input.csv";
        private readonly string _TestInputFile = "test-sample-input.csv";
        private readonly ICSVAccess _CSVAccess;

        public AssessmentCSVHandlerTest()
        {
            _CSVAccess = new CSVHandler();
            ((CSVHandler)_CSVAccess).FilePath = _TestInputFile;
            _ExpectedWritePersons = new List<Persons>
            {
                new Persons(1, "Hans", "Müller", "67742 Lauterecken", 1),
                new Persons(2, "Peter", "Petersen", "18439 Stralsund", 2),
                new Persons(3, "Illidan", "Stormrage", "66666 Outland", 2)
            };
            _ExpectedReadPersons = new List<Persons>
            {
                new Persons(1, "Hans", "Müller", "67742 Lauterecken", 1),
                new Persons(2, "Peter", "Petersen", "18439 Stralsund", 2)
            };
            File.Copy(_TestInputFile, _BackupTestFile, true);
        }

        [Fact]
        public void ReadCSV_ShouldReturnAllPersons()
        {
            IEnumerable<Persons> PersonsResult;
            _CSVAccess.ReadPersons(out PersonsResult);
            PersonsResult.Should().BeEquivalentTo(_ExpectedReadPersons, options => options.IncludingAllRuntimeProperties());
        }

        [Fact]
        public void WriteCSV_ShouldWriteAllPersons()
        {
            _CSVAccess.AddPersons(new Persons(3, "Illidan", "Stormrage", "66666 Outland", 2));
            IEnumerable<Persons> WrittenResults;
            _CSVAccess.ReadPersons(out WrittenResults);
            File.Copy(_BackupTestFile, _TestInputFile, true);
            WrittenResults.Should().BeEquivalentTo(_ExpectedWritePersons, options => options.IncludingAllRuntimeProperties());
        }
    }
}
