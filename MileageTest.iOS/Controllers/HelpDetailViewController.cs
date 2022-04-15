using MileageManagerForms.iOS.Controllers;
using System;
using System.Text;
using UIKit;
using Xamarin.Forms;

namespace MileageManagerForms.iOS.Controllers
{
    public partial class HelpDetailViewController : UIViewController
    {
        readonly UITableView tableView = new UITableView(UIScreen.MainScreen.Bounds);
        public UIWindow Window { get; set; }

        public HelpDetailViewController()
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        protected HelpDetailViewController(IntPtr handle) : base(handle)
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
            string title = string.Empty;
            UIImageView imageView = new UIImageView();


            View = new UIView
            {
                BackgroundColor = UIColor.LightGray,
            };

            UIButton btnReturn = new UIButton(UIButtonType.Custom);
            btnReturn.SetTitle("Help Menu", UIControlState.Normal);
            btnReturn.BackgroundColor = UIColor.Blue;

            switch (App.Current.Properties["Segue"].ToString())
            {
                case "MileageEntrySegue":
                    imageView.TranslatesAutoresizingMaskIntoConstraints = false;
                    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                    imageView.Image = UIImage.FromBundle("PartialMileageEntry.png");
                    title = "Enter Mileage Help";
                    break;
                case "MileageUpdateSegue":
                    imageView.TranslatesAutoresizingMaskIntoConstraints = false;
                    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                    imageView.Image = UIImage.FromBundle("EditHelp.png");
                    title = "Update Mileage Help";
                    break;
                case "ViewMileageSegue":
                    imageView.TranslatesAutoresizingMaskIntoConstraints = false;
                    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                    imageView.Image = UIImage.FromBundle("ViewMIleage.png");
                    title = "View Mileage Help";
                    break;
                case "MileageSummarySegue":
                    imageView.TranslatesAutoresizingMaskIntoConstraints = false;
                    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                    imageView.Image = UIImage.FromBundle("MileageSummary.png");
                    title = "Mileage Summary Help";
                    break;
                case "TotalStatsSegue":
                    imageView.TranslatesAutoresizingMaskIntoConstraints = false;
                    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                    imageView.Image = UIImage.FromBundle("TotalStats.png");
                    title = "Total Stats Help";
                    break;
                case "iCloutSegue":
                    imageView.TranslatesAutoresizingMaskIntoConstraints = false;
                    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                    imageView.Image = UIImage.FromBundle("iCloud.png");
                    title = "iCloud Process Help";
                    break;
            }

            UIStackView stackLayout = new UIStackView(new[]
            {
                btnReturn,
                getContentLabelHeader(title),
                imageView,
                getContentLabel(App.Current.Properties["Segue"].ToString()),
                new UIView()
            })
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Axis = UILayoutConstraintAxis.Vertical,
                Spacing = 8,
                LayoutMarginsRelativeArrangement = true,
                LayoutMargins = new UIEdgeInsets(18, 28, 18, 18)
            };

            UIScrollView scrollView = new UIScrollView
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            // Add the views.
            View.AddSubview(scrollView);
            scrollView.AddSubview(stackLayout);

            // Lay out the views.
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                scrollView.TopAnchor.ConstraintEqualTo(View.TopAnchor), //SafeAreaLayoutGuide.TopAnchor),
                scrollView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),
                scrollView.LeftAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeftAnchor),
                scrollView.RightAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.RightAnchor),
                stackLayout.TopAnchor.ConstraintEqualTo(scrollView.TopAnchor),
                stackLayout.BottomAnchor.ConstraintEqualTo(scrollView.BottomAnchor),
                stackLayout.LeftAnchor.ConstraintEqualTo(scrollView.LeftAnchor),
                stackLayout.RightAnchor.ConstraintEqualTo(scrollView.RightAnchor),
                // Prevent horizontal scrolling
                stackLayout.WidthAnchor.ConstraintEqualTo(scrollView.WidthAnchor)
            });            
        }

        UILabel getContentLabel(string segue)
        {
            UILabel label = new UILabel
            {
                LineBreakMode = UILineBreakMode.WordWrap,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.BoldSystemFontOfSize(21),
                Lines = 0
            };
            switch (segue)
            {
                case "MileageEntrySegue":
                    label.Text = MileageEntryContent();
                    break;
                case "MileageUpdateSegue":
                    label.Text = MileageUpdateContent();
                    break;
                case "ViewMileageSegue":
                    label.Text = ViewMileageContent();
                    break;
                case "MileageSummarySegue":
                    label.Text = ViewMileageSummaryContent();
                    break;
                case "TotalStatsSegue":
                    label.Text = TotalStatsContent();
                    break;
                case "iCloutSegue":
                    label.Text = iCloudContent();
                    break;
            }

            return label;
        }

        UILabel getContentLabelHeader(string content)
        {
            UILabel label = new UILabel
            {
                Text = content,
                LineBreakMode = UILineBreakMode.WordWrap,
                Font = UIFont.BoldSystemFontOfSize(24),
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 0,
                TextAlignment = UITextAlignment.Center
            };
            return label;
        }

        public string MileageEntryContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1. Select a date, defaults to todays date \n");
            sb.Append("2. Enter miles driven on the current tank \n ");
            sb.Append("3. Enter what it cost you to fill up \n ");
            sb.Append("4. Enter gas you used to fill up \n ");
            sb.Append("5. Enter notes about your trip \n ");
            sb.Append("6. Press the Calculate button \n ");
            sb.Append("7. View your calculated MPG \n \n");
            sb.Append("Note: When you press calculate, the information about this fill up is stored so you can view it later and see your progress from fill up to fill up");
            return sb.ToString();
        }

        public string MileageUpdateContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1. Touch the row  \n");
            sb.Append("2. Select Edit to change any piece of data in your entry \n ");
            sb.Append("3. Select Note to see your note \n ");
            sb.Append("4. Select Delete to Delete the record \n ");
            sb.Append("5. Select Cancel to return to the View Mileage screen \n ");
            return sb.ToString();
        }

        public string ViewMileageContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("This screen displays a history of your fill ups, sorted by date descending. However, each column header, if touched will sort by that field, ie. Miles or Gas etc. It is from this page that you can update records if you have made a mistake, see the Mileage Update help for more information. \n \n");
            sb.Append("Note: The * in front of the date indicates the record has a note included \n");
            return sb.ToString();
        }

        public string ViewMileageSummaryContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("This screen displays a history of your fill ups accumulated by month. The records are sorted by date descending, however, each column header, if touched will sort by that field, ie. Miles or Gas etc. If you touch a row on this page, it will pop up a box which will ask if you would like to see the details for that month. \n");
            return sb.ToString();
        }

        public string TotalStatsContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("This screen displays the accumulated totals since data started being recorded. When the page first comes up, it will display as stated, however, if you enter a year in the year field and click Submit, it will show the stats for that given year. If you want to see the total stats again, just hit the Totals button and they will display. \n \n");
            return sb.ToString();
        }

        public string iCloudContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CAUTION: It is important to backup with each fill up that way you never lose data. \n \n");
            sb.Append("This screen has 2 very powerful functions. \n");
            sb.Append("1. Backup - When clicked, it will take all your records and back them up to iCloud. The date is used in the name of the backed up file. It defaults to todays date, but you can change it to any date you want. \n");
            sb.Append("2. Restore - When clicked, a new screen will appear showing all your backup files thus far. Select the one you want to restore from and press continue, that is all there is to it. Then wait patiently as it takes a little time to restore. \n ");
            return sb.ToString();
        }

        partial void BtnHelpMenu_TouchUpInside(UIButton sender)
        {
           
        }
    }
}