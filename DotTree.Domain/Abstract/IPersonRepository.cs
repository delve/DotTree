using DotTree.Domain.Entities;
using System.Collections.Generic;

namespace DotTree.Domain.Abstract
{
    public interface IPersonRepository
    {
        IEnumerable<Person> People { get; }

        void SavePerson(Person person);
        Person DeletePerson(int personID);
    }

}
