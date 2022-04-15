using Microsoft.AppCenter.Analytics;
using MileageManagerForms.DataAccess;
using MileageManagerForms.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace MileageManagerForms.ViewModels
{
    internal class MainMenuViewModel : INotifyPropertyChanged
    {
        public int _id;
        public AutoWithSwitch _selectedItem;       
        private ObservableCollection<AutoWithSwitch> MyList;
        public ObservableCollection<AutoWithSwitch> AutoData
        {
            get { return MyList; }
            set { MyList = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public MainMenuViewModel()
        {
            GetAutoData();            
        }

        public void GetAutoData()
        {
            AutoData = new ObservableCollection<AutoWithSwitch>();
            MileageItemRepository repo = new MileageItemRepository();
            var autoResults = repo.GetAuto2();
            
            foreach (var item in autoResults)
            {
                if (item.IsDefault)
                    item.CarDesc = "**"+item.CarDesc;

                MyList.Add(new AutoWithSwitch
                {
                    Name = item.CarDesc,
                });                                   
            }
            AutoData = MyList;
            AutoData.SortOrder(i => i.Name, true);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AutoWithSwitch SelectedItem
        {
            get
            {
                return _selectedItem;
                Analytics.TrackEvent("MainMenuViewModel - SelectedItem - get");
            }
            set
            {
                _selectedItem = value;
                Analytics.TrackEvent("MainMenuViewModel - SelectedItem - set");
                if (_selectedItem == null)
                    return;
                Application.Current.Properties["autoId"] = Id;
            }
        }
    }    
}
