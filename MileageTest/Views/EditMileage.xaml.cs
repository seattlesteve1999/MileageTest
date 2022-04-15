using MileageManagerForms.Database;
using MileageManagerForms.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MileageManagerForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMileage : ContentPage
    {
        public string EntError { get; set; }

        public EditMileage()
        {
            InitializeComponent();
            var binding_context = (BindingContext as EditViewModel);
            if (binding_context != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    EntError = binding_context.EntError;
                });
            }
        }
    }
}