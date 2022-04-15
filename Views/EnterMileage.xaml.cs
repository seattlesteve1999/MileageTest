using Microsoft.AppCenter.Analytics;
using MileageManagerForms.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MileageManagerForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterMileage : ContentPage
    {
        public string EntError { get; set; }
        public string EntMPG { get; set; }
        public string EntResults { get; set; }

        public EnterMileage()
        {
            InitializeComponent();

            var binding_context = (BindingContext as EntryViewModel);
            if (binding_context != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    EntError = binding_context.EntError;
                    EntMPG = binding_context.EntMPG;
                    EntResults = binding_context.EntResults;
                });
            }            
        }

        void AddMileageData(object sender, EventArgs args)
        {
            Analytics.TrackEvent("Top of AddMileageData in EnterMileage.xaml.cs");

            Button button = (Button)sender;
            var imt = (Grid)button.Parent;
            var c2 = (DatePicker)imt.Children[1];
            var SDate = c2.Date;
            var c5 = (Entry)imt.Children[4];
            var EntMiles = c5.Text;
            var c7 = (Entry)imt.Children[6];
            var EntGas = c7.Text;
            var c9 = (Entry)imt.Children[8];
            var EntCost = c9.Text;
            var c11 = (Entry)imt.Children[10];
            var EntNote = c11.Text;
            //var c14 = (Entry)imt.Children[14];
            //var EntNote = c14.Text;

            Analytics.TrackEvent("Top of AddMileageData in EnterMileage.xaml.cs EntMiles = " + EntMiles + " EntGas = " + EntGas);

            EntryViewModel evm = (EntryViewModel)BindingContext;
            evm.AddMileageData(SDate, EntMiles, EntGas, EntCost, EntNote);
        }
    }
}