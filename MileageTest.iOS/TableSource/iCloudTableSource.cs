using CloudKit;
using Foundation;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MileageManagerForms.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;
using MileageManagerForms.Database;
using MileageManager.iOS.Utilities;

namespace MileageManagerForms.iOS
{
    public partial class iCloudTableSource : UITableViewSource
    {
        readonly string[] TableItems;
        readonly string CellIdentifier = "TableCell";
        private readonly List<CKRecord> previousSearchRequests = new List<CKRecord>();

        public UIWindow Window
        {
            get;
            set;
        }

        public iCloudTableSource(string[] items)
        {
            Array.Sort(items);
            Array.Reverse(items);
            TableItems = items;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            string item = TableItems[indexPath.Row];

            //if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            cell.TextLabel.Text = item;

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            UIAlertView alert = new UIAlertView()
            {
                Title = "Restore",
            };
            alert.AddButton("Continue");
            alert.AddButton("Cancel");
            alert.Show();
            alert.Clicked += (s, b) =>
            {
                if (b.ButtonIndex == 0)
                {
                    string data = TableItems[indexPath.Row];
                    App.Current.Properties["MakeRestore"] = true;
                    RestoreFromiCloud("AppleDevice", data, tableView);

                    alert = new UIAlertView()
                    {
                        Title = "Please Wait, Restoring",
                    };
                    alert.AddButton("Ok");
                    alert.Show();
                    alert.Clicked += (x, y) =>
                    {
                        if (y.ButtonIndex == 0)
                        {
                           
                        }
                    };
                }
            };
        }

        public async Task RestoreFromiCloud(string fileName, string name, UITableView tableView)
        {
            try
            {
                //int counter;
                List<MileageTableDefination> resp = new List<MileageTableDefination>();
                MileageTableDefination result = new MileageTableDefination();
                MileageItemRepository repository = new MileageItemRepository();
                string restoreResults = string.Empty;
                string dateString = name.Substring(11, 8);
                string ReferenceItemRecordName = fileName; // "MileageManager" + UIDevice.CurrentDevice.Name.Replace(" ", "").Trim() + dateString;

                iCloudManager cloudManager = new iCloudManager();

                int counter = await cloudManager.FetchRecords(ReferenceItemRecordName, dateString, results =>
                {
                    //previousSearchRequests = results;
                    //tableView.ReloadData();
                });
            }
            catch (Exception exception)
            {
                Analytics.TrackEvent("In FetchRecords Ex = " + exception);
                Crashes.TrackError(exception);
            }
        }
    }
}