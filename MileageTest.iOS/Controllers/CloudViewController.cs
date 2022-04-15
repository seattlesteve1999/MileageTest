using CloudKit;
using CoreGraphics;
using Foundation;
using MileageManagerForms.iOS.Controllers;
using MileageManagerForms.iOS;
using System;
using System.IO;
using UIKit;
using Xamarin.Forms;

namespace MileageManager.iOS.Controllers
{
    public partial class CloudViewController : UIViewController
    {
        private NSUrl _baseUrl;
        public UIWindow Window { get; set; }
        public UILabel headingLabel = new UILabel();
        public UITextField rstrResults = new UITextField();


        public CloudViewController(IntPtr handle) : base(handle)
        {

        }

        public CloudViewController()
        {
            //container = CKContainer.DefaultContainer;
            //privateDatabase = container.PrivateCloudDatabase;
        }

        public AppDelegate ThisApp
        {
            get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            nfloat h = 31.0f;
            nfloat w = View.Bounds.Width;
            rstrResults = new UITextField
            {
                Placeholder = "Results",
                BorderStyle = UITextBorderStyle.RoundedRect,
                Frame = new CGRect(15, 76, w - 20, h)
            };
            NSFileManager fm = new NSFileManager();
            if (fm.UbiquityIdentityToken != null)
            {
                string[] files = GetFiles();
                UITableView table = new UITableView(new CGRect(15, 140, 345, 600))
                {
                    SeparatorColor = UIColor.Black,
                    SeparatorStyle = UITableViewCellSeparatorStyle.DoubleLineEtched
                };
                headingLabel = new UILabel()
                {
                    Font = UIFont.FromName("Cochin-BoldItalic", 26f),
                    TextColor = UIColor.White, //.FromRGB(127, 51, 0),
                    BackgroundColor = UIColor.Blue,
                    TextAlignment = UITextAlignment.Center
                };
                headingLabel.Text = "Select File to Restore";
                View.AddSubview(new UIView { headingLabel });
                table.Source = new iCloudTableSource(files);
                Add(table);
            }
            else
            {
                Xamarin.Forms.Application.Current.Properties["Counter"] = -1;                
            }
        }

        public void ProcessingMessages(bool starting)
        {
            if (starting)
                rstrResults.Text = "Processing Restore";
            else
                rstrResults.Text = "Restore Complete";
            View.AddSubview(rstrResults);
        }
        public void WriteToiCloud(string name, string date)
        {
            NSUrl filePath = NSFileManager.DefaultManager.GetUrlForUbiquityContainer(null).Append("Documents", true);

            // ***** Comment Out *****           
            //DeleteCloudDocument(filePath);
            // ***** End Comment Out *****

            _baseUrl = NSFileManager.DefaultManager.GetUrlForUbiquityContainer(null);
            string ret = Path.Combine(_baseUrl.RelativePath, "Documents");
            if (!Directory.Exists(ret))
                Directory.CreateDirectory(ret);
            //File.WriteAllText(Path.Combine(filePath.Path, "DadsiPhone07072020"), "Test");
            //File.WriteAllText(Path.Combine(filePath.Path, "DadsiPhone07192020"), "Test");
            //File.WriteAllText(Path.Combine(filePath.Path, "DadsiPhone08062020"), "Test");
            File.WriteAllText(Path.Combine(filePath.Path, name + date), "Test");
        }

        public void DeleteCloudDocument(NSUrl path)
        {
            NSError error;
            NSFileCoordinator fileCoordinator = new NSFileCoordinator();
            NSFileManager manager = new NSFileManager();
            NSUrl filePath = NSFileManager.DefaultManager.GetUrlForUbiquityContainer(null);
            fileCoordinator.CoordinateWrite(filePath, NSFileCoordinatorWritingOptions.ForDeleting, out error,
                writingURL =>
                {
                    NSFileManager fileManager = new NSFileManager();
                    bool success = fileManager.Remove(writingURL, out error);
                });
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            ContentView cv = new ContentView();
            headingLabel.Frame = new CGRect(15, 90, 345, 60);
        }

        public CKRecord[] GetDBFiles()
        {
            CKRecord[] files = null;
            NSPredicate truePredicate = NSPredicate.FromValue(true);
            CKQuery query = new CKQuery("MileageManageriPhone11Pro03032020", truePredicate);
            ThisApp.PrivateDatabase.PerformQuery(query, null, (records, error) =>
            {
                // Was there an error?                                       
                if (error != null)
                {
                    Console.WriteLine("SetUpErrorMessage(In BackupToiCloud  + err.ToString(), false");
                }
                files = records;
            });
            return null;
        }

        public string[] GetFiles()
        {
            _baseUrl = NSFileManager.DefaultManager.GetUrlForUbiquityContainer(null);
            NSError error;
            string ret = Path.Combine(_baseUrl.RelativePath, "Documents");
            if (!Directory.Exists(ret))
                Directory.CreateDirectory(ret);
            Console.WriteLine(ret);
            string[] files = NSFileManager.DefaultManager.GetDirectoryContent(makeUrl().Path, out error);
            string[] newArray = new string[files.Length];
            int i = 0;
            foreach (string item in files)
            {
                newArray[i] = item.Replace(".icloud", "").Replace(".", "");
                i++;
            }
            return newArray;
        }

        private NSUrl makeUrl(string fname = null)
        {
            NSUrl url = _baseUrl.Append("Documents", true);
            if (fname != null) url = url.Append(fname, false);
            return url;
        }

        //partial void UIButton135333_TouchUpInside(UIButton sender)
        //{
        //    UIStoryboard Storyboard = UIStoryboard.FromName("Main", null);

        //    var webController = Storyboard.InstantiateViewController("MainMenuViewController") as MainMenuViewController;
        //    Window = new UIWindow(UIScreen.MainScreen.Bounds);
        //    this.Window.RootViewController = webController;
        //    this.Window.MakeKeyAndVisible();
        //}
    }
}