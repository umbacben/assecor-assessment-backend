namespace assecor_assessment_backend
{
    public interface ICSVAccess
    {
        bool ReadPersons(out IEnumerable<Models.Persons> persons);
        bool AddPersons(Models.Persons person);
    }
}
