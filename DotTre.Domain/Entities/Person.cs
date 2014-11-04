namespace DotTree.Domain
{
    public class Person
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string MiddleName { get; set; }
        public string NameSuffix { get; set; }
        public string NamePrefix { get; set; }
        public int BirthYear { get; set; }
        public int BirthMonth { get; set; }
        public int BirthDay { get; set; }
    }
}
