using MileageManagerForms.iOS;
using MileageManagerForms.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using MileageManagerForms.Database;

namespace MileageManagerForms.iOS.Controllers
{
    public partial class MileageSummaryViewController1 : UIViewController
    {
        readonly UITableView tableView = new UITableView(UIScreen.MainScreen.Bounds);
        readonly UIWindow Window = new UIWindow();

        public MileageSummaryViewController1() : base("MileageSummaryViewController1", null)
        {
        }
        protected MileageSummaryViewController1(IntPtr handle) : base(handle)
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
            GetMileageData();
            // Perform any additional setup after loading the view, typically from a nib.
        }
        public async void GetMileageData()
        {
            MileageItemRepository repository = new MileageItemRepository();

            //await repository.DropMileageTable();


            //var response = await repository.GetAllMileageDisplayData();  //Change 0 to position 
            int autoId = Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["autoId"]);
            var  response = await repository.GetMileageData(autoId);
            List<Mileage> resp = new List<Mileage>();
            Mileage result = new Mileage();

            if (response != null)
            {
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
                List<Mileage> sortedResults = SortAndSummarize(resp);

                sortedResults.Sort((x, y) => x.Date.CompareTo(y.Date));
                sortedResults.Reverse();

                tableView.Source = new TableSourceSummary(sortedResults);
                tableView.BackgroundColor = UIColor.FromRGB(105, 112, 229);
                this.Add(tableView);
            }
        }

        public List<Mileage> SortAndSummarize(List<Mileage> SortedList)
        {
            List<Mileage> listOfData = new List<Mileage>();
            Mileage data = new Mileage();
            if (SortedList.Count == 0)
            {
                return new List<Mileage>();
            }
            else
            {
                decimal holdMiles = 0;
                decimal holdGas = 0;
                decimal holdPrice = 0;
                bool processing = false;
                int holdYear = 0;
                int holdMonth = 0;

                foreach (Mileage item in SortedList.OrderBy(c => c.Date.Year).ThenBy(c => c.Date.Month))
                {
                    holdYear = item.Date.Year;
                    holdMonth = item.Date.Month;
                    break;
                }

                foreach (Mileage item in SortedList.OrderBy(c => c.Date.Year).ThenBy(c => c.Date.Month))
                {                    
                    if (holdYear == item.Date.Year)
                    {
                        if (holdMonth == item.Date.Month)
                        {
                            data.Date = Convert.ToDateTime(item.Date.Month + "/" + item.Date.Year);
                            data.Miles += item.Miles;
                            data.Gas += item.Gas;
                            data.Price += item.Price;
                            holdMiles = data.Miles;
                            holdGas = data.Gas;
                            holdPrice = data.Price;
                            processing = true;
                        }
                        else
                        {
                            if (data.Miles != holdMiles)
                            {
                                if (!processing)
                                {
                                    data.Miles += holdMiles;
                                    data.Gas += holdGas;
                                    data.Price += holdPrice;
                                }
                                else
                                {
                                    data.Miles = holdMiles;
                                    data.Gas = holdGas;
                                    data.Price = holdPrice;
                                }
                            }
                            data.Date = Convert.ToDateTime(holdMonth + "/" + holdYear);
                            data.MPG = Math.Round(data.Miles / data.Gas, 3);
                            listOfData.Add(data);
                            data = new MileageTableDefination();
                            holdMonth = item.Date.Month;
                            holdYear = item.Date.Year;
                            holdMiles = item.Miles;
                            holdGas = item.Gas;
                            holdPrice = item.Price;
                            data.Miles = holdMiles;
                            data.Gas = holdGas;
                            data.Price = holdPrice;
                            processing = false;
                        }
                    }
                    else
                    {
                        if (data.Miles != holdMiles)
                        {
                            if (!processing)
                            {
                                data.Miles += holdMiles;
                                data.Gas += holdGas;
                                data.Price += holdPrice;
                            }
                            else
                            {
                                data.Miles = holdMiles;
                                data.Gas = holdGas;
                                data.Price = holdPrice;
                            }
                        }
                        data.Date = Convert.ToDateTime(holdMonth + "/" + holdYear);
                        data.MPG = Math.Round(data.Miles / data.Gas, 3);
                        listOfData.Add(data);
                        holdMonth = item.Date.Month;
                        holdYear = item.Date.Year;
                        holdMiles = item.Miles;
                        holdGas = item.Gas;
                        holdPrice = item.Price;
                        data = new MileageTableDefination
                        {
                            Miles = holdMiles,
                            Gas = holdGas,
                            Price = holdPrice
                        };
                        processing = false;
                    }
                }                               
                data.Date = Convert.ToDateTime(holdMonth + "/" + holdYear);
                data.Miles = holdMiles;
                data.Gas = holdGas;
                data.MPG = Math.Round(holdMiles / holdGas, 3);
                data.Price = Math.Round(holdPrice, 2);
                listOfData.Add(data);

                return listOfData;
            }

        }
    }
}