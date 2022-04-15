using MileageManagerForms.DataAccess;
using MileageManagerForms.Utilities;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using MvvmHelpers;
using MileageManagerForms.Database;

namespace MileageManagerForms.ViewModels
{
    public class MileageDetailViewModel : BaseViewModel, INotifyPropertyChanged
    {       
        public ObservableCollection<Mileage> MyList = new ObservableCollection<Mileage>();
        public ObservableCollection<Mileage> MileageFields { get { return MyList; } }

        public MileageDetailViewModel()
        {
            var data = Application.Current.Properties["SummaryMileageDetail"];
            GetDisplayData(data);
            Sort.SortOrder(MyList, i => i.Date, false);                       
        }

        public void GetDisplayData(object response)
        {            
            var mileResponse = (List<MileageTableDefination>) response;
            foreach (Mileage item in mileResponse)
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
            };
        }        
    }
}
