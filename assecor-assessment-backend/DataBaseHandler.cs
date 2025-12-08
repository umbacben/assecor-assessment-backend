using assecor_assessment_backend.Data;
using assecor_assessment_backend.Models;

namespace assecor_assessment_backend
{
    public class DataBaseHandler : IDataAccess
    {
        private readonly PersonsContext personsContext;

        public DataBaseHandler(PersonsContext personsContext)
        {
            this.personsContext = personsContext;
        }

        public bool AddPersons(Persons person)
        {
            if (person == null)
            {
                return false;
            }

            personsContext.Persons.Add(person);
            personsContext.SaveChanges();
            return true;
        }

        public bool ReadPersons(out IEnumerable<Persons> persons)
        {
            if (personsContext.Persons == null)
            {
                persons = Enumerable.Empty<Persons>();
                return false;
            }
            persons = personsContext.Persons.ToList();
            return true;
        }
    }
}
