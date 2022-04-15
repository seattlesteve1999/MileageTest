using System;
using System.Collections.Generic;
using UIKit;
using MileageManagerForms.Database;
using MileageManagerForms.DataAccess;

namespace MileageManagerForms.iOS
{
    public partial class ViewNotesViewController : UIViewController
    {
        public int Id { get; set; }
        public static MileageTableDefination miles = new MileageTableDefination();

        public ViewNotesViewController(IntPtr handle) : base(handle)
        {
        }

        public ViewNotesViewController(MileageTableDefination mileage)
        {
            miles.Date = Convert.ToDateTime(mileage.Date.ToString("MM/dd/yyyy"));
            miles.Note = mileage.Note;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            tfDate.Text = miles.Date.ToString("MM/dd/yyyy");
            tfNote.Text = miles.Note;

            tfDate.TextColor = UIColor.Black;
            tfNote.TextColor = UIColor.Black;
        }

        partial void BtnCancel_TouchUpInside(ViewNotesViewController sender)
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