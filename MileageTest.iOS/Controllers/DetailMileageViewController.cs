using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using MileageManagerForms.DataAccess;

namespace MileageManagerForms.iOS
{
    public partial class DetailMileageViewController : UIViewController
    {
        public DateTime dateIn;
        readonly UIWindow Window = new UIWindow();
        readonly UITableView tableView = new UITableView(UIScreen.MainScreen.Bounds);

        public DetailMileageViewController()
        {
            ViewDidLoad();
        }

        public DetailMileageViewController(DateTime date) : base("DetailMileageViewController1", null)
        {
        }

        public DetailMileageViewController(IntPtr handle) : base(handle)
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
            dateIn = App.summaryDate;
            GetMileageData(dateIn);
        }

        public async void GetMileageData(DateTime date)
        {
            NSIndexPath indexPath = new NSIndexPath();
            MileageItemRepository repository = new MileageItemRepository();
            int month = date.Month;
            int year = date.Year;

            int autoId = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]);
            var response = await repository.GetMileageDisplayDataByDate(autoId, month.ToString(), year);
            List<Mileage> resp = new List<Mileage>();
            Mileage result = new Mileage();
            foreach (var item in response)
            {
                result = new Mileage
                {
                    Date = Convert.ToDateTime(item.StrDate),
                    Gas = item.Gas,
                    Id = item.Id,
                    Miles = item.Miles,
                    MPG = item.MPG,
                    Price = item.Price
                };
                resp.Add(result);
            }

            resp.Sort((x, y) => x.Date.CompareTo(y.Date));
            resp.Reverse();
            tableView.Source = new DetailTableSource(resp);
            tableView.BackgroundColor = UIColor.FromRGB(105, 112, 229);
            this.Add(tableView);
        }
    }
}