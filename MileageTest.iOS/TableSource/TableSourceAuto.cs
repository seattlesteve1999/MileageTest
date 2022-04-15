using CoreGraphics;
using Foundation;
using MileageManagerForms.iOS.Controllers;
using MileageManagerForms.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;
using MileageManagerForms.Database;

namespace MileageManagerForms.iOS
{
    public partial class MyTableSourceAuto : UITableViewSource
    {

        private readonly string cellID = "MyCellAuto";
        private MyCellAuto cell;
        public List<Auto> dataList;
        public List<UISwitch> switchList;
        public UISwitch swtch;
        public bool[] swdflt;
        public int count = 0;
        public int counter = 0;
        public bool changed;
        private readonly UITableViewHeaderFooterView footer = new UITableViewHeaderFooterView();
        private readonly string message;

        public UIWindow Window
        {
            get;
            set;
        }

        public MyTableSourceAuto(List<Auto> autoInfo)
        {
            App.Current.UserAppTheme = OSAppTheme.Light;
            dataList = autoInfo;
            swdflt = new bool[10];
            switchList = new List<UISwitch>();
            for (int i = 0; i < autoInfo.Count; i++)
            {
                swtch = new UISwitch();
                swdflt[i] = dataList[i].Default;
                if (dataList[i].Default)
                    swtch.SetState(true, true);
                else
                    swtch.SetState(false, true);

                switchList.Add(swtch);
                count++;
            }
            changed = false;
            Xamarin.Forms.Application.Current.Properties["AutoCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["DeletingAuto"] = false;
            Xamarin.Forms.Application.Current.Properties["FirstAttempt"] = true;
            Xamarin.Forms.Application.Current.Properties["Trailer"] = true;
            Xamarin.Forms.Application.Current.Properties["TrailerCounter"] = 0;
        }

        public MyTableSourceAuto()
        {
            dataList = new List<Auto>();
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
            return true;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row > 0)
            {
                Xamarin.Forms.Application.Current.Properties["DeletingAuto"] = true;
                Xamarin.Forms.Application.Current.Properties["Refresh"] = true;
                MileageItemRepository repository = new MileageItemRepository();
                AutoTableDefination mObject = new AutoTableDefination();
                for (int i = 0; i < count; i++)
                {
                    int rowNum = indexPath.Row - 1;
                    if (i != rowNum)
                    {
                        Auto dataAll = dataList[i];
                        mObject.Id = dataAll.Id;
                        mObject.CarYear = dataAll.Year;
                        mObject.CarDesc = dataAll.Name;
                        //if (switchList[i].On)
                        //{
                        mObject.IsDefault = false;
                        switchList[i].SetState(false, true);
                        var resp = repository.UpdateAutoAsync(mObject.IsDefault, mObject.Id);
                        //}
                    }
                    else
                    {
                        Auto data = dataList[rowNum];
                        mObject.Id = data.Id;
                        mObject.CarYear = data.Year;
                        mObject.CarDesc = data.Name;
                        if (!switchList[rowNum].On)
                        {
                            mObject.IsDefault = true;
                            switchList[rowNum].SetState(true, true);
                            var resp2 = repository.UpdateAutoAsync(mObject.IsDefault, mObject.Id);
                        }
                    }
                }
                Xamarin.Forms.Application.Current.Properties["processedChange"] = false;
                tableView.ReloadData();
            }
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:                    
                    MyTableSource mySource = new MyTableSource();
                    AutoTableDefination mObject = new AutoTableDefination();
                    MileageItemRepository repository = new MileageItemRepository();
                    Auto data = dataList[indexPath.Row - 1];
                    mObject.CarYear = data.Year;
                    mObject.Id = data.Id;
                    mObject.CarDesc = data.Name;
                    mObject.IsDefault = data.Default;
                    System.Threading.Tasks.Task results = repository.DeleteCar(mObject);
                    // remove the item from the underlying data source
                    dataList.RemoveAt(indexPath.Row - 1);
                    // delete the row from the table
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);                   
                    tableView.ReloadData();
                    break;
                case UITableViewCellEditingStyle.None:
                    Console.WriteLine("CommitEditingStyle:None called");
                    break;
            }
        }

        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {            
            return "    Del";
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            int row = 0;
            cell = tableView.DequeueReusableCell(cellID) as MyCellAuto;
            row = indexPath.Row;


            if (null == cell)
            {
                if (row == dataList.Count)
                {
                    Xamarin.Forms.Application.Current.Properties["AutoCounter"] = 0;
                }
                cell = new MyCellAuto(UITableViewCellStyle.Default, cellID);
            }

            if (row == 0)
            {
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            }

            if (row == dataList.Count)
            {
                Xamarin.Forms.Application.Current.Properties["AutoCounter"] = 0;
                cell.SetData(null, null, 0, null, 0);
                CanEditRow(tableView, indexPath);
            }
            else
            {
                Xamarin.Forms.Application.Current.Properties["AutoCounter"] = 1;
                Xamarin.Forms.Application.Current.Properties["DeleteFlag"] = false;
                cell.SetData(dataList[row].Year, dataList[row].Name, dataList[row].Id, switchList[row].On, dataList.Count);
            }

            return cell;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            UIView view = new UIView(new System.Drawing.Rectangle(0, 65, 320, 100));

            UITextField label = new UITextField
            {
                Opaque = true,
                TextColor = UIColor.Black, //.FromRGB(190, 0, 0);
                Font = UIFont.FromName("Helvetica-Bold", 22f),
                Frame = new RectangleF(5, 60, 375, 20),
                Text = "Update Cars",
                TextAlignment = UITextAlignment.Center,
                UserInteractionEnabled = false
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


            UILabel lblYear = new UILabel
            {
                Opaque = true,
                TextColor = UIColor.Black, //.FromRGB(190, 0, 0);
                Font = UIFont.FromName("Helvetica-Bold", 20f),
                Frame = new RectangleF(55, 110, 375, 20),
                Text = "Year",
                TextAlignment = UITextAlignment.Left
            };
            label.UserInteractionEnabled = false;
            view.AddSubview(lblYear);

            UILabel lblDesc = new UILabel
            {
                Opaque = true,
                TextColor = UIColor.Black, //.FromRGB(190, 0, 0);
                Font = UIFont.FromName("Helvetica-Bold", 20f),
                Frame = new RectangleF(145, 110, 375, 20),
                Text = "Description",
                TextAlignment = UITextAlignment.Justified
            };
            label.UserInteractionEnabled = false;
            view.AddSubview(lblDesc);

            UILabel lblDefault = new UILabel
            {
                Opaque = true,
                TextColor = UIColor.Black, //.FromRGB(190, 0, 0);
                Font = UIFont.FromName("Helvetica-Bold", 20f),
                Frame = new RectangleF(285, 110, 375, 20),
                Text = "Default",
                TextAlignment = UITextAlignment.Justified
            };
            label.UserInteractionEnabled = false;
            view.AddSubview(lblDefault);

            view.BackgroundColor = tableView.BackgroundColor;

            return view;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 80;
        }
    }

    public class MyCellAuto : UITableViewCell
    {
        private UILabel tfYear;
        private UILabel tfName;
        private UILabel tfId;
        private UILabel lblMessage;
        private UILabel lblMessage2;
        private UILabel lblMessage3;
        private UILabel lblMessage4;
        private UIButton button;
        private int counter;

        public MyCellAuto(UITableViewCellStyle style, string cellID) : base(style, cellID)
        {            
            tfYear = new UILabel
            {
                TextAlignment = UITextAlignment.Right,
                Font = UIFont.BoldSystemFontOfSize(17)
            };
            this.AddSubview(tfYear);

            tfName = new UILabel
            {
                TextAlignment = UITextAlignment.Left,
                Font = UIFont.BoldSystemFontOfSize(17)
            };
            this.AddSubview(tfName);

            tfId = new UILabel
            {
                TextAlignment = UITextAlignment.Right,
                Font = UIFont.BoldSystemFontOfSize(17)
            };
            this.AddSubview(tfId);
            
            button = new UIButton
            {
                Frame = new CGRect(5, 5, 30, 35),
                BackgroundColor = UIColor.Green
            };
            button.SetTitle("On", UIControlState.Normal);
            button.SetTitleColor(UIColor.Black, UIControlState.Normal);
            button.SetTitleShadowColor(UIColor.Black, UIControlState.Normal);

            if (Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["AutoCounter"]) == 0)
            {
                button.BackgroundColor = UIColor.Clear;
                button.SetTitle("", UIControlState.Normal);
            }
            this.AddSubview(button);
        }

        public void SetData(string Year, string Name, int id, bool? dflt, int count)
        {
            if (lblMessage != null)
            {
                lblMessage.Hidden = true;
                lblMessage2.Hidden = true;
                lblMessage3.Hidden = true;
                lblMessage4.Hidden = true;
            }

            if (tfYear != null)
                tfYear.Text = Year;

            if (tfName != null)
                tfName.Text = Name;

            //tfId.Text = id.ToString();
            if (button != null)
            {
                if (dflt != null)
                {
                    if (!Convert.ToBoolean(dflt))
                    {
                        button.BackgroundColor = UIColor.Red;
                        button.SetTitle("Off", UIControlState.Normal);
                        button.SetTitleColor(UIColor.White, UIControlState.Normal);
                    }
                    else
                    {
                        button.BackgroundColor = UIColor.Green;
                        button.SetTitle("On", UIControlState.Normal);
                        button.SetTitleColor(UIColor.Black, UIControlState.Normal);
                        button.SetTitleShadowColor(UIColor.Black, UIControlState.Normal);
                    }
                }
                else
                {
                    button.BackgroundColor = UIColor.Clear;
                    button.SetTitle("", UIControlState.Normal);                    
                }
            }
          
        }


        public override void LayoutSubviews()
        {
            nfloat lbWidth = Bounds.Width / 3;
            nfloat lbWidth2 = lbWidth - 35;
            nfloat lbWidth3 = lbWidth2 + 55;
            nfloat lbWidth4 = lbWidth3 + 148;
            nfloat lbHeight = this.Bounds.Height;
            if (Convert.ToInt32(Xamarin.Forms.Application.Current.Properties["AutoCounter"]) == 1)
            {
                tfYear.Frame = new CGRect(2, 55, lbWidth2, lbHeight);
                tfName.Frame = new CGRect(lbWidth3, 55, 200, lbHeight);
                button.Frame = new CGRect(lbWidth4, 55, 55, 25);
            }
            else if (Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["FirstAttempt"]))
            {                
                lblMessage = new UILabel
                {
                    TextAlignment = UITextAlignment.Left,                                      
                    Font = UIFont.BoldSystemFontOfSize(20)
                };
                this.AddSubview(lblMessage);

                lblMessage2 = new UILabel
                {
                    TextAlignment = UITextAlignment.Left,
                    TextColor = UIColor.White,
                    Font = UIFont.BoldSystemFontOfSize(25)
                };
                this.AddSubview(lblMessage2);

                lblMessage3 = new UILabel
                {
                    TextAlignment = UITextAlignment.Left,
                    Font = UIFont.BoldSystemFontOfSize(20)
                };
                this.AddSubview(lblMessage3);

                lblMessage4 = new UILabel
                {
                    TextAlignment = UITextAlignment.Left,
                    Font = UIFont.BoldSystemFontOfSize(20)
                };
                this.AddSubview(lblMessage4);
                lblMessage.Text = "To Delete, Swipe Left";
                lblMessage2.Text = "Warning!";
                lblMessage3.Text = "Don't Delete The Default Before";
                lblMessage4.Text = "Selecting a New Default First";               
                lblMessage.Frame = new CGRect(2, 55, 500, lbHeight);
                lblMessage2.Frame = new CGRect(2, 78, 300, lbHeight);
                lblMessage3.Frame = new CGRect(2, 102, 500, lbHeight);
                lblMessage4.Frame = new CGRect(2, 125, 500, lbHeight);                
                App.Current.Properties["FirstAttempt"] = false;                
            }
        }
    }
}
