using Foundation;
using MileageManagerForms.iOS.Controllers;
using System;
using UIKit;

namespace MileageManagerForms.iOS.Controllers
{
    public partial class HelpMenuViewController : UIViewController
    {
        readonly UITableView tableView = new UITableView(UIScreen.MainScreen.Bounds);
        public UIWindow Window { get; set; }

        public HelpMenuViewController()
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected HelpMenuViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        partial void UIButton15073_TouchUpInside(UIButton sender)
        {
           
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            switch (segue.Identifier)
            {
                case "MileageEntrySegue":
                    Xamarin.Forms.Application.Current.Properties["Segue"] = "MileageEntrySegue";
                    break;
                case "MileageUpdateSegue":
                    Xamarin.Forms.Application.Current.Properties["Segue"] = "MileageUpdateSegue";
                    break;
                case "ViewMileageSegue":
                    Xamarin.Forms.Application.Current.Properties["Segue"] = "ViewMileageSegue";
                    break;
                case "MileageSummarySegue":
                    Xamarin.Forms.Application.Current.Properties["Segue"] = "MileageSummarySegue";
                    break;
                case "TotalStatsSegue":
                    Xamarin.Forms.Application.Current.Properties["Segue"] = "TotalStatsSegue";
                    break;
                case "iCloutSegue":
                    Xamarin.Forms.Application.Current.Properties["Segue"] = "iCloutSegue";
                    break;
            }
        }
    }
}