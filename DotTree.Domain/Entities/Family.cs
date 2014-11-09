using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DotTree.Domain.Entities
{
    public class Family
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        
        [Required(ErrorMessage="Please enter a family name")]
        [Display(Name="Family Name")]
        public string FamilyName { get; set; }

        [Display(Prompt="My wonderful extended family!")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Family Members")]
        public ICollection<Person> FamilyMembers { get; set; }

        public Family()
        {
            FamilyMembers = new HashSet<Person>();
        }
    }
}
