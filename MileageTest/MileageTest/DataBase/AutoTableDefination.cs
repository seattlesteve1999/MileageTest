using SQLite;

namespace MileageManagerForms.Database
{
    [Table("AutoData")]
    public class AutoTableDefination
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool IsDefault { get; set; }
        public string CarYear { get; set; }
        public string CarDesc { get; set; }
    }
}