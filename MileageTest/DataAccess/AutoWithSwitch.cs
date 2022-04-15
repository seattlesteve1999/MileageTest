

namespace MileageManagerForms.DataAccess
{
    public class AutoWithSwitch
    {
        #region Computed Propoperties
        public int Id { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        #endregion

        #region Constructors
        public AutoWithSwitch()
        {
        }

        public AutoWithSwitch(int id, string year, string name, bool swDefault)
        {
            Id = id;
            Year = year;
            Name = name;
            IsChecked = swDefault;
        }
        #endregion
    }
}