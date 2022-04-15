using MileageManagerForms.DataAccess;
using MileageManagerForms.Database;
using MileageManagerForms.Utilities;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using MileageManagerForms.Views;
using System.ComponentModel;
using System.Collections.Generic;
using MvvmHelpers;
using Microsoft.AppCenter.Analytics;
using MileageManagerForms.Interfaces;

namespace MileageManagerForms.ViewModels
{
    public class MileageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public Mileage _selectedItem;
        private int dateCounter = 0;
        private int milesCounter = 0;
        private int gasCounter = 0;
        private int mpgCounter = 0;
        private int costCounter = 0;
        public INavigation _navigation { get; set; }

        //public ObservableCollection<Mileage> MyList = new ObservableCollection<Mileage>();
        //public ObservableCollection<Mileage> MileageFields { get { return MyList; } }
        private ObservableCollection<Mileage> MyList;
        public ObservableCollection<Mileage> MileageFields
        {
            get { return MyList; }
            set { MyList = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MileageViewModel()
        {
            if (Application.Current.Properties["SummaryMileageDetail"] != null)
            {
                var summaryDetail = Application.Current.Properties["SummaryMileageDetail"] as List<MileageTableDefination>;
                ShowSummaryDetail(summaryDetail);
            }
            else
            {
                GetDisplayData(false, "Date");
                Sort.SortOrder(MileageFields, i => i.StrDate, false);
            }
        }

        public void GetDisplayData(bool didDelete, string sortFunction)
        {
            MileageItemRepository repository = new MileageItemRepository();
            var response = repository.GetMileageData(Convert.ToInt32(Application.Current.Properties["autoId"]));

            //Clear the Items on the page to start with a clean slate each time
            if (didDelete)
            {
                MyList.Clear();
                didDelete = false;
            }
            MileageFields = new ObservableCollection<Mileage>();
            //MyList.Clear();
            foreach (var item in response.Result)
            {
                //Analytics.TrackEvent("Mileage Data View Mileage: carId " + item.CarId + " Gas = " + item.Gas + " Miles = " + item.Miles + " ID " + item.Id);
                               
                MyList.Add(new Mileage
                {
                    StrDate = Convert.ToDateTime(item.StrDate).ToString("MM/dd/yy"),
                    Gas = item.Gas,
                    Id = item.Id,
                    Miles = item.Miles,
                    MPG = item.MPG,
                    Price = item.Price,
                    Note = item.Note
                });
            };

            MileageFields = MyList;
        }

        public void ShowSummaryDetail(List<MileageTableDefination> detail)
        {
            MyList.Clear();
            MileageFields = new ObservableCollection<Mileage>();
            foreach (var item in detail)
            {
                //Analytics.TrackEvent("Mileage Data View Mileage: carId " + item.CarId + " Gas = " + item.Gas + " Miles = " + item.Miles + " ID " + item.Id);                            
                MyList.Add(new Mileage
                {
                    StrDate = Convert.ToDateTime(item.StrDate).ToString("MM/dd/yy"),
                    Gas = item.Gas,
                    Id = item.Id,
                    Miles = item.Miles,
                    MPG = item.MPG,
                    Price = item.Price,
                    Note = item.Note
                });
            };
            MileageFields = MyList;
            Application.Current.Properties["SummaryMileageDetail"] = null;
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
                
                //DependencyService.Get<IDeviceOrientationService>().ShowMsg(value);                          
            }
        }

        public void ProcessResponse(int results, Mileage value)
        {
            MileageTableDefination mObject = new MileageTableDefination();
            Mileage data = value;
            mObject.CarId = 1;
            mObject.Id = Convert.ToInt32(data.Id);
            mObject.Date = Convert.ToDateTime(data.Date);
            mObject.Miles = Convert.ToDecimal(data.Miles);
            mObject.Gas = Convert.ToDecimal(data.Gas);
            mObject.MPG = Convert.ToDecimal(data.MPG);
            mObject.Price = Convert.ToDecimal(data.Price);
            mObject.Note = data.Note;

            if (results == 0)
            {
                Application.Current.Properties["MileageData"] = mObject;
                var nav = MyNavigation.GetNavigation();
                nav.PushAsync(new EditMileage());
            }
            if (results == 1)
            {
                MileageItemRepository repository = new MileageItemRepository();
                repository.DeleteMileageEntry(mObject);
                GetDisplayData(true, "Nothing");
                
            }
        }

        public ObservableCollection<Mileage> SortDate()
        {
            dateCounter = Convert.ToInt32(Application.Current.Properties["DateCounter"]);
            if (dateCounter % 2 == 0)
            {
                Sort.SortOrder(MyList, i => i.StrDate, false);
            }
            else
            {
                Sort.SortOrder(MyList, i => i.StrDate, true);
            }
            dateCounter++;
            Application.Current.Properties["DateCounter"] = dateCounter;
            return MyList;
        }

        public ObservableCollection<Mileage> SortMiles()
        {
            milesCounter = Convert.ToInt32(Application.Current.Properties["MilesCounter"]);
            if (milesCounter % 2 == 0)
            {
                Sort.SortOrder(MyList, i => i.Miles, false);
            }
            else
            {
                Sort.SortOrder(MyList, i => i.Miles, true);
            }
            milesCounter++;
            Application.Current.Properties["MilesCounter"] = milesCounter;
            return MyList;
        }

        public ObservableCollection<Mileage> SortGas()
        {
            gasCounter = Convert.ToInt32(Application.Current.Properties["GasCounter"]);
            if (gasCounter % 2 == 0)
            {
                Sort.SortOrder(MyList, i => i.Gas, false);
            }
            else
            {
                Sort.SortOrder(MyList, i => i.Gas, true);
            }
            gasCounter++;
            Application.Current.Properties["GasCounter"] = gasCounter;
            return MyList;
        }

        public ObservableCollection<Mileage> SortMPG()
        {
            mpgCounter = Convert.ToInt32(Application.Current.Properties["MPGCounter"]);
            if (mpgCounter % 2 == 0)
            {
                Sort.SortOrder(MyList, i => i.MPG, false);
            }
            else
            {
                Sort.SortOrder(MyList, i => i.MPG, true);
            }
            mpgCounter++;
            Application.Current.Properties["MPGCounter"] = mpgCounter;
            return MyList;
        }

        public ObservableCollection<Mileage> SortCost()
        {
            costCounter = Convert.ToInt32(Application.Current.Properties["CostCounter"]);
            if (costCounter % 2 == 0)
            {
                Sort.SortOrder(MyList, i => i.Price, false);
            }
            else
            {
                Sort.SortOrder(MyList, i => i.Price, true);
            }
            costCounter++;
            Application.Current.Properties["CostCounter"] = costCounter;
            return MyList;
        }

        private async void EnterClick(object sender, EventArgs e)
        {
            await _navigation.PushAsync(new Views.EditMileage());
        }
    }
}

