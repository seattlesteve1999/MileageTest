using MileageManagerForms.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MileageManagerForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TotalStats : ContentPage
    {        
        public string CarDesc { get; set; }
        public string EntMiles { get; set; }
        public string EntGas { get; set; }
        public string EntMPG { get; set; }
        public string EntAvgCost { get; set; }
        public string EntTotalCost { get; set; }       

        public TotalStats()
        {
            //App.Current.Properties["EnterCarFirstTime"] = true;           
            InitializeComponent();
            var binding_context = (BindingContext as TotalStatsViewModel);
            if (binding_context != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    CarDesc = binding_context.CarDesc;
                    EntMiles = binding_context.EntMiles;
                    EntGas = binding_context.EntGas;
                    EntMPG = binding_context.EntMPG;
                    EntAvgCost = binding_context.EntAvgCost;
                    EntTotalCost = binding_context.EntTotalCost;
                });
            }
        }

        void YearlyTotals(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            var imt = (Grid)button.Parent;
            var c = (Entry)imt.Children[13];
            var Year = c.Text;
            TotalStatsViewModel tvm = (TotalStatsViewModel)BindingContext;
            tvm.GetYearlyTotals(Year);
        }

        void GrandTotals(object sender, EventArgs args)
        {
            TotalStatsViewModel tvm = (TotalStatsViewModel)BindingContext;
            tvm.GrandTotals();
        }
    }
}   