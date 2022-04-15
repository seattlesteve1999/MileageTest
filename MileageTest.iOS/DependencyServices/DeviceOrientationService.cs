using MileageManagerForms.Interfaces;
using UIKit;

namespace MileageManagerForms.iOS.DependencyServices
{
    public class DeviceOrientationService : IDeviceOrientationService
    {               
        public void ShowMsg(string msg)
        {
            var alert = new UIAlertView("Message", msg, null, "Ok", null);
            alert.Show();
        }        
    }
}