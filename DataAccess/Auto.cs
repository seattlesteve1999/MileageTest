namespace MileageManagerForms.DataAccess
{
    public class Auto
    {
        #region Computed Propoperties
        public int Id { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public bool Default { get; set; }
        #endregion

        #region Constructors
        public Auto()
        {
        }

        public Auto(int id, string year, string name)
        {
            Id = id;
            Year = year;
            Name = name;
            Default = true;
        }
        #endregion
    }
}