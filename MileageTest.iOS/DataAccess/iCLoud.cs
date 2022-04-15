using CloudKit;
using MileageManagerForms.iOS.Controllers;
using UIKit;

namespace MileageManagerForms.iOS.DataAccess
{
    public class iCloud
    {
        readonly UITableView tableView = new UITableView(UIScreen.MainScreen.Bounds);

        public AppDelegate ThisApp
        {
            get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
        }

        public void WriteToTheCloud(CKRecord data, CKRecordID id)
        {
            // Save it to iCloud
            ThisApp.PublicDatabase.SaveRecord(data, (record, err) =>
            {
                // Was there an error?
                if (err == null)
                {
                    iCloudProcessViewController cntl = new iCloudProcessViewController();
                    cntl.SetUpErrorMessage("In WriteToTheCloud" + err.Description, true);
                }
            });
        }
    }
}