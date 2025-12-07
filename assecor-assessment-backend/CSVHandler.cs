using assecor_assessment_backend.Models;
using System;

namespace assecor_assessment_backend
{
    public class CSVHandler : ICSVAccess
    {
        public string? FilePath { get; set; } = "sample-input.csv";
        public CSVHandler()
        {
        }
        public bool AddPersons(Persons person)
        {
            if (FilePath == null)
            {
                return false;
            }

            string CsvString;
            if (!person.ToCSVString(out CsvString))
            {
                return false;
            }

            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine();
                writer.Write(CsvString);
            }
            return true;
        }

        public bool ReadPersons(out IEnumerable<Persons> persons)
        {
            if (FilePath == null)
            {
                persons = [];
                return false;
            }
            var personsList = new List<Persons>();
            using (StreamReader reader = new StreamReader(FilePath))
            {
                string? line;
                int Id = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');  // Split based on the comma delimiter
                    if (values.Length >= 4)
                    {
                        var person = new Persons(Id, values[1], values[0], values[2], Int32.Parse(values[3]));
                        personsList.Add(person);
                    }
                    Id++;
                }
            }
            persons = personsList;
            return true;
        }
    }
}
