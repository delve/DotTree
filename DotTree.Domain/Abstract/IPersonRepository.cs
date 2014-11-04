using System.Collections.Generic;
using DotTree.Domain.Entities;

namespace DotTree.Domain.Abstract
{
    public interface IPersonRepository
    {
        IEnumerable<Person> People { get; }
    }
}
