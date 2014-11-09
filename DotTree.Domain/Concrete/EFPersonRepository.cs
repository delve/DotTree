using System.Collections.Generic;
using DotTree.Domain.Abstract;
using DotTree.Domain.Entities;

namespace DotTree.Domain.Concrete
{
    public class EFPersonRepository : IPersonRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Person> People
        {
            get { return context.People; }
        }

        public void SavePerson(Person person)
        {
            if (0== person.Id)
            {
                context.People.Add(person);
            }
            else
            {
                Person dbEntry = context.People.Find(person.Id);
                if (dbEntry != null)
                {
                    dbEntry.BirthDay = person.BirthDay;
                    dbEntry.BirthMonth = person.BirthMonth;
                    dbEntry.BirthYear = person.BirthYear;
                    dbEntry.FamilyId = person.FamilyId;
                    dbEntry.FirstName = person.FirstName;
                    dbEntry.LastName = person.LastName;
                    dbEntry.MiddleName = person.MiddleName;
                    dbEntry.NamePrefix = person.NamePrefix;
                    dbEntry.NameSuffix = person.NameSuffix;
                    dbEntry.ParentId = person.ParentId;
                    dbEntry.ParentId2 = person.ParentId2;
                }
            }

            context.SaveChanges();
        }

        public Person DeletePerson(int personID)
        {
            Person dbEntry = context.People.Find(personID);

            if (null != dbEntry)
            {
                context.People.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
