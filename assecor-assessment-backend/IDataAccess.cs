namespace assecor_assessment_backend
{
    public interface IDataAccess
    {
        bool ReadPersons(out IEnumerable<Models.Persons> persons);
        bool AddPersons(Models.Persons person);
    }
}
