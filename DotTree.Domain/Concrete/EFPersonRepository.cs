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
    }
}
