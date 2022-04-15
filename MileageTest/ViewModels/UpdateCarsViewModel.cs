using Microsoft.AppCenter.Analytics;
using MileageManagerForms.DataAccess;
using MileageManagerForms.Database;
using MileageManagerForms.Interfaces;
using MileageManagerForms.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace MileageManagerForms.ViewModels
{
    public class UpdateCarsViewModel : INotifyPropertyChanged
    {        
        public AutoWithSwitch _selectedItem;
        MileageItemRepository repository = new MileageItemRepository();
        private ObservableCollection<AutoWithSwitch> MyList;
        public ObservableCollection<AutoWithSwitch> AutoData
        {
            get { return MyList; }
            set { MyList = value; }
        }       

        public event PropertyChangedEventHandler PropertyChanged;

        public UpdateCarsViewModel()
        {
            //Analytics.TrackEvent("Top of Constructor in UpdateCarsViewModel");
            GetDisplayData();                        
        }

        public void GetDisplayData()
        {
            MileageItemRepository repository = new MileageItemRepository();
            var response = repository.GetAuto2();

            ////MyList.Clear();
            AutoData = new ObservableCollection<AutoWithSwitch>();

            foreach (var item in response)
            {
                //Analytics.TrackEvent("Car Data GetDisplayData: carId " + item.Id + " Name = " + item.CarDesc + " Year = " + item.CarYear);

                //get data logic 

                MyList.Add(new AutoWithSwitch
                {
                    Id = item.Id,
                    Year = item.CarYear,
                    Name = item.CarDesc,
                    IsChecked = item.IsDefault                   
                });

                //Analytics.TrackEvent("In GetDisplayData in UpdateCarsViewModel Car Name = " + item.CarDesc + " IsChecked = " + item.IsDefault);
            }
            
            AutoData = MyList;
            AutoData.SortOrder(i => i.IsChecked, false);
            AutoData.SortOrder(i => i.Name, false);
            AutoData.SortOrder(i => i.Year, false);         
        }

        public AutoWithSwitch SelectedItem
        {
            get
            {
                Analytics.TrackEvent("UpdateCarViewModel - SelectedItem - get");
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                Analytics.TrackEvent("UpdateCarViewModel - SelectedItem - set");
                if (_selectedItem == null)
                    return;
                DisplayAlerts(value);
            }
        }

        public async void DisplayAlerts(AutoWithSwitch value)
        {
            Analytics.TrackEvent("UpdateCarViewModel - DisplayAlerts top");

            Application.Current.Properties["DeleteData"] = value;            
            DependencyService.Get<IMessage>().LongAlert("Stupid");

            Analytics.TrackEvent("UpdateCarViewModel - DisplayAlerts Back From Call");
        }

        public async void DeleteCar()
        {
            AutoWithSwitch data = Application.Current.Properties["DeleteData"] as AutoWithSwitch;
            AutoTableDefination data2 = new AutoTableDefination();
            data2.Id = Convert.ToInt32(data.Id);
            await repository.DeleteCar(data2);       
            GetDisplayData();
            Sort.SortOrder(AutoData, i => i.Name, false);
        }       
    }
}