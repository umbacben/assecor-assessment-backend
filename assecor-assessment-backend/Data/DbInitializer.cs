using assecor_assessment_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace assecor_assessment_backend.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PersonsContext context)
        {
            // don't touch DB if already seeded
            if (context.Persons.Any())
            {
                return;
            }

            var entityType = context.Model.FindEntityType(typeof(Persons));
            var schema = entityType?.GetSchema() ?? "dbo";
            var table = entityType?.GetTableName() ?? "Persons";
            var fullTableName = $"[{schema}].[{table}]";

            // Keep the same connection / session so SET IDENTITY_INSERT applies to the upcoming inserts
            context.Database.OpenConnection();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {fullTableName} ON");

                var PersonsArray = new Persons[]
                {
                    new Persons(1, "Hans", "Müller", "67742", "Lauterecken", 1 ),
                    new Persons(2, "Peter", "Petersen", "18439", "Stralsund", 2 ),
                    new Persons(3, "Johnny", "Johnson", "88888", "made up", 3 ),
                    new Persons(4, "Milly", "Millenium", "77777", "made up too", 4 ),
                    new Persons(5, "Jonas", "Müller", "32323", "Hansstadt", 5 ),
                    new Persons(6, "Tastatur", "Fujitsu", "42342", "Japan", 6 ),
                    new Persons(7, "Anders", "Andersson", "32132", "Schweden - ☀", 2 ),
                    new Persons(8, "Bertram", "Bart", "12313", "Wasweißich", 1 ),
                    new Persons(9, "Gerda", "Gerber", "76535", "Woanders", 3 ),
                    new Persons(10, "Klaus", "Klaussen", "43246", "Hierach", 2 )
                };

                context.Persons.AddRange(PersonsArray);
                context.SaveChanges();

                context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {fullTableName} OFF");

                transaction.Commit();
            }
            finally
            {
                // Ensure IDENTITY_INSERT turned off and connection closed even on error
                try
                {
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {fullTableName} OFF");
                }
                catch
                {
                    // swallow - best effort to reset DB state
                }

                context.Database.CloseConnection();
            }
        }
    }
}
