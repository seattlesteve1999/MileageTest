using Microsoft.AppCenter.Crashes;
using MileageManagerForms.DataAccess;
using MileageManagerForms.Database;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MileageManagerForms.ViewModels
{    
    public class EntryViewModel : INotifyPropertyChanged
    {
        public string entError { get; set; }
        public string entResults { get; set; }
        public string entMPG { get; set; }
        public DateTime SDate { get; set; }
        public string EntMiles { get; set; }
        public string EntGas { get; set; }
        public string EntCost { get; set; }
        public string EntNote { get; set; }
        public bool error { get; set; }
        public bool isVisibleLabel { get; set; }

        public EntryViewModel()
        {
            SDate = DateTime.Now;           
        }

        public string EntResults
        {
            get => entResults;

            set
            {
                entResults = value;
                var args = new PropertyChangedEventArgs(nameof(EntResults));
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

        public string EntError
        {
            get => entError;
                       
            set { entError = value;
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

        public void AddMileageData(DateTime SDate, string EntMiles, string EntGas, string EntCost, string EntNote)
        {
            int autoId = Convert.ToInt32(Application.Current.Properties["autoId"]);
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
                IsVisibleLabel = true;
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
                //Analytics.TrackEvent("Mileage Data in Enter Mileage: AutoId " + autoId);

                MileageTableDefination mileage = new MileageTableDefination
                {
                    CarId = autoId,
                    Date = SDate.Date,
                    StrDate = SDate.Date.ToString("MM/dd/yyyy"),
                    Gas = Convert.ToDecimal(EntGas),
                    Miles = Convert.ToDecimal(EntMiles),
                    Price = Convert.ToDecimal(EntCost),
                    MPG = Math.Round(Convert.ToDecimal(EntMiles) / Convert.ToDecimal(EntGas), 3),
                    Note = EntNote
                };
                //Analytics.TrackEvent("Mileage Data Going in: carId " + mileage.CarId + " StrDate = " + mileage.StrDate + " Gas = " + mileage.Gas + " Miles = " + mileage.Miles + " Price = " + mileage.Price + " MPG = " + mileage.MPG + " Note = " + mileage.Note);                           

                //Xamarin.Forms.Application.Current.Properties["EntryDate"] = dateString;
                //Xamarin.Forms.Application.Current.Properties["EntryMiles"] = EntMiles.Text;

                try
                {
                    MileageItemRepository repository = new MileageItemRepository();
                    var results = repository.AddMileageData(mileage);
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }

                IsVisibleLabel = false;
                EntMPG = Math.Round(Convert.ToDecimal(EntMiles) / Convert.ToDecimal(EntGas), 3).ToString();
                EntResults = "Entry Successful";                
            }            
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