using System;
using System.Collections.Generic;
using UIKit;
using MileageManagerForms.Database;
using MileageManagerForms.DataAccess;

namespace MileageManagerForms.iOS.Controllers
{
    public partial class EditMileageViewController1 : UIViewController
    {
        public int Id { get; set; }
        public static MileageTableDefination miles = new MileageTableDefination();

        public EditMileageViewController1()
        {
        }

        public EditMileageViewController1(MileageTableDefination mileage)
        {
            miles.Date = mileage.Date;
            miles.Miles = mileage.Miles;
            miles.Gas = mileage.Gas;
            miles.Id = mileage.Id;
            miles.CarId = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]);
            miles.MPG = mileage.MPG;
            miles.Price = mileage.Price;
            miles.Note = mileage.Note;
        }

        protected EditMileageViewController1(IntPtr handle) : base(handle)
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
            tfDate.Text = miles.Date.ToString("MM/dd/yyyy");
            tfMiles.Text = miles.Miles.ToString();
            tfGas.Text = miles.Gas.ToString();
            tfCost.Text = Math.Round(miles.Price, 3).ToString();
            tfNote.Text = miles.Note;

            tfDate.TextColor = UIColor.Black;
            tfMiles.TextColor = UIColor.Black;
            tfGas.TextColor = UIColor.Black;
            tfCost.TextColor = UIColor.Black;
            tfNote.TextColor = UIColor.Black;

            // Perform any additional setup after loading the view, typically from a nib.
        }

        partial void BtnUpdate_TouchUpInside(UIButton sender)
        {
            miles.StrDate = tfDate.Text;
            miles.Miles = Convert.ToDecimal(tfMiles.Text);
            miles.Gas = Convert.ToDecimal(tfGas.Text);
            miles.Id = miles.Id;
            miles.CarId = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]);
            miles.MPG = Math.Round(miles.Miles / miles.Gas, 3);
            miles.Price = Convert.ToDecimal(tfCost.Text);
            miles.Note = tfNote.Text;
            MileageItemRepository mir = new MileageItemRepository();
            var results = mir.UpdateMileageAsync(miles);
            GetMileageData();
        }

        partial void BtnCancel_TouchUpInside(UIButton sender)
        {
            GetMileageData();
        }

        public async void GetMileageData()
        {
            UITableView tableView = new UITableView(UIScreen.MainScreen.Bounds);
            MileageItemRepository repository = new MileageItemRepository();

            //var response = await repository.GetAllMileageDisplayData();  //Change 0 to position 
            int autoId = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]);
            var response = await repository.GetMileageData(autoId);
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
                    Price = item.Price,
                    Note = item.Note
                };
                resp.Add(result);
            }

            resp.Sort((x, y) => x.Date.CompareTo(y.Date));
            resp.Reverse();

            tableView.Source = new MyTableSource(resp);
            //tableView.BackgroundColor = UIColor.Red;
            this.Add(tableView);
        }
    }
}