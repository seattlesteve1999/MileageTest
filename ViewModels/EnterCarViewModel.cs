using MileageManagerForms.DataAccess;
using MileageManagerForms.Database;
using MileageManagerForms.Utilities;
using MileageManagerForms.Views;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Microsoft.AppCenter.Analytics;

namespace MileageManagerForms.ViewModels
{
    public class EnterCarViewModel : INotifyPropertyChanged
    {
        public string entError { get; set; }
        public string entResults { get; set; }
        public string entYear { get; set; }        
        public string entName { get; set; }       
        public bool error { get; set; }
        public bool isVisibleLabel { get; set; }
        public bool isVisibleResults { get; set; }
        public bool isChecked { get; set; }        

        public EnterCarViewModel()
        {
            //Analytics.TrackEvent("In Constructor, EnterCarViewModel");            
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

        public string EntName
        {
            get => entName;

            set
            {
                entName = value;
                var args = new PropertyChangedEventArgs(nameof(EntName));
                PropertyChanged?.Invoke(this, args);
            }
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

        public bool IsVisibleResults
        {
            get => isVisibleResults;

            set
            {
                isVisibleResults = value;
                var args = new PropertyChangedEventArgs(nameof(IsVisibleResults));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public bool IsChecked
        {
            get => isChecked;

            set
            {
                isChecked = value;
                var args = new PropertyChangedEventArgs(nameof(IsChecked));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public void AddCarData(string EntYear, string EntName)
        {
            //Analytics.TrackEvent("Top of AddCarData. EntYear = " + EntYear + " EntName = " + EntName);

            bool errorFound = false;
            IsVisibleLabel = false;
            IsVisibleResults = false;
            if (EntYear == string.Empty || EntYear == null)
            {
                IsVisibleLabel = true;
                EntError = "You Must Enter Year";
                errorFound = true;
            }
            else if (!errorFound)
            {
                Regex regex = new Regex(@"^[1-9]\d{3,}$");
                Match match = regex.Match(EntYear);
                if (!match.Success && !errorFound)
                {
                    IsVisibleLabel = true;
                    EntError = "Year Must Be 4 Numbers";
                    errorFound = true;
                }
                else if ((Convert.ToInt32(EntYear) < 1900) && !errorFound)
                {
                    IsVisibleLabel = true;
                    EntError = "Car Year is Less Than 1900";
                    errorFound = true;
                }
            }
            if (EntName == null && !errorFound)
            {
                IsVisibleLabel = true;
                EntError = "You Must Enter a Car Name";
                errorFound = true;
            }

            //Analytics.TrackEvent("In AddCarData. errorFound = " + errorFound);

            if (!errorFound)
            {
                var isDefault = false;
                Analytics.TrackEvent("In AddCarData IsChecked = " + IsChecked);
                if (IsChecked)
                {
                    isDefault = true;
                }
                else
                {
                    isDefault = false;
                }
                MileageItemRepository repository = new MileageItemRepository();
                if (IsChecked)
                {
                    var autoResults = repository.GetAuto2();
                    foreach (var item in autoResults)
                    {
                        if (item.IsDefault)
                        {
                            var resp = repository.UpdateAutoAsync(false, item.Id);
                        }
                    }
                    App.Current.Properties["processedChange"] = false;
                }
                Analytics.TrackEvent("In AddCarData IsDefault = " + isDefault);
                AutoTableDefination data = new AutoTableDefination()
                {
                    IsDefault = isDefault,
                    CarYear = EntYear,
                    CarDesc = EntName
                };

                int results = repository.AddAutoData(data).Result;

                //Analytics.TrackEvent("In AddCarData Results from AddAutoData. results = " + results);

                if (results == 1)
                {
                    IsVisibleResults = true;
                    EntResults = "Entered Successfully";
                    if (Convert.ToBoolean(App.Current.Properties["firstAutoEntered"]))
                    {
                        App.Current.Properties["autoId"] = 1;
                        App.Current.Properties["firstAutoEntered"] = false;
                    }

                    //var nav = MyNavigation.GetNavigation();
                    //nav.PushAsync(new UpdateCars());
                }
                else
                {
                    IsVisibleResults = true;
                    EntResults = "Error, Please Try Again";
                }
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