using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotTree.Domain.Abstract;
using DotTree.Domain.Entities;

namespace DotTree.Domain.Concrete
{
    public class EFFamilyRepository : IFamilyRepository
    {
        private EFDbContext context = new EFDbContext();
        public int LoadMethod { get; set; }

        public IEnumerable<Family> Families
        {
            get 
            {
                switch (this.LoadMethod)
                {
                    case 0:
                        return context.Families;
                    case 1:
                        return context.Families.Include("FamilyMembers");
                    case 2:
                        return context.Families.ToList();
                    default:
                        return context.Families;
                }
            }
        }

        /// <summary>
        /// Instantiates a family repository from an Entity Framework context 
        /// </summary>
        /// <param name="EFLoadMethod">0 = lazy loading; 1 = eager loading; 2 = explixit loading</param>
        public EFFamilyRepository(int EFLoadMethod = 0)
        {
            this.LoadMethod = EFLoadMethod;
        }


        public void SaveFamily(Family family)
        {
            if (0 == family.Id)
            {
                context.Families.Add(family);
            }
            else
            {
                Family dbEntry = context.Families.Find(family.Id);
                if (dbEntry!=null)
                {
                    dbEntry.Description = family.Description;
                    dbEntry.FamilyName = family.FamilyName;
                }
            }

            context.SaveChanges();
        }


        public Family DeleteFamily(int familyID)
        {
            Family dbEntry = context.Families.Find(familyID);

            if (null != dbEntry)
            {
                context.Families.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}