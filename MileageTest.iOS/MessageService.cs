using MileageManagerForms.DataAccess;
using MileageManagerForms.Interfaces;
using MileageManagerForms.ViewModels;
using UIKit;
using Xamarin.Forms;
using Foundation;
using Microsoft.AppCenter.Analytics;

[assembly: Dependency(typeof(MileageTest.iOS.MessageService))]
namespace MileageTest.iOS
{
    public class MessageService : IMessage
    {        
        NSTimer alertDelay;
        UIAlertController alert;

        public void ShowMsg(Mileage value)
        {
                       
        }

        public void LongAlert(string message)
        {
            ShowAlert(message);
        }
        public void ShortAlert(string message)
        {
            ShowAlert(message);
        }

        void ShowAlert(string message)
        {
            Analytics.TrackEvent("MessageService - ShowAlert top");
            UpdateCarsViewModel ucvm = new UpdateCarsViewModel();
            //alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            //{
            //    dismissMessage();
            //});
            alert = UIAlertController.Create(null, "Action", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Delete", UIAlertActionStyle.Default,UIAlertAction => ucvm.DeleteCar()));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
            Analytics.TrackEvent("MessageService - ShowAlert bottom");
        }

        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
        //var alert = new UIAlertView()
        //{
        //    Title = "Actions",
        //};
        //alert.AddButton("Edit");
        //alert.AddButton("Delete");
        //alert.AddButton("Cancel");
        //alert.Show();
        //alert.Clicked += (s, b) =>
        //{
        //    if (b.ButtonIndex == 0)
        //    {
        //        mvm.ProcessResponse(0, value);
        //    }
        //    if (b.ButtonIndex == 1)
        //    {
        //        mvm.ProcessResponse(1, value);
        //    }
        //};
    }
}