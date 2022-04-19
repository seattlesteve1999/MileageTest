using SQLite;
using System;

namespace MileageManagerForms.Database
{
    [Table("MileageData")]
    public class MileageTableDefination
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime Date { get; set; }
        public string StrDate { get; set; }
        public decimal Miles { get; set; }
        public decimal Gas { get; set; }
        public decimal MPG { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
    }
}