using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DotTree.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please select a parent, or 'Unknown'")]
        public int ParentId { get; set; }
        
        [Required(ErrorMessage = "Please select a parent, or 'Unknown'")]
        public int ParentId2 { get; set; }
        public int FamilyId { get; set; }
        
        [Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; }
        public string NameSuffix { get; set; }
        public string NamePrefix { get; set; }
        public int? BirthYear { get; set; }
        public int? BirthMonth { get; set; }
        public int? BirthDay { get; set; }

        public ICollection<Family> FamilyMemberships { get; set; }

        public Person()
        {
            FamilyMemberships = new HashSet<Family>();
        }
    }
}
