using CoreGraphics;
using Foundation;
using MileageManagerForms.iOS.Controllers;
using MileageManagerForms.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using UIKit;
using MileageManagerForms.Database;

namespace MileageManagerForms.iOS
{
    public partial class TableSourceSummary : UITableViewSource
    {
        private readonly string cellID = "MySummaryCell";
        private MySummaryCell cell;
        public List<Mileage> dataList;
        private string _headerText { get; set; }
        private readonly bool first = true;
        private readonly UITableViewHeaderFooterView footer = new UITableViewHeaderFooterView();
        private int dateCounter = 0;
        private int milesCounter = 0;
        private int gasCounter = 0;
        private int mpgCounter = 0;
        private int costCounter = 0;
        public UIWindow Window
        {
            get;
            set;
        }

        public TableSourceSummary(List<Mileage> mileageInfo)
        {
            dataList = mileageInfo;
        }

        public TableSourceSummary()
        {
            dataList = new List<Mileage>();
        }

        public void RemoveAt(int row)
        {
            dataList.RemoveAt(row);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return dataList.Count + 1;
        }

        public override bool CanEditRow(UITableView tableview, NSIndexPath indexPath)
        {
            return false;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            int row = 0;
            cell = tableView.DequeueReusableCell(cellID) as MySummaryCell;

            if (null == cell)
            {
                cell = new MySummaryCell(UITableViewCellStyle.Default, cellID);
            }

            row = indexPath.Row;

            if (row == dataList.Count)
            {
                cell.SetData(null, null, null, null, 0, null);
                CanEditRow(tableView, indexPath);
            }
            else
            {
                decimal gasRound = Math.Round(Convert.ToDecimal(dataList[row].Gas), 3);
                decimal milesRound = Math.Round(Convert.ToDecimal(dataList[row].Miles), 3);
                decimal mpgRound = Math.Round(Convert.ToDecimal(dataList[row].MPG), 3);
                decimal costRound = Math.Round(Convert.ToDecimal(dataList[row].Price), 2);

                cell.SetData(dataList[row].Date.ToString("MM/yy"), milesRound.ToString(), gasRound.ToString(), mpgRound.ToString(), dataList[row].Id, costRound.ToString());
            }

            return cell;
        }


        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //if (indexPath.Row > 0)
            //{                
            UIAlertView alert = new UIAlertView()
            {
                Title = "Action",
            };
            alert.AddButton("Details");
            alert.AddButton("Cancel");
            alert.Show();
            alert.Clicked += (s, b) =>
            {
                if (b.ButtonIndex == 0)
                {
                    Mileage data = dataList[indexPath.Row];

                    int id = Convert.ToInt32(data.Id);
                    DateTime date = Convert.ToDateTime(data.Date);

                    App.summaryDate = date;
                    

                }
            };
            tableView.ReloadData();
        }

        public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        {
            cell.TextLabel.TextColor = UIColor.Black;
            if (indexPath.Row % 2 == 0)
            {
                cell.BackgroundColor = UIColor.Gray;
            }
            else
            {
                cell.BackgroundColor = UIColor.LightGray;
            }
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            UIView view = new UIView(new Rectangle(0, 65, 320, 90));

            UILabel label = new UILabel
            {
                Opaque = true,
                TextColor = UIColor.Black, //.FromRGB(190, 0, 0);
                Font = UIFont.FromName("Helvetica-Bold", 22f),
                Frame = new RectangleF(12, 62, 375, 23),
                Text = "View Mileage Summary",
                TextAlignment = UITextAlignment.Center
            };
            view.AddSubview(label);

            UIButton buttonRect = new UIButton
            {
                Opaque = true,
                BackgroundColor = UIColor.FromRGB(191, 187, 189),
                Font = UIFont.FromName("Helvetica Neue", 17f),
                Frame = new CGRect(130, 7, 128, 30)
            };
            buttonRect.SetTitle("Main Menu", UIControlState.Normal);
            buttonRect.SetTitleColor(UIColor.Black, UIControlState.Normal);
            buttonRect.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            buttonRect.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            buttonRect.TouchUpInside += delegate
            {
               

            };
            view.AddSubview(buttonRect);

            UIButton buttonDate = new UIButton
            {
                Opaque = true,
                BackgroundColor = UIColor.DarkGray,
                Font = UIFont.FromName("Helvetica-Bold", 13f),
                Frame = new CGRect(15, 105, 50, 25)
            };
            buttonDate.SetTitle("Date", UIControlState.Normal);
            buttonDate.SetTitleColor(UIColor.White, UIControlState.Normal);
            buttonDate.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            buttonDate.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            buttonDate.TouchUpInside += delegate
            {
                dataList.Sort((x, y) => x.Date.CompareTo(y.Date));
                if (dateCounter % 2 != 0)
                {
                    dataList.Reverse();
                }

                tableView.ReloadData();
                dateCounter++;
            };
            view.AddSubview(buttonDate);

            UIButton buttonMiles = new UIButton
            {
                Opaque = true,
                BackgroundColor = UIColor.DarkGray,
                Font = UIFont.FromName("Helvetica-Bold", 13f),
                Frame = new CGRect(85, 105, 50, 25)
            };
            buttonMiles.SetTitle("Miles", UIControlState.Normal);
            buttonMiles.SetTitleColor(UIColor.White, UIControlState.Normal);
            buttonMiles.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            buttonMiles.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            buttonMiles.TouchUpInside += delegate
            {
                dataList.Sort((x, y) => x.Miles.CompareTo(y.Miles));
                if (milesCounter % 2 == 0)
                {
                    dataList.Reverse();
                }

                tableView.ReloadData();
                milesCounter++;
            };
            view.AddSubview(buttonMiles);

            UIButton buttonGas = new UIButton
            {
                Opaque = true,
                BackgroundColor = UIColor.DarkGray,
                Font = UIFont.FromName("Helvetica-Bold", 13f),
                Frame = new CGRect(159, 105, 50, 25)
            };
            buttonGas.SetTitle("Gas", UIControlState.Normal);
            buttonGas.SetTitleColor(UIColor.White, UIControlState.Normal);
            buttonGas.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            buttonGas.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            buttonGas.TouchUpInside += delegate
            {
                dataList.Sort((x, y) => x.Gas.CompareTo(y.Gas));
                if (gasCounter % 2 == 0)
                {
                    dataList.Reverse();
                }

                tableView.ReloadData();
                gasCounter++;
            };
            view.AddSubview(buttonGas);

            UIButton buttonMpg = new UIButton
            {
                Opaque = true,
                BackgroundColor = UIColor.DarkGray,
                Font = UIFont.FromName("Helvetica-Bold", 13f),
                Frame = new CGRect(232, 105, 50, 25)
            };
            buttonMpg.SetTitle("MPG", UIControlState.Normal);
            buttonMpg.SetTitleColor(UIColor.White, UIControlState.Normal);
            buttonMpg.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            buttonMpg.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            buttonMpg.TouchUpInside += delegate
            {
                dataList.Sort((x, y) => x.MPG.CompareTo(y.MPG));
                if (mpgCounter % 2 == 0)
                {
                    dataList.Reverse();
                }

                tableView.ReloadData();
                mpgCounter++;
            };
            view.AddSubview(buttonMpg);

            UIButton buttonCost = new UIButton
            {
                Opaque = true,
                BackgroundColor = UIColor.DarkGray,
                Font = UIFont.FromName("Helvetica-Bold", 13f),
                Frame = new CGRect(310, 105, 50, 25)
            };
            buttonCost.SetTitle("Cost", UIControlState.Normal);
            buttonCost.SetTitleColor(UIColor.White, UIControlState.Normal);
            buttonCost.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            buttonCost.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            buttonCost.TouchUpInside += delegate
            {
                dataList.Sort((x, y) => x.Price.CompareTo(y.Price));
                if (costCounter % 2 == 0)
                {
                    dataList.Reverse();
                }

                tableView.ReloadData();
                costCounter++;
            };
            view.AddSubview(buttonCost);

            view.BackgroundColor = tableView.BackgroundColor;

            return view;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 150;
        }
    }

    public class MySummaryCell : UITableViewCell
    {
        private readonly UILabel lbC0;
        private readonly UILabel lbC1;
        private readonly UILabel lbC2;
        private readonly UILabel lbC3;
        private readonly UILabel lbId;
        private readonly UILabel lbcst;
        private readonly MileageTableDefination mObject;
        public UIWindow Window { get; set; }

        public delegate void DeleteCellHandler(UITableViewCell cell);

        public MySummaryCell(UITableViewCellStyle style, string cellID) : base(style, cellID)
        {
            lbC0 = new UILabel
            {
                TextAlignment = UITextAlignment.Right
            };
            this.AddSubview(lbC0);

            lbC1 = new UILabel
            {
                TextAlignment = UITextAlignment.Right
            };
            this.AddSubview(lbC1);

            lbC2 = new UILabel
            {
                TextAlignment = UITextAlignment.Right
            };
            this.AddSubview(lbC2);

            lbC3 = new UILabel
            {
                TextAlignment = UITextAlignment.Right
            };
            this.AddSubview(lbC3);

            lbId = new UILabel
            {
                TextAlignment = UITextAlignment.Right
            };
            this.AddSubview(lbId);

            lbcst = new UILabel
            {
                TextAlignment = UITextAlignment.Right
            };
            this.AddSubview(lbcst);
        }

        public void SetData(string str0, string str1, string str2, string str3, int id, string str4)
        {
            lbC0.Text = str0;
            lbC1.Text = str1;
            lbC2.Text = str2;
            lbC3.Text = str3;
            lbcst.Text = str4;
            lbId.Text = id.ToString();
        }

        public override void LayoutSubviews()
        {
            nfloat lbWidth = Bounds.Width / 6;
            nfloat lbWidth2 = lbWidth + 7;
            nfloat lbWidth3 = lbWidth2 + 75;
            nfloat lbWidth4 = lbWidth3 + 75;
            nfloat lbWidth5 = lbWidth4 + 75;
            nfloat lbHeight = this.Bounds.Height - 100;
            if (lbC0 != null)
            {
                lbC0.Frame = new CGRect(2, 55, lbWidth, lbHeight);
                lbC1.Frame = new CGRect(lbWidth2, 55, lbWidth, lbHeight);
                lbC2.Frame = new CGRect(lbWidth3, 55, lbWidth, lbHeight);
                lbC3.Frame = new CGRect(lbWidth4, 55, lbWidth, lbHeight);
                lbcst.Frame = new CGRect(lbWidth5, 55, lbWidth, lbHeight);
            }
        }
    }
}
