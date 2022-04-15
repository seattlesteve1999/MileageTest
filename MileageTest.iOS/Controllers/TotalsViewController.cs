using MileageManagerForms.DataAccess;
using MileageManagerForms.Database;
using System;
using UIKit;

namespace MileageManagerForms.iOS.Controllers
{
    public partial class TotalsViewController : UIViewController
    {
        public TotalsViewController()
        {
        }

        protected TotalsViewController(IntPtr handle) : base(handle)
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

            tbYear.TextColor = UIColor.Black;
            tbHeading.TextColor = UIColor.Black;
            MileageItemRepository mir = new MileageItemRepository();
            System.Collections.Generic.List<AutoTableDefination> results = mir.GetAuto3(Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]));
            tbHeading.Text = results[0].CarDesc + " Totals";
            GetTotalData();
            // Perform any additional setup after loading the view, typically from a nib.
        }


        public async void GetTotalData()
        {
            MileageItemRepository repository = new MileageItemRepository();
            int autoId = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]);
            //System.Collections.Generic.List<MileageDisplayDefination> response = await repository.GetMileageDisplayData(autoId);
            var response = await repository.GetMileageData(autoId);
            decimal totalGas = 0;
            decimal totalMiles = 0;
            decimal totalPrice = 0;
            decimal rounded = 0;

            foreach (var item in response)
            {
                totalGas += item.Gas;
                totalMiles += item.Miles;
                totalPrice += item.Price;
            }
            tbGas.Text = totalGas.ToString("###,##0.000");
            tbMiles.Text = totalMiles.ToString("###,##0.0");
            tbCost.Text = Math.Round(totalPrice, 2).ToString("$ ,##,##0.00");
            if (totalMiles > 0 || totalGas > 0)
            {
                rounded = Math.Round((totalMiles / totalGas), 3);
                tbAvgPrice.Text = Math.Round((totalPrice / totalGas), 2).ToString("$ ,##0.00");
            }

            tbMPG.Text = rounded.ToString("#,##0.000");
        }

        partial void UIButton115315_TouchUpInside(UIButton sender)
        {
            GetTotalsByYear();
        }


        public async void GetTotalsByYear()
        {
            MileageItemRepository repository = new MileageItemRepository();
            int autoId = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]);
            var response = await repository.GetMileageData(autoId);
            decimal totalGas = 0;
            decimal totalMiles = 0;
            decimal totalPrice = 0;
            decimal rounded = 0;

            foreach (var item in response)
            {
                if (item.StrDate.Substring(6, 4) == tbYear.Text)
                {
                    totalGas += item.Gas;
                    totalMiles += item.Miles;
                    totalPrice += item.Price;
                }
            }
            tbGas.Text = totalGas.ToString("###,##0.000");
            tbMiles.Text = totalMiles.ToString("###,##0.0");
            tbCost.Text = Math.Round(totalPrice, 2).ToString("$ ,##,##0.00");
            if (totalMiles > 0 || totalGas > 0)
            {
                rounded = Math.Round((totalMiles / totalGas), 3);
                tbAvgPrice.Text = Math.Round((totalPrice / totalGas), 2).ToString("$ ,##0.00");

            }

            tbMPG.Text = rounded.ToString("#,##0.000");
        }

        partial void UIButton116238_TouchUpInside(UIButton sender)
        {
            GetTotalData();
        }

        partial void UIButton27046_TouchUpInside(UIButton sender)
        {
            DismissViewController(true, null);
        }
    }
}