using MileageManagerForms.Interfaces;
using DependencyAttribute = Xamarin.Forms.DependencyAttribute;
using MileageManagerForms.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(MileageManagerForms.iOS.Utilities.DeviceOrientationService))]
namespace MileageManagerForms.iOS.Utilities
{
    public class DeviceOrientationService : IDeviceOrientationService
    {
        //public Command RefreshCommand()
        //{
        //    MileageViewModel view = new MileageViewModel();
        //    Command refreshCommand = view.RefreshCommand;
        //    return refreshCommand;  
        //}

        MileageViewModel IDeviceOrientationService.SelectedItem()
        {
            MileageViewModel view = new MileageViewModel();
            //view.GetDisplayData();
            return null;
        }
    }
}