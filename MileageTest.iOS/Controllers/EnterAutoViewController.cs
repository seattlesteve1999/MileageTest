using MileageManagerForms.iOS.Controllers;
using MileageManagerForms.DataAccess;
using System;
using System.Text.RegularExpressions;
using UIKit;
using MileageManagerForms.Database;

namespace MileageManagerForms.iOS
{
    public partial class EnterAutoViewController : UIViewController
    {
        public UIWindow Window { get; set; }

        public EnterAutoViewController(IntPtr handle) : base(handle)
        {
            
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            txtYear.TextColor = UIColor.Black;
            txtCarName.TextColor = UIColor.Black;
            txtResults.TextColor = UIColor.Black;            
            
            // Perform any additional setup after loading the view, typically from a nib.
        }

        partial void BtnSubmit_TouchUpInside(UIButton sender)
        {
            AddCar();
        }

        public async void AddCar()
        {
            bool errorFound = false;

            Regex regex = new Regex(@"^[1-9]\d{3,}$");
            Match match = regex.Match(txtYear.Text);
            if (!match.Success)
            {
                txtResults.TextColor = UIColor.White;
                txtResults.Text = "Year Must Be 4 Numbers";
                errorFound = true;
            }
            else if ((txtYear.Text == string.Empty || (Convert.ToInt32(txtYear.Text) < 1900)) && !errorFound)
            {
                txtResults.TextColor = UIColor.White;
                txtResults.Text = "Year Must Be Greater Than 1900";
                errorFound = true;
            }
            else if (string.IsNullOrEmpty(txtCarName.Text) && !errorFound)
            {
                txtResults.TextColor = UIColor.White;
                txtResults.Text = "You Must Enter a Car Name";
                errorFound = true;
            }

            if (!errorFound)
            {
                var isDefault = false;
                if (swDefault.On)
                {
                    isDefault = true;
                }
                else
                {
                    isDefault = false;
                }
                MileageItemRepository repository = new MileageItemRepository();               
                if (swDefault.On)
                {
                    var autoResults = repository.GetAuto2();
                    foreach (var item in autoResults)
                    {
                        if (item.IsDefault)
                        {
                            var resp = repository.UpdateAutoAsync(false, item.Id);
                        }
                    }
                    App.Current.Properties["processedChange"] = false;
                }
                AutoTableDefination data = new AutoTableDefination()
                {
                    IsDefault = isDefault,
                    CarYear = txtYear.Text,
                    CarDesc = txtCarName.Text
                };
               
                int results = await repository.AddAutoData(data);

                if (results == 1)
                {
                    if (Convert.ToBoolean(App.Current.Properties["firstAutoEntered"]))
                    {
                        App.Current.Properties["autoId"] = 1;
                        App.Current.Properties["firstAutoEntered"] = false;
                    }

                    txtResults.Text = "Car Successfully Added";                                       
                }
                else
                {
                    txtResults.Text = "Sorry, Something Went Wrong. Please Try Again";
                }
            }
        }

        partial void UIButton15073_TouchUpInside(UIButton sender)
        {
            DismissViewController(true, null);
        }
    }
}