using Foundation;
using MileageManagerForms.iOS;
using MileageManagerForms.DataAccess;
using System;
using UIKit;
using Microsoft.AppCenter.Crashes;
using MileageManagerForms.Database;

namespace MileageManagerForms.iOS
{
    public partial class EnterMileageViewController : UIViewController
    {
        public AppDelegate ThisApp
        {
            get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
        }

        public EnterMileageViewController(IntPtr handle) : base(handle)
        {           
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            MilesDriven.TextColor = UIColor.Black;
            GasUsed.TextColor = UIColor.Black;
            tbPrice.TextColor = UIColor.Black;
            tbNote.TextColor = UIColor.Black;

            // Perform any additional setup after loading the view, typically from a nib.
        }

        partial void BtnCalculate_TouchUpInside(UIButton sender)
        {
            var error = false;
            BtnCalculate.Enabled = false;
            var dateFormatter = new NSDateFormatter
            {
                DateFormat = "MM-dd-yyyy"
            };
            if (!Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["First"]))
            {
                if (Xamarin.Forms.Application.Current.Properties["EntryDate"].ToString() == dateFormatter.StringFor(dpDate.Date)
                    && Xamarin.Forms.Application.Current.Properties["EntryMiles"].ToString() == MilesDriven.Text)
                {
                    error = true;
                    tbError.Hidden = false;
                    tbError.Text = "Possible Duplicate";
                }
                BtnCalculate.Enabled = true;
            }
            if (!error)
                AddMileageData();
            Xamarin.Forms.Application.Current.Properties["First"] = false;
        }

        public async void AddMileageData()
        {            
            MilesPerGallon.Enabled = false;
            tbError.Hidden = true;
            bool error = false;

            if (MilesDriven.Text == "0" || MilesDriven.Text == "" || MilesDriven.Text == "Miles")
            {
                error = true;
                tbError.Hidden = false;
                tbError.Text = "Miles Driven Must Be Numeric";
            }
            else if (Convert.ToDecimal(MilesDriven.Text) > 1000)
            {
                error = true;
                tbError.Hidden = false;
                tbError.Text = "Miles Driven Since Last Fillup";
            }
            else if (GasUsed.Text == "0" || GasUsed.Text == "" || GasUsed.Text == "Gas")
            {
                error = true;
                tbError.Hidden = false;
                tbError.Text = "Gas Used Must Be Numeric";
            }
            else if ((tbPrice.Text == "0" || tbPrice.Text == "" || tbPrice.Text == "Cost" || Convert.ToDecimal(tbPrice.Text) > 300.00m))
            {
                error = true;
                tbError.Hidden = false;
                tbError.Text = "Cost Must Be Numeric And < $300";
            }

            if (!error)
            {
                BtnCalculate.Enabled = false;
                NSDateFormatter dateFormatter = new NSDateFormatter
                {
                    DateFormat = "MM-dd-yyyy"
                };
                string dateString = dateFormatter.StringFor(dpDate.Date);
                int autoId = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]);

                //Analytics.TrackEvent("Mileage Data in Enter Mileage: AutoId " + autoId);

                MileageTableDefination mileage = new MileageTableDefination
                {
                    CarId = autoId,
                    StrDate = dateString,                   
                    Gas = Convert.ToDecimal(GasUsed.Text),
                    Miles = Convert.ToDecimal(MilesDriven.Text),
                    Price = Convert.ToDecimal(tbPrice.Text),
                    MPG = Math.Round(Convert.ToDecimal(MilesDriven.Text) / Convert.ToDecimal(GasUsed.Text), 3),
                    Note = tbNote.Text
                };                

                //Analytics.TrackEvent("Mileage Data Going in: carId " + mileage.CarId + " StrDate = " + mileage.StrDate + " Gas = " + mileage.Gas + " Miles = " + mileage.Miles + " Price = " + mileage.Price + " MPG = " + mileage.MPG + " Note = " + mileage.Note);                

                MilesPerGallon.Text = Math.Round(Convert.ToDecimal(MilesDriven.Text) / Convert.ToDecimal(GasUsed.Text), 3).ToString();

                Xamarin.Forms.Application.Current.Properties["EntryDate"] = dateString;
                Xamarin.Forms.Application.Current.Properties["EntryMiles"] = MilesDriven.Text;

                try
                {
                    MileageItemRepository repository = new MileageItemRepository();                    
                    var results = await repository.AddMileageData(mileage);                   
                }
                catch(Exception ex)
                {
                    Crashes.TrackError(ex);
                }

                tbResults.Text = "Entry Successful";
                View.EndEditing(true);
            }
            BtnCalculate.Enabled = true;
        }

        partial void UIButton18419_TouchUpInside(UIButton sender)
        {
            DismissViewController(true, null);
        }
    }
}