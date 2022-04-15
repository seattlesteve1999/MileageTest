using MileageManagerForms.DataAccess;
using MileageManagerForms.Database;
using MileageManagerForms.Utilities;
using MileageManagerForms.Views;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MileageManagerForms.ViewModels
{
    public class TotalStatsViewModel : INotifyPropertyChanged
    {
        public string carDesc { get; set; }
        public string entMiles { get; set; }
        public string entGas { get; set; }
        public string entMPG { get; set; }        
        public string entAvgCost { get; set; }       
        public string entTotalCost { get; set; }
        public string entYear { get; set; }
        
        public TotalStatsViewModel()
        {
            MileageItemRepository mir = new MileageItemRepository();
            System.Collections.Generic.List<AutoTableDefination> results = mir.GetAuto3(Convert.ToInt32(Application.Current.Properties["autoId"]));
            CarDesc = results[0].CarDesc + " Totals";
            GetTotalData();
        }

        public string CarDesc
        {
            get => carDesc;

            set
            {
                carDesc = value;
                var args = new PropertyChangedEventArgs(nameof(CarDesc));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public string EntMiles
        {
            get => entMiles;

            set
            {
                entMiles = value;
                var args = new PropertyChangedEventArgs(nameof(EntMiles));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public string EntGas
        {
            get => entGas;

            set
            {
                entGas = value;
                var args = new PropertyChangedEventArgs(nameof(EntGas));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public string EntMPG
        {
            get => entMPG;

            set
            {
                entMPG = value;
                var args = new PropertyChangedEventArgs(nameof(EntMPG));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public string EntAvgCost
        {
            get => entAvgCost;

            set
            {
                entAvgCost = value;
                var args = new PropertyChangedEventArgs(nameof(EntAvgCost));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public string EntTotalCost
        {
            get => entTotalCost;

            set
            {
                entTotalCost = value;
                var args = new PropertyChangedEventArgs(nameof(EntTotalCost));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public string EntYear
        {
            get => entYear;

            set
            {
                entYear = value;
                var args = new PropertyChangedEventArgs(nameof(EntYear));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public async void GetTotalData()
        {
            MileageItemRepository repository = new MileageItemRepository();
            int autoId = Convert.ToInt32(Application.Current.Properties["autoId"]);
            //System.Collections.Generic.List<MileageDisplayDefination> response = await repository.GetMileageDisplayData(autoId);
            var response = await repository.GetMileageData(autoId);
            decimal totalGas = 0;
            decimal totalMiles = 0;
            decimal totalPrice = 0;
            decimal rounded = 0;   

            foreach (var item in response)
            {
                totalGas += item.Gas;
                totalMiles += item.Miles;
                totalPrice += item.Price;
            }
            EntGas = totalGas.ToString("###,##0.000");
            EntMiles = totalMiles.ToString("###,##0.0");
            EntTotalCost = Math.Round(totalPrice, 2).ToString("$ ,##,##0.00");
            if (totalMiles > 0 || totalGas > 0)
            {
                rounded = Math.Round((totalMiles / totalGas), 3);
                EntAvgCost = Math.Round((totalPrice / totalGas), 2).ToString("$ ,##0.00");
            }

            EntMPG = rounded.ToString("#,##0.000");
        }

        public void GetYearlyTotals(string year)
        {
            GetTotalsByYear(year);
        }

        public void GrandTotals()
        {
            GetTotalData();
        }

        public async void GetTotalsByYear(string EntYear)
        {
            MileageItemRepository repository = new MileageItemRepository();
            int autoId = Convert.ToInt32(Application.Current.Properties["autoId"]);
            var response = await repository.GetMileageData(autoId);
            decimal totalGas = 0;
            decimal totalMiles = 0;
            decimal totalPrice = 0;
            decimal rounded = 0;

            foreach (var item in response)
            {
                if (item.StrDate.Substring(6, 4) == EntYear)
                {
                    totalGas += item.Gas;
                    totalMiles += item.Miles;
                    totalPrice += item.Price;
                }
            }
            EntGas = totalGas.ToString("###,##0.000");
            EntMiles = totalMiles.ToString("###,##0.0");
            EntTotalCost = Math.Round(totalPrice, 2).ToString("$ ,##,##0.00");
            if (totalMiles > 0 || totalGas > 0)
            {
                rounded = Math.Round((totalMiles / totalGas), 3);
                EntAvgCost = Math.Round((totalPrice / totalGas), 2).ToString("$ ,##0.00");

            }

            EntMPG = rounded.ToString("#,##0.000");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public Command DateCommand { get; set; }
    }
}