using MileageManagerForms.DataAccess;
using MileageManagerForms.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MileageManagerForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMileageDetail : ContentPage
    {       
        public ViewMileageDetail()
        {
            InitializeComponent();
            BindingContext = new MileageDetailViewModel();            
        }      
    }
}