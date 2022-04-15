using MileageManagerForms.DataAccess;
using MileageManagerForms.Database;
using MileageManagerForms.Utilities;
using MileageManagerForms.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace MileageManagerForms.ViewModels
{
    internal class EditViewModel : INotifyPropertyChanged
    {
        public DateTime SDate { get; set; }
        public string entError { get; set; }
        public string EntMiles { get; set; }
        public string EntGas { get; set; }
        public string EntCost { get; set; }
        public string EntNote { get; set; }
        public int EntId { get; set; }
        public bool error { get; set; }
        public bool isVisibleLabel { get; set; }

        public static MileageTableDefination miles = new MileageTableDefination();

        public event PropertyChangedEventHandler PropertyChanged;

        public string EntError
        {
            get => entError;

            set
            {
                entError = value;
                var args = new PropertyChangedEventArgs(nameof(EntError));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public bool IsVisibleLabel
        {
            get => isVisibleLabel;

            set
            {
                isVisibleLabel = value;
                var args = new PropertyChangedEventArgs(nameof(IsVisibleLabel));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public EditViewModel()
        {
            MileageTableDefination mileage = new MileageTableDefination();
            mileage = Application.Current.Properties["MileageData"] as MileageTableDefination;
            SDate = Convert.ToDateTime(mileage.Date);
            EntMiles = mileage.Miles.ToString();
            EntGas = mileage.Gas.ToString();
            EntCost = mileage.Price.ToString();
            if (mileage.Note != null)
                EntNote = mileage.Note.ToString();
            EntId = mileage.Id;
        }

        public Command UpdateMileageData => new Command(async () =>
        {
            int autoId = Convert.ToInt32(Application.Current.Properties["autoId"]);
            //Analytics.TrackEvent("Mileage Data in Enter Mileage: AutoId " + autoId);
            error = false;
            IsVisibleLabel = false;

            if (EntMiles == "0" || EntMiles == null)
            {
                error = true;
                IsVisibleLabel = true;
                EntError = "Miles Driven Must Be Numeric";
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(EntMiles, @"^[0-9]\d*(\.\d+)?$"))
            {
                error = true;
                IsVisibleLabel = true;
                EntError = "Miles Driven Must Be Numeric";
            }
            else if (Convert.ToDecimal(EntMiles) > 1000)
            {
                error = true;
                //IsVisibleLabel = true;
                EntError = "Miles Driven Since Last Fillup";
            }
            if (EntGas == "0" || EntGas == null)
            {
                error = true;
                IsVisibleLabel = true;
                EntError = "Gas Used Must Be Numeric";
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(EntGas, @"^[0-9]\d*(\.\d+)?$"))
            {
                error = true;
                IsVisibleLabel = true;
                EntError = "Gas Used Must Be Numeric";
            }
            if (EntCost == "0" || EntCost == null)
            {
                error = true;
                IsVisibleLabel = true;
                EntError = "Cost Must Be Numeric";
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(EntCost, @"^[0-9]\d*(\.\d+)?$"))
            {
                error = true;
                IsVisibleLabel = true;
                EntError = "Cost Must Be Numeric";
            }
            else if (Convert.ToDecimal(EntCost) > 300.00m)
            {
                error = true;
                IsVisibleLabel = true;
                EntError = "Cost Must Be Numeric And < $300";
            }

            if (!error)
            {
                miles.Date = SDate.Date;
                miles.StrDate = SDate.Date.ToString("MM/dd/yyyy");
                miles.Miles = Convert.ToDecimal(EntMiles);
                miles.Gas = Convert.ToDecimal(EntGas);
                miles.Id = EntId;
                miles.CarId = autoId;
                miles.MPG = Math.Round(Convert.ToDecimal(EntMiles) / Convert.ToDecimal(EntGas), 3);
                miles.Price = Convert.ToDecimal(EntCost);
                miles.Note = EntNote;
                MileageItemRepository mir = new MileageItemRepository();
                var results = await mir.UpdateMileageAsync(miles);
                if (results == 1)
                {
                    Application.Current.Properties["UpdatedData"] = miles;
                    var nav = MyNavigation.GetNavigation();
                    nav.PushAsync(new ViewMileage());
                }
            }
        });

        public Command Cancel => new Command(async () =>
        {
            var nav = MyNavigation.GetNavigation();
            nav.PushAsync(new ViewMileage());
        });
    }
}