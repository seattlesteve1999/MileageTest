using MileageManagerForms.DataAccess;
using MileageManagerForms.Utilities;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MileageManagerForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateCars : ContentPage
    {             
        public UpdateCars()
        {
            InitializeComponent();                      
        }       

        void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            var swtch = (Switch)sender;
            var human = (AutoWithSwitch)swtch.BindingContext;
            var id = human.Id;
            if (!Convert.ToBoolean(Application.Current.Properties["FirstToggle"]))
            {
                MileageItemRepository mileageItemRepository = new MileageItemRepository();
                var autoResults = mileageItemRepository.GetAuto2();
                foreach (var item in autoResults)
                {
                    if (item.IsDefault)
                    {
                        var resp = mileageItemRepository.UpdateAutoAsync(false, item.Id);
                    }
                }
                mileageItemRepository.UpdateAutoAsync(true, id);
                Application.Current.Properties["autoId"] = id;
                var nav = MyNavigation.GetNavigation();
                nav.PushAsync(new MainPage());
            }
            else
                Application.Current.Properties["FirstToggle"] = false;
        }
    }
}