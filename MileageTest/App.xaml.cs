using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;


namespace MileageManagerForms
{
    public partial class App : Application
    {
        public static DateTime summaryDate = new DateTime();        

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(root: new MainPage());
        }

        protected override void OnStart()
        {           
            AppCenter.Configure("ios=f3784577-da22-43e1-88fd-a7a92621763d;macos=f3784577-da22-43e1-88fd-a7a92621763d;");
            if (AppCenter.Configured)
            {
                AppCenter.Start(typeof(Analytics));
                AppCenter.Start(typeof(Crashes));
            }

            try
            {
                //PublicDatabase = CKContainer.DefaultContainer.PublicCloudDatabase;
                //PrivateDatabase = CKContainer.DefaultContainer.PrivateCloudDatabase;
                CodeFile.CodeFileCall();
            }
            catch (Exception ex)
            {
                Analytics.TrackEvent("In FetchRecords Ex = " + ex);
                Crashes.TrackError(ex);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
