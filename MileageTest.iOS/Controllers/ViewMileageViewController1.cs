using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MileageManagerForms.iOS;
using MileageManagerForms.DataAccess;
using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;

namespace MileageManagerForms.iOS.Controllers
{
    public partial class ViewMileageViewController1 : UIViewController
    {
        readonly UITableView tableView = new UITableView(UIScreen.MainScreen.Bounds);
        readonly UIWindow Window = new UIWindow();

        public ViewMileageViewController1()
        {
            
        }

        protected ViewMileageViewController1(IntPtr handle) : base(handle)
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
            GetMileageData();
        }

        public async void GetMileageData()
        {            
            MileageItemRepository repository = new MileageItemRepository();

            //var response = await repository.GetAllMileageDisplayData();  //Change 0 to position 
            int autoId = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]);

            //Analytics.TrackEvent("Mileage Data View M ileage: AutoId " + autoId);

            var response = await repository.GetMileageData(autoId);

            List<Mileage> resp = new List<Mileage>();
            Mileage result = new Mileage();
            foreach (var item in response)
            {               
                //Analytics.TrackEvent("Mileage Data View Mileage: carId " + item.CarId + " Gas = " + item.Gas + " Miles = " + item.Miles + " ID " + item.Id);             

                result = new Mileage
                {
                    Date = Convert.ToDateTime(item.StrDate),
                    Gas = item.Gas,
                    Id = item.Id,
                    Miles = item.Miles,
                    MPG = item.MPG,
                    Price = item.Price,
                    Note = item.Note
                };
                resp.Add(result);
            }

            resp.Sort((x, y) => x.Date.CompareTo(y.Date));
            resp.Reverse();

            tableView.Source = new MyTableSource(resp);
            tableView.BackgroundColor = UIColor.FromRGB(105, 112, 229);
            //tableView.RowHeight = UITableView.AutomaticDimension;
            //tableView.EstimatedRowHeight = 70;
            this.Add(tableView);
        }
    }
}