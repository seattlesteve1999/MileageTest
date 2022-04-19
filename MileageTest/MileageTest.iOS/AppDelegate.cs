using CloudKit;
using Foundation;
using MileageManagerForms;
using UIKit;

namespace MileageManagerForms.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible f.or launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        UIWindow window;

        public UIWindow Window { get => window; set => window = value; }

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Window = new UIWindow(UIScreen.MainScreen.Bounds);
            //app.RegisterForRemoteNotifications();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            //Setting up the first time boolean's
            Xamarin.Forms.Application.Current.Properties["enter1stCar"] = false;
            Xamarin.Forms.Application.Current.Properties["firstAutoEntered"] = false;
            Xamarin.Forms.Application.Current.Properties["firstTime"] = true;
            Xamarin.Forms.Application.Current.Properties["OneBackup"] = false;
            Xamarin.Forms.Application.Current.Properties["Counter"] = 0;
            Xamarin.Forms.Application.Current.Properties["Count"] = 0;
            Xamarin.Forms.Application.Current.Properties["SummaryMileageDetail"] = null;
            Xamarin.Forms.Application.Current.Properties["DateCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["MilesCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["GasCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["MPGCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["CostCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["SrtDateCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["SrtMilesCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["SrtGasCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["SrtMPGCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["SrtCostCounter"] = 1;
            Xamarin.Forms.Application.Current.Properties["IsDefault"] = false;
            Xamarin.Forms.Application.Current.Properties["FirstToggle"] = true;
            Xamarin.Forms.Application.Current.Properties["DeleteData"] = null;


            // Get the default public and private databases for
            // the application
            //PublicDatabase = CKContainer.DefaultContainer.PublicCloudDatabase;    //******* Make sure to uncomment ********
            //PrivateDatabase = CKContainer.DefaultContainer.PrivateCloudDatabase;            

            //return true;

            return base.FinishedLaunching(app, options);
        }
    }
}
