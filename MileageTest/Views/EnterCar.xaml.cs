using Microsoft.AppCenter.Analytics;
using MileageManagerForms.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MileageManagerForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterCar : ContentPage
    {
        public string EntYear { get; set; }
        public string EntName { get; set; }
        public string EntError { get; set; }
        public string EntResults { get; set; }
        public bool IsChecked { get; set; }

        public EnterCar()
        {
            //App.Current.Properties["EnterCarFirstTime"] = true;           
            InitializeComponent(); 
            //BindingContext = new EnterCarViewModel();
            var binding_context = (BindingContext as EnterCarViewModel);
            if (binding_context != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    EntYear = binding_context.EntYear;
                    EntName = binding_context.EntName;
                    EntError = binding_context.EntError;
                    EntResults = binding_context.EntResults;
                    IsChecked = binding_context.IsChecked;
                });
            }
        }

        void AddCarData(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            var imt = (Grid)button.Parent;
            var c = (Entry)imt.Children[3];
            var EntYear = c.Text;
            var c2 = (Entry)imt.Children[5];
            var EntName = c2.Text;

            Analytics.TrackEvent("AddCarData in EnterCar.xaml.cs EntYear = " + EntYear + " EntName = " + EntName);
            EnterCarViewModel enterCarViewModel = (EnterCarViewModel)BindingContext;
            enterCarViewModel.AddCarData(EntYear, EntName);
        }
    }
}   