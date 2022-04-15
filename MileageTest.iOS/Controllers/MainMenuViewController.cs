using MileageManagerForms.iOS;
using MileageManagerForms.iOS.Controllers;
using MileageManagerForms.DataAccess;
using System;
using System.Collections.Generic;
using UIKit;
using MileageManagerForms.Utilities;
using MileageManagerForms.Database;

namespace MileageManagerForms.iOS.Controllers
{
    public partial class MainMenuViewController : UIViewController
    {
        readonly UITableView tableView = new UITableView(UIScreen.MainScreen.Bounds);
        public UIWindow Window { get; set; }

        public MainMenuViewController()
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected MainMenuViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.           
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //**********************************************************************************************************************//
            //******** Turns out this is called twice so I have had to do some tricky logic to keep it processing correctly ********//
            //**********************************************************************************************************************//

            UILayoutGuide layoutGuide = new UILayoutGuide();
            View.AddLayoutGuide(layoutGuide);
            UILayoutGuide layoutGuide2 = new UILayoutGuide();
            View.AddLayoutGuide(layoutGuide2);
            UILayoutGuide container = new UILayoutGuide();
            View.AddLayoutGuide(container);

            layoutGuide.WidthAnchor.ConstraintEqualTo(layoutGuide2.WidthAnchor).Active = true;
            UILayoutGuide margins = View.LayoutMarginsGuide;

            container.LeadingAnchor.ConstraintEqualTo(margins.LeadingAnchor).Active = true;
            container.TrailingAnchor.ConstraintEqualTo(margins.TrailingAnchor).Active = true;
            container.TopAnchor.ConstraintEqualTo(TopLayoutGuide.GetBottomAnchor(), 20).Active = true;

            btnHelp.AutoresizingMask = UIViewAutoresizing.FlexibleBottomMargin;

            Xamarin.Forms.Application.Current.Properties["First"] = true;
            Xamarin.Forms.Application.Current.Properties["iCloudFirst"] = true;
            Xamarin.Forms.Application.Current.Properties["EntryMiles"] = "";
            Xamarin.Forms.Application.Current.Properties["EntryDate"] = "";
            Xamarin.Forms.Application.Current.Properties["Counter"] = 0;

            //AutoPicker pickerModel = new AutoPicker(); 
            //autoPicker.Model = pickerModel;
            //pickerModel.ValueChanged += (sender, e) =>
            //{
            //    Xamarin.Forms.Application.Current.Properties["processedChange"] = true;
            //    Xamarin.Forms.Application.Current.Properties["autoId"] = pickerModel.SelectedValue;
            //};

            if (Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["enter1stCar"]))
                EnterFirstCar();
            else
            {
                switch (Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["firstTime"]))
                {
                    //**********************************************************************************************************//
                    //******** Count is originally defined in AppDelegate.cs, good place to initialize global variables ********//
                    //**********************************************************************************************************//

                    case true:
                        Xamarin.Forms.Application.Current.Properties["Count"] = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["Count"]) + 1;
                        ProcessDefaultLogic(Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["Count"]));
                        break;
                    default:
                        ProcessingLogic();
                        break;
                }
            }
        }

        public void EnterFirstCar()
        {
            btnEnterMiles.Enabled = false;
            btnEnterMiles.Alpha = .3F;
            btnTotalStats.Enabled = false;
            btnTotalStats.Alpha = .3F;
            btnViewCar.Enabled = false;
            btnViewCar.Alpha = .3F;
            btnViewMiles.Enabled = false;
            btnViewMiles.Alpha = .3F;
            btnIcloud.Enabled = false;
            btnIcloud.Alpha = .3F;
            btnMileageSummary.Enabled = false;
            btnMileageSummary.Alpha = .3F;
            txtMessage.Text = "You Must Enter a Car First";
            txtMessage.Highlighted = true;
            txtMessage.HighlightedTextColor = UIColor.Black;
            txtMessage.Font = UIFont.BoldSystemFontOfSize(26);
            Xamarin.Forms.Application.Current.Properties["firstAutoEntered"] = true;
        }

        public void ProcessDefaultLogic(int count)
        {
            //*****************************************************************************************************************************************//
            //******** Had to check count and only call after the second attempt time to keep from setting the firstTime flag before it's time. *******//
            //******** If I let it run the first time and set the flag the page will not show requiring them so select a default                *******//
            //*****************************************************************************************************************************************//
            if (count > 1)
            {
                bool defaultCount = false;
                MileageItemRepository repository = new MileageItemRepository();
                var autoData = repository.GetAuto2();

                foreach (AutoTableDefination item in autoData)
                {
                    if (item.IsDefault)
                        defaultCount = true;
                }
                if (!defaultCount)
                {
                    btnEnterMiles.Enabled = false;
                    btnEnterMiles.Alpha = .3F;
                    btnTotalStats.Enabled = false;
                    btnTotalStats.Alpha = .3F;
                    btnEnterCar.Enabled = false;
                    btnEnterCar.Alpha = .3F;
                    btnViewMiles.Enabled = false;
                    btnViewMiles.Alpha = .3F;
                    btnIcloud.Enabled = false;
                    btnIcloud.Alpha = .3F;
                    btnMileageSummary.Enabled = false;
                    btnMileageSummary.Alpha = .3F;
                    txtMessage.Text = "        Select a Default Car";
                    txtMessage.Highlighted = true;
                    txtMessage.HighlightedTextColor = UIColor.Black;
                    txtMessage.Font = UIFont.BoldSystemFontOfSize(26);
                    //Xamarin.Forms.Application.Current.Properties["ChooseDefault"] = true;
                }
                Xamarin.Forms.Application.Current.Properties["processedChange"] = false;
                Xamarin.Forms.Application.Current.Properties["firstTime"] = false;
                int i = 0;
                foreach (AutoTableDefination item in autoData)
                {
                    if (item.IsDefault)
                    {
                        Xamarin.Forms.Application.Current.Properties["rowId"] = i;
                        autoPicker.Select(i, 0, false);
                        Xamarin.Forms.Application.Current.Properties["autoId"] = item.Id;
                    }
                    i++;
                }
                View.Add(autoPicker);
            }
        }

        public void ProcessingLogic()
        {
            MileageItemRepository repository = new MileageItemRepository();
            var autoData = repository.GetAuto2();

            if (!Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["firstTime"]))
            {
                //row = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["rowId"]);
                if (!Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["processedChange"]))
                {
                    int i = 0;

                    foreach (AutoTableDefination item in autoData)
                    {
                        if (item.IsDefault)
                        {
                            Xamarin.Forms.Application.Current.Properties["rowId"] = i;
                            autoPicker.Select(i, 0, false);
                            Xamarin.Forms.Application.Current.Properties["autoId"] = item.Id;
                        }
                        i++;
                    }
                }
                else
                {
                    int row = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["rowId"]);
                    autoPicker.Select(row, 0, false);
                }
            }
            else
            {
                Xamarin.Forms.Application.Current.Properties["processedChange"] = false;
                Xamarin.Forms.Application.Current.Properties["rowId"] = 0;
            }
            View.Add(autoPicker);
            //Xamarin.Forms.Application.Current.Properties["Count"] = 1;
            txtMessage.Text = "Scroll to View Other Cars";
            txtMessage.Highlighted = true;
            txtMessage.HighlightedTextColor = UIColor.Black;
            txtMessage.Font = UIFont.BoldSystemFontOfSize(15);
            txtMessage.TextAlignment = UITextAlignment.Center;
            btnEnterMiles.Enabled = true;
            btnTotalStats.Enabled = true;
            btnViewCar.Enabled = true;
            btnEnterCar.Enabled = true;
            btnViewMiles.Enabled = true;
            btnIcloud.Enabled = true;
            btnMileageSummary.Enabled = true;
        }


        partial void BtnViewMiles_TouchUpInside(UIButton sender)
        {
            //ViewMileageViewController vc = new ViewMileageViewController();           
        }

        partial void BtnEnterMiles_TouchUpInside(UIButton sender)
        {
            //throw new NotImplementedException();
        }

        partial void BtnTotalStats_TouchUpInside(UIButton sender)
        {
            //throw new NotImplementedException();
        }

        partial void UIButton105475_TouchUpInside(UIButton sender)
        {
            Xamarin.Forms.Application.Current.Properties["Backup"] = false;            
        }

        partial void BtnHelp_TouchUpInside(UIButton sender)
        {
            
        }
    }
}