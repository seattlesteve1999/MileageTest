

using SQLite;

namespace MileageManagerForms.Database
{
    [Table("MileageData")]
    public class MileageDisplayDefination
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CarId { get; set; }
        public string Date { get; set; }
        public string StrDate { get; set; }
        public decimal Miles { get; set; }
        public decimal Gas { get; set; }
        public decimal MPG { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
    }
}