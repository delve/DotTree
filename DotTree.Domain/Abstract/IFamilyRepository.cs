using DotTree.Domain.Entities;
using System.Collections.Generic;

namespace DotTree.Domain.Abstract
{
    public interface IFamilyRepository
    {
        IEnumerable<Family> Families { get;  }
        int LoadMethod { get; set; }

        void SaveFamily(Family family);
        Family DeleteFamily(int familyID);
    }
}
