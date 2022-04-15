using MileageManagerForms.DataAccess;
using MileageManagerForms.Database;
using MileageManagerForms.Utilities;
using MileageManagerForms.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
//using UIKit;
using Xamarin.Forms;

namespace MileageManagerForms.ViewModels
{
    public class SummaryViewModel : INotifyPropertyChanged
    {
        public Mileage _selectedItem;
        private int dateCounter = 0;
        private int milesCounter = 0;
        private int gasCounter = 0;
        private int mpgCounter = 0;
        private int costCounter = 0;

        public Label _date;
        public Label Date
        {    get { return _date; }
             set { _date = value; }
        }

        private ObservableCollection<Mileage> MySortedList;
        public ObservableCollection<Mileage> MileageSummaryFields
        {
            get { return MySortedList; }
            set { MySortedList = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SummaryViewModel()
        {           
            GetSummaryDisplayData(false);
            Sort.SortOrder(MileageSummaryFields, i => i.StrDate, false);
        }

        public ICommand SelectCommand { get; set; }

        public void GetSummaryDisplayData(bool didDelete)
        {
            //Clear the Items on the page to start with a clean slate each time
            if (didDelete)
            {
                MySortedList.Clear();
                didDelete = false;
            }

            MileageItemRepository repository = new MileageItemRepository();
            var response = repository.GetMileageData(Convert.ToInt32(Application.Current.Properties["autoId"]));
            List<Mileage> resp = new List<Mileage>();
            ObservableCollection<Mileage> MyList = new ObservableCollection<Mileage>();
            MileageSummaryFields = new ObservableCollection<Mileage>();

            foreach (var item in response.Result)
            {
                //Analytics.TrackEvent("Mileage Data View Mileage: carId " + item.CarId + " Gas = " + item.Gas + " Miles = " + item.Miles + " ID " + item.Id);             

                MyList.Add(new Mileage
                {
                    Date = Convert.ToDateTime(item.StrDate),
                    Gas = item.Gas,
                    Id = item.Id,
                    Miles = item.Miles,
                    MPG = item.MPG,
                    Price = item.Price,
                    Note = item.Note
                });
                //resp.Add(MySortedList);
            };
            ObservableCollection<Mileage> sortedResults = SortAndSummarize(MyList);
            
            //MySortedList = new ObservableCollection<Mileage>();
            //sortedResults.Sort((x, y) => x.Date.CompareTo(y.Date));
            //sortedResults.Reverse();
            for (int i = 0; i < sortedResults.Count; i++)
            {
                var mm = sortedResults[i].Date.Month;
                var yy = sortedResults[i].Date.Year;

                MySortedList.Add(new Mileage
                {
                    StrDate = mm + "/" + yy,
                    Gas = sortedResults[i].Gas,
                    Id = sortedResults[i].Id,
                    Miles = sortedResults[i].Miles,
                    MPG = sortedResults[i].MPG,
                    Price = sortedResults[i].Price,
                    Note = sortedResults[i].Note
                });
            }
            MileageSummaryFields = MySortedList;
        }


        public ObservableCollection<Mileage> SortAndSummarize(ObservableCollection<Mileage> SortedList)
        {
            ObservableCollection<Mileage> listOfData = new ObservableCollection<Mileage>();
            Mileage data = new Mileage();
            if (SortedList.Count == 0)
            {
                return new ObservableCollection<Mileage>();
            }
            else
            {
                decimal holdMiles = 0;
                decimal holdGas = 0;
                decimal holdPrice = 0;
                bool processing = false;
                int holdYear = 0;
                int holdMonth = 0;

                foreach (Mileage item in SortedList.OrderBy(c => c.Date.Year).ThenBy(c => c.Date.Month))
                {
                    holdYear = item.Date.Year;
                    holdMonth = item.Date.Month;
                    break;
                }

                foreach (Mileage item in SortedList.OrderBy(c => c.Date.Year).ThenBy(c => c.Date.Month))
                {                   
                    if (holdYear == item.Date.Year)
                    {
                        if (holdMonth == item.Date.Month)
                        {
                            data.Date = Convert.ToDateTime(item.Date.Month + "/" + item.Date.Year);
                            data.Miles += item.Miles;
                            data.Gas += item.Gas;
                            data.Price += item.Price;
                            holdMiles = data.Miles;
                            holdGas = data.Gas;
                            holdPrice = data.Price;
                            processing = true;
                        }
                        else
                        {
                            if (data.Miles != holdMiles)
                            {
                                if (!processing)
                                {
                                    data.Miles += holdMiles;
                                    data.Gas += holdGas;
                                    data.Price += holdPrice;
                                }
                                else
                                {
                                    data.Miles = holdMiles;
                                    data.Gas = holdGas;
                                    data.Price = holdPrice;
                                }
                            }
                            data.Date = Convert.ToDateTime(holdMonth + "/" + holdYear);
                            data.MPG = Math.Round(data.Miles / data.Gas, 3);
                            listOfData.Add(data);
                            data = new MileageTableDefination();
                            holdMonth = item.Date.Month;
                            holdYear = item.Date.Year;
                            holdMiles = item.Miles;
                            holdGas = item.Gas;
                            holdPrice = item.Price;
                            data.Miles = holdMiles;
                            data.Gas = holdGas;
                            data.Price = holdPrice;
                            processing = false;
                        }
                    }
                    else
                    {
                        if (data.Miles != holdMiles)
                        {
                            if (!processing)
                            {
                                data.Miles += holdMiles;
                                data.Gas += holdGas;
                                data.Price += holdPrice;
                            }
                            else
                            {
                                data.Miles = holdMiles;
                                data.Gas = holdGas;
                                data.Price = holdPrice;
                            }
                        }
                        data.Date = Convert.ToDateTime(holdMonth + "/" + holdYear);
                        data.MPG = Math.Round(data.Miles / data.Gas, 3);
                        listOfData.Add(data);
                        holdMonth = item.Date.Month;
                        holdYear = item.Date.Year;
                        holdMiles = item.Miles;
                        holdGas = item.Gas;
                        holdPrice = item.Price;
                        data = new MileageTableDefination
                        {
                            Miles = holdMiles,
                            Gas = holdGas,
                            Price = holdPrice
                        };
                        processing = false;
                    }
                }
                data.Date = Convert.ToDateTime(holdMonth + "/" + holdYear);
                data.Miles = holdMiles;
                data.Gas = holdGas;
                data.MPG = Math.Round(holdMiles / holdGas, 3);
                data.Price = Math.Round(holdPrice, 2);
                listOfData.Add(data);
                
                return listOfData;
            }
        }

        public Mileage SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

                //UIAlertView alert = new UIAlertView()
                //{
                //    Title = "Actions",
                //};
                //alert.AddButton("Detail");               
                //alert.AddButton("Cancel");
                //alert.Show();
                //alert.Clicked += (s, b) =>
                var answer = App.Current.MainPage.DisplayAlert("Action", "View Details", "Yes", "No").Result;
                if (answer == true)
                {
                    Mileage data = value;

                    var slash = data.StrDate.IndexOf('/');
                    var autoId = Convert.ToInt32(Application.Current.Properties["autoId"]);
                    MileageItemRepository repository = new MileageItemRepository();
                    var detailMileage = repository.GetMileageDisplayDataByDate(autoId, data.StrDate.Substring(0, slash), Convert.ToInt32(data.StrDate.Substring(slash + 1)));
                    if (detailMileage.IsCompleted)
                    {
                        Application.Current.Properties["SummaryMileageDetail"] = detailMileage.Result;
                        var nav = MyNavigation.GetNavigation();
                        nav.PushAsync(new ViewMileageDetail());
                    }
                }
            }
        }


        public ObservableCollection<Mileage> SortDate()
        {
            dateCounter = Convert.ToInt32(Application.Current.Properties["SrtDateCounter"]);
            if (dateCounter % 2 == 0)
            {
                Sort.SortOrder(MySortedList, i => i.StrDate, false);
            }
            else
            {
                Sort.SortOrder(MySortedList, i => i.StrDate, true);
            }
            dateCounter++;
            Application.Current.Properties["SrtDateCounter"] = dateCounter;
            return MySortedList;
        }

        public ObservableCollection<Mileage> SortMiles()
        {
            milesCounter = Convert.ToInt32(Application.Current.Properties["SrtMilesCounter"]);
            if (milesCounter % 2 == 0)
            {
                Sort.SortOrder(MySortedList, i => i.Miles, false);
            }
            else
            {
                Sort.SortOrder(MySortedList, i => i.Miles, true);
            }
            milesCounter++;
            Application.Current.Properties["SrtMilesCounter"] = milesCounter;
            return MySortedList;
        }

        public ObservableCollection<Mileage> SortGas()
        {
            gasCounter = Convert.ToInt32(Application.Current.Properties["SrtGasCounter"]);
            if (gasCounter % 2 == 0)
            {
                Sort.SortOrder(MySortedList, i => i.Gas, false);
            }
            else
            {
                Sort.SortOrder(MySortedList, i => i.Gas, true);
            }
            gasCounter++;
            Application.Current.Properties["SrtGasCounter"] = gasCounter;
            return MySortedList;
        }

        public ObservableCollection<Mileage> SortMPG()
        {
            mpgCounter = Convert.ToInt32(Application.Current.Properties["SrtMPGCounter"]);
            if (mpgCounter % 2 == 0)
            {
                Sort.SortOrder(MySortedList, i => i.MPG, false);
            }
            else
            {
                Sort.SortOrder(MySortedList, i => i.MPG, true);
            }
            mpgCounter++;
            Application.Current.Properties["SrtMPGCounter"] = mpgCounter;
            return MySortedList;
        }

        public ObservableCollection<Mileage> SortCost()
        {
            costCounter = Convert.ToInt32(Application.Current.Properties["SrtCostCounter"]);
            if (costCounter % 2 == 0)
            {
                Sort.SortOrder(MySortedList, i => i.Price, false);
            }
            else
            {
                Sort.SortOrder(MySortedList, i => i.Price, true);
            }
            costCounter++;
            Application.Current.Properties["SrtCostCounter"] = costCounter;
            return MySortedList;
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
