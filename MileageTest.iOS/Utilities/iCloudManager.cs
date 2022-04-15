using CloudKit;
using Foundation;
using Microsoft.AppCenter.Analytics;
using MileageManagerForms.iOS.Controllers;
using MileageManagerForms.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;
using MileageManagerForms.Database;

namespace MileageManager.iOS.Utilities
{
    public class iCloudManager : NSObject
    {
        readonly CKContainer container;
        private readonly CKDatabase privateDatabase;
        private int rstrCounter = 0;

        public UIWindow Window { get; private set; }

        public iCloudManager()
        {
            container = CKContainer.DefaultContainer;
            privateDatabase = container.PrivateCloudDatabase;
        }

        public async Task<int> FetchRecords(string recordType, string dateString, Action<List<CKRecord>> completionHandler)
        {
            int autoId = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]);
            string predicateWithFormat = "AutoId = " + autoId + " AND DateKey = " + "\"" + dateString + "\"";
            NSPredicate truePredicate = NSPredicate.FromFormat(predicateWithFormat);
            MileageTableDefination result = new MileageTableDefination();
            List<MileageTableDefination> resp = new List<MileageTableDefination>();
            NSFileManager fm = new NSFileManager();
            
            //Analytics.TrackEvent("predicateWithFormat = " + predicateWithFormat);
            //Analytics.TrackEvent("RecordType = " + recordType);


            try
            {
                if (fm.UbiquityIdentityToken != null)
                {
                    CKQuery query = new CKQuery(recordType, truePredicate);
                    //{
                    //    SortDescriptors = new[] { new NSSortDescriptor("StrDate", false) }
                    //};

                    CKQueryOperation queryOperation = new CKQueryOperation(query)
                    {
                        DesiredKeys = new[] { "StrDate", "Gas", "Id", "Miles", "MPG", "Price", "Note" }
                    };

                    string note = string.Empty;
                    List<CKRecord> results = new List<CKRecord>();
                    queryOperation.ResultsLimit = 2000;

                    queryOperation.RecordFetched = (record) =>
                    {
                        results.Add(record);
                    };

                    queryOperation.Completed = async (cursor, err) =>
                    {
                        if (err != null)
                        {
                            if (err.ToString().IndexOf("This request requires an authenticated account") > -1 || err.ToString().IndexOf("CloudKit access was denied") > -1 || err.ToString().IndexOf("This operation has been rate limited") > -1)
                            {
                                Xamarin.Forms.Application.Current.Properties["Counter"] = -1;
                                await Task.Delay(15000);
                            }
                            else
                                Analytics.TrackEvent("Rstr QueryOperation Error = " + err);
                        }
                        InvokeOnMainThread(() => completionHandler(results));
                        foreach (CKRecord item in results)
                        {
                            await Task.Delay(250);
                            if (item["Note"] != null)
                                note = item["Note"].ToString();
                            else
                                note = string.Empty;

                            //Write to TableView for Display                        
                            result = new MileageTableDefination
                            {
                                CarId = autoId,
                                StrDate = item["StrDate"].ToString(),
                                Gas = Convert.ToDecimal(item["Gas"].ToString()),
                                Id = Convert.ToInt32(item["Id"].ToString()),
                                Miles = Convert.ToDecimal(item["Miles"].ToString()),
                                MPG = Convert.ToDecimal(item["MPG"].ToString()),
                                Price = Convert.ToDecimal(item["Price"].ToString()),
                                Note = note
                            };
                            resp.Add(result);
                        }
                        resp.Sort((x, y) => x.Date.CompareTo(y.Date));
                        resp.Reverse();

                        if (resp.Count > 0)
                        {
                            MileageItemRepository repository = new MileageItemRepository();

                            // ***** Delete DB Data *****
                            repository.DeleteAllMileageEntries(autoId);

                            foreach (MileageTableDefination item in resp)
                            {
                                await repository.AddMileageData(item);
                                rstrCounter++;
                            }

                            Analytics.TrackEvent("rstrCounter = " + rstrCounter);
                            Xamarin.Forms.Application.Current.Properties["Counter"] = rstrCounter;
                        }
                    };
                    privateDatabase.AddOperation(queryOperation);

                    do
                    {
                        await Task.Delay(15000);
                    } while (rstrCounter != results.Count);

                    Window = new UIWindow(UIScreen.MainScreen.Bounds);
                    //this.Window.RootViewController = webController;
                    this.Window.MakeKeyAndVisible();
                }
                else
                {
                    Xamarin.Forms.Application.Current.Properties["Counter"] = -1;
                    Window = new UIWindow(UIScreen.MainScreen.Bounds);
                    //this.Window.RootViewController = webController;
                    this.Window.MakeKeyAndVisible();
                }
            }
            catch (Exception exception)
            {
                Analytics.TrackEvent("Exception Thrown in FetchRecords = " + exception);
            }

            //iCloudProcessViewController ipvc = new iCloudProcessViewController();
            //ipvc.SetUpResultMessage(rstrCounter, true);            


            return 0;
        }
    }
}
