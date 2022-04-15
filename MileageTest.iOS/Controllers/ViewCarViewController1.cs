using MileageManagerForms.iOS;
using MileageManagerForms.DataAccess;
using System;
using System.Collections.Generic;
using UIKit;
using MileageManagerForms.Database;

namespace MileageManagerForms.iOS //MileageManagerForms.iOS.Controllers
{
    public partial class ViewCarViewController : UIViewController
    {
        readonly UIWindow Window = new UIWindow();
        readonly UITableView tableView = new UITableView(UIScreen.MainScreen.Bounds);

        public ViewCarViewController(IntPtr handle) : base(handle)
        {
        }

        public ViewCarViewController() : base("ViewCarViewController1", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //View.BackgroundColor = UIColor.Red;
            //View.AddSubview(GetViewForHeader(tableView));           
            MileageItemRepository repository = new MileageItemRepository();
            List<AutoTableDefination> response = await repository.GetAuto();
            List<Auto> resp = new List<Auto>();
            Auto result = new Auto();

            foreach (AutoTableDefination item in response)
            {
                result = new Auto
                {
                    Year = item.CarYear,
                    Name = item.CarDesc,
                    Id = Convert.ToInt32(item.Id),
                    Default = item.IsDefault
                };
                resp.Add(result);
            }
            //await repository.DropAutosTable();           
            tableView.AllowsSelection = true;
            tableView.Source = new MyTableSourceAuto(resp);
            tableView.BackgroundColor = UIColor.FromRGB(105, 112, 229);
            //View.AddSubview(tableView);
            Add(tableView);            

            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}