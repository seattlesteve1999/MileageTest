using MileageManagerForms.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MileageManagerForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryMileage : ContentPage
    {
        SummaryViewModel svm = new SummaryViewModel();

        public SummaryMileage()
        {
            InitializeComponent();          
        }

        private void SortByDate(object sender, EventArgs e)
        {
            MileageView.ItemsSource = null;
            var sortedDate = svm.SortDate();
            MileageView.ItemsSource = sortedDate;
        }

        private void SortByMiles(object sender, EventArgs e)
        {
            MileageView.ItemsSource = null;
            var sortedMiles = svm.SortMiles();
            MileageView.ItemsSource = sortedMiles;
        }

        private async void SortByGas(object sender, EventArgs e)
        {
            MileageView.ItemsSource = null;
            var sortedGas = svm.SortGas();
            MileageView.ItemsSource = sortedGas;
        }
        private async void SortByMPG(object sender, EventArgs e)
        {
            MileageView.ItemsSource = null;
            var sortedMPG = svm.SortMPG();
            MileageView.ItemsSource = sortedMPG;
        }
        private async void SortByCost(object sender, EventArgs e)
        {
            MileageView.ItemsSource = null;
            var sortedCost = svm.SortCost();
            MileageView.ItemsSource = sortedCost;
        }
    }
}