namespace DotTree.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ParentId2 { get; set; }
        public int FamilyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameSuffix { get; set; }
        public string NamePrefix { get; set; }
        public int? BirthYear { get; set; }
        public int? BirthMonth { get; set; }
        public int? BirthDay { get; set; }
    }
}
