using MileageManagerForms.DataAccess;
using MileageManagerForms.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MileageManagerForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMileage : ContentPage
    {
        ObservableCollection<Mileage> MyList = new ObservableCollection<Mileage>();
        public ObservableCollection<Mileage> MileageFields { get { return MyList; } }
        MileageViewModel mvm = new MileageViewModel();

        public ViewMileage()
        {
            InitializeComponent();
            BindingContext = new MileageViewModel();
        }
      
        private void SortByDate(object sender, EventArgs e)
        {
            MileageView.ItemsSource = null;
            var sortedDate = mvm.SortDate();
            MileageView.ItemsSource = sortedDate;                      
        }

        private void SortByMiles(object sender, EventArgs e)
        {
            MileageView.ItemsSource = null;
            var sortedMiles = mvm.SortMiles();
            MileageView.ItemsSource = sortedMiles;
        }

        private async void SortByGas(object sender, EventArgs e)
        {
            MileageView.ItemsSource = null;
            var sortedGas = mvm.SortGas();
            MileageView.ItemsSource = sortedGas;
        }
        private async void SortByMPG(object sender, EventArgs e)
        {
            MileageView.ItemsSource = null;
            var sortedMPG = mvm.SortMPG();
            MileageView.ItemsSource = sortedMPG;
        }
        private async void SortByCost(object sender, EventArgs e)
        {
            MileageView.ItemsSource = null;
            var sortedCost = mvm.SortCost();
            MileageView.ItemsSource = sortedCost;
        }
    }
}