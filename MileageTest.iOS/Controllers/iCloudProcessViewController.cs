
using CloudKit;
using CoreGraphics;
using Foundation;
using Microsoft.AppCenter.Analytics;
using MileageManagerForms.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;
using MileageManagerForms.Database;
using MileageManagerForms.iOS.DataAccess;
using MileageManager.iOS.Controllers;
using MileageManager.iOS.Utilities;

namespace MileageManagerForms.iOS.Controllers
{
    public partial class iCloudProcessViewController : UIViewController
    {
        readonly UITableView TableView = new UITableView(UIScreen.MainScreen.Bounds);
        private readonly iCloud cloud = new iCloud();
        private readonly UITextField bkupResults = new UITextField();
        private UITextField rstrResults = new UITextField();
        private List<CKRecord> previousSearchRequests = new List<CKRecord>();
        private string rsltString;
        private int bkupCounter = 0;
        private UIDatePicker restoreDate = new UIDatePicker();
        private readonly UIDatePicker bkupDate = new UIDatePicker();
        public event EventHandler Finished;
        public UIActivityIndicatorView myIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Large);
        public bool iCloudError;

        readonly CKContainer container;
        private readonly CKDatabase privateDatabase;


        public UIWindow Window { get; set; }

        public AppDelegate ThisApp
        {
            get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
        }

        public iCloudProcessViewController(string data)
        {
            if (Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["MakeRestore"]))
                MakeRestoreCall("AppleDevice");
            Xamarin.Forms.Application.Current.Properties["MakeRestore"] = false;
        }

        protected iCloudProcessViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public iCloudProcessViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["Counter"]) == -1)
            {
                SetUpResultMessage(0, true);
            }
            else if (Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["Counter"]) > 0)
            {
                SetUpResultMessage(Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["Counter"]), true);
            }
            //Xamarin.Forms.Application.Current.Properties["Counter"] = 0;
            //actyIndicator.StartAnimating();
            actyIndicator.Hidden = true;
            myIndicator.Color = UIColor.Black;
            CGRect sFrame = myIndicator.Frame;
            sFrame.Offset(185, 450);
            myIndicator.Frame = sFrame;
            //myIndicator.HidesWhenStopped = true;
            View.Add(myIndicator);

            CGRect frame = new CGRect(40, 195, 100, 100);
            restoreDate = new UIDatePicker(frame)
            {
                Mode = UIDatePickerMode.Date
            };

            View.Add(restoreDate);
            Xamarin.Forms.Application.Current.Properties["RestoreResults"] = rsltString;
            int restoreCount = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["Counter"]);

            if (!Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["iCloudFirst"]))
            {
                myIndicator.StartAnimating();
                myIndicator.Hidden = false;
                if (restoreCount > 0)
                {
                    SetUpResultMessage(Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["Counter"]), true);
                    myIndicator.Hidden = true;
                }
            }
            else
                Xamarin.Forms.Application.Current.Properties["iCloudFirst"] = false;
        }

        public async Task<int> BackupToiCloud(int autoId)
        {
            NSFileManager fm = new NSFileManager();
            MileageItemRepository repository = new MileageItemRepository();
            NSDateFormatter dateFormatter = new NSDateFormatter
            {
                DateFormat = "MMddyyyy"
            };
            string dateString = dateFormatter.StringFor(restoreDate.Date);
            string ReferenceItemRecordName = "AppleDevice"; // UIDevice.CurrentDevice.Name.Replace(" ", "").Trim() + dateString;
            iCloudError = false;

            try
            {
                if (fm.UbiquityIdentityToken != null)
                {
                    List<MileageTableDefination> response = await repository.GetMileageData(autoId);

                    foreach (MileageTableDefination item in response)
                    {
                        if (!iCloudError)
                        {
                            bkupCounter++;
                            // Create a new record on ICloud
                            CKRecord newRecord = new CKRecord(ReferenceItemRecordName);
                            newRecord["DateKey"] = (NSString)dateString;
                            newRecord["AutoId"] = (NSNumber)item.CarId;
                            newRecord["Id"] = (NSNumber)item.Id;
                            //newRecord["Date"] = (NSDate)item.Date;
                            newRecord["StrDate"] = (NSString)item.StrDate;
                            newRecord["Gas"] = (NSNumber)Convert.ToDouble(item.Gas);
                            newRecord["Miles"] = (NSNumber)Convert.ToDouble(item.Miles);
                            newRecord["Price"] = (NSNumber)Convert.ToDouble(item.Price);
                            newRecord["MPG"] = (NSNumber)Convert.ToDouble(Math.Round(Convert.ToDecimal(item.Miles) / Convert.ToDecimal(item.Gas), 3).ToString());
                            if (!string.IsNullOrWhiteSpace(item.Note))
                                newRecord["Note"] = (NSString)item.Note;

                            CKRecordID recordID = new CKRecordID(dateString);
                            await Task.Delay(200);
                            // Save it to iCloud
                            ThisApp.PrivateDatabase.SaveRecord(newRecord, (record, err) =>
                            {
                                // Was there an error?                                       
                                if (err != null && !iCloudError)
                                {
                                    if (err.ToString().IndexOf("This request requires an authenticated account") > -1 || err.ToString().IndexOf("CloudKit access was denied") > -1 || err.ToString().IndexOf("This operation has been rate limited") > -1)
                                        iCloudError = true;
                                    else
                                        Analytics.TrackEvent("BkupToCloudError = " + err);
                                }
                            });
                        }
                    }
                    //return bkupCounter;
                }
                else
                    iCloudError = true;
            }
            catch (Exception exception)
            {
                Analytics.TrackEvent("In BackupToiCloud Ex = " + exception);
                //Crashes.TrackError(exception);
            }

            //Analytics.TrackEvent("End " + bkupCounter);

            if (fm.UbiquityIdentityToken != null && !iCloudError)
            {
                CloudViewController cvc = new CloudViewController();
                cvc.WriteToiCloud(ReferenceItemRecordName, dateString);
            }
            myIndicator.Hidden = true;
            return bkupCounter;
        }

        public async Task RestoreFromiCloud(string fileName)
        {
            try
            {
                List<MileageTableDefination> resp = new List<MileageTableDefination>();
                MileageTableDefination result = new MileageTableDefination();
                MileageItemRepository repository = new MileageItemRepository();
                string restoreResults = string.Empty;
                NSDateFormatter dateFormatter = new NSDateFormatter
                {
                    DateFormat = "MMddyyyy"
                };
                string dateString = dateFormatter.StringFor(restoreDate.Date);
                string ReferenceItemRecordName = fileName; // "MileageManager" + UIDevice.CurrentDevice.Name.Replace(" ", "").Trim() + dateString;

                iCloudManager cloudManager = new iCloudManager();

                Task<int> counter = cloudManager.FetchRecords(ReferenceItemRecordName, dateString, results =>
                {
                    previousSearchRequests = results;
                    TableView.ReloadData();
                });

                SetUpResultMessage(counter.Result, true);
            }
            catch (Exception exception)
            {
                Analytics.TrackEvent("In FetchRecords Ex = " + exception);
                //Crashes.TrackError(exception);
            }
        }

        public void SetUpResultMessage(int counter, bool restore)
        {
            nfloat h = 31.0f;
            nfloat w = View.Bounds.Width;
            rstrResults = new UITextField
            {
                Placeholder = "Results",
                BorderStyle = UITextBorderStyle.RoundedRect,
                Frame = new CGRect(10, 82, w - 20, h)
            };

            if (iCloudError ||
                Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["Counter"]) == -1)
            {
                myIndicator.StopAnimating();
                myIndicator.Hidden = true;
                rsltString = "Error: You must connect to iCloud first";
            }
            else if (!restore)
            {
                rsltString = "Backup Succeessful " + counter + " Records ";
            }
            else
            {
                rsltString = "Restore Succeeded " + counter + " Records Restored";
                // Xamarin.Forms.Application.Current.Properties["Counter"] = 0;
            }

            rstrResults.Text = rsltString;
            View.AddSubview(rstrResults);
        }

        public void SetUpErrorMessage(string errorMsg, bool restore)
        {
            nfloat h = 31.0f;
            nfloat w = View.Bounds.Width;
            rstrResults = new UITextField
            {
                Placeholder = "Results",
                BorderStyle = UITextBorderStyle.RoundedRect,
                Frame = new CGRect(10, 82, w - 20, h)
            };


            if (Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["OneBackup"]))
            {
                rsltString = "Backup Canceled";
                myIndicator.Hidden = true;
            }
            else if (!restore)
            {
                rsltString = "Backup Failed With " + errorMsg;
            }
            else
            {
                rsltString = "Restore Failed With " + errorMsg;
            }

            rstrResults.Text = rsltString;
            View.AddSubview(rstrResults);
        }

        public async void Backup()
        {
            nfloat h = 31.0f;
            nfloat w = View.Bounds.Width;
            rstrResults = new UITextField
            {
                Placeholder = "Results",
                BorderStyle = UITextBorderStyle.RoundedRect,
                Frame = new CGRect(10, 82, w - 20, h)
            };
            NSDateFormatter dateFormatter = new NSDateFormatter
            {
                DateFormat = "MMddyyyy"
            };
            string dateString = dateFormatter.StringFor(restoreDate.Date);

            if (Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["OneBackup"]) &&
                Xamarin.Forms.Application.Current.Properties["BackupDate"].ToString() == dateString)
            {
                UIAlertView alert = new UIAlertView()
                {
                    Title = "CAUTION Backing up twice for the same date will duplicate records on iCloud",
                };
                alert.AddButton("Continue");
                alert.AddButton("Cancel");
                alert.Show();
                alert.Clicked += (s, b) =>
                {
                    if (b.ButtonIndex == 0)
                    {
                        Xamarin.Forms.Application.Current.Properties["OneBackup"] = true;
                        Xamarin.Forms.Application.Current.Properties["BackupDate"] = dateString;
                        BackupAsync();
                    }
                    else
                    {
                        SetUpErrorMessage("Backup Canceled", false);
                    }
                };
            }
            else
            {
                bkupCounter = 0;
                Xamarin.Forms.Application.Current.Properties["OneBackup"] = true;
                Xamarin.Forms.Application.Current.Properties["BackupDate"] = dateString;
                rstrResults.Text = "Processing Backup";
                View.AddSubview(rstrResults);
                await BackupToiCloud(Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]));
                SetUpResultMessage(bkupCounter, false);
            }
        }


        public async void BackupAsync()
        {
            bkupCounter = 0;
            rstrResults.Text = "Processing Backup";
            View.AddSubview(rstrResults);
            bkupCounter = await BackupToiCloud(Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]));
            SetUpResultMessage(bkupCounter, false);
        }

        public async Task MakeRestoreCall(string fileName)
        {
            Analytics.TrackEvent("In MakeRestoreCall" + fileName);
            await RestoreFromiCloud(fileName);
        }

        partial void UIButton106730_TouchUpInside(UIButton sender)
        {
           
        }

        partial void UIButton133708_TouchUpInside(UIButton sender)
        {
            myIndicator.StartAnimating();
            myIndicator.Hidden = false;
        }

        partial void BtnBackup_TouchUpInside(UIButton sender)
        {
            Backup();
            myIndicator.StartAnimating();
            myIndicator.Hidden = false;
        }
    }
}