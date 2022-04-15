using MileageManagerForms.DataAccess;
using MileageManagerForms.Utilities;
using MileageManagerForms.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MileageManagerForms
{
    public partial class MainPage : ContentPage
    {                       
        public MainPage()
        {
            InitializeComponent();
            //BindingContext = new MainMenuViewModel();
           
            MileageItemRepository repo = new MileageItemRepository();
            var autoResults = repo.GetAuto2();

            if (autoResults.Count > 0)
            {                
                foreach (var item in autoResults)
                {                                                         
                    if (item.IsDefault)
                    {
                        Application.Current.Properties["autoId"] = item.Id;
                    }
                }                
            }
            else
            {
                Navigation.PushAsync(new Views.EnterCar());
            }        
        }
         
        //private async void EnterClick(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Views.EnterMileage());
        //}

        //private async void ViewClick(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Views.ViewMileage());
        //}

        //private async void SummaryClick(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Views.SummaryMileage());
        //}

        private async void AddCarClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.EnterCar());
        }

        private async void UpdateCarClick(object sender, EventArgs e)
        {
            Application.Current.Properties["FirstToggle"] = true;
            await Navigation.PushAsync(new Views.UpdateCars());
        }

        //private async void TotalsClick(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Views.TotalStats());
        //}

        //private void iCloudClick(object sender, EventArgs e)
        //{
        //    DisplayAlert(
        //      "iCloudClick Mileage Clickedr",
        //      "Would you like to call " + "?",
        //      "Yes",
        //      "No");
        //}

        //private void HelpClick(object sender, EventArgs e)
        //{
        //    DisplayAlert(
        //      "HelpClick Mileage Clickedr",
        //      "Would you like to call " + "?",
        //      "Yes",
        //      "No");
        //}
    }
}
