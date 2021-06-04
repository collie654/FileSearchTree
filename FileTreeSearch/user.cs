using CsvHelper.Configuration.Attributes;

namespace FileTreeSearch
{
    class user
    {
        [Name("First_Name")]
        public string firstName { get; set; }
        [Name("Last_Name")]
        public string lastName { get; set; }
        [Name("Email_Address")]
        public string emailAddress { get; set; }
    }
}
