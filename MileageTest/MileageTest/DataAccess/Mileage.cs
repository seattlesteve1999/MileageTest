using MileageManagerForms.Database;
using System;

namespace MileageManagerForms.DataAccess
{
    public class Mileage
    {
        #region Computed Propoperties
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StrDate { get; set; }
        public decimal Gas { get; set; }
        public decimal Miles { get; set; }
        public decimal MPG { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        #endregion

        #region Constructors
        public Mileage()
        {
        }

        public Mileage(int id, DateTime date, decimal gas, decimal miles, decimal mpg, decimal price, string note)
        {
            Id = id;
            Date = date;
            Gas = gas;
            Miles = miles;
            MPG = mpg;
            Price = price;
            Note = note;
        }

        public static implicit operator Mileage(MileageTableDefination v)
        {
            Mileage m = new Mileage
            {
                Id = v.Id,
                Date = Convert.ToDateTime(v.StrDate),
                StrDate = v.StrDate,
                Gas = v.Gas,
                Miles = v.Miles,
                MPG = v.MPG,
                Price = v.Price,
                Note = v.Note
            };
            return m;
        }

        #endregion
    }
}