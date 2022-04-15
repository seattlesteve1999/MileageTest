using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace MileageManagerForms.Utilities
{
    public class MyNavigation
    {
        public static INavigation GetNavigation()
        {
            var mdp = Application.Current.MainPage as MasterDetailPage;
            if (mdp != null)
            {
                // Return the navigation of the detail-page
                return mdp.Detail.Navigation;
            }

            var np = Application.Current.MainPage as NavigationPage;
            if (np != null)
            {
                // Page is no MasterDetail-Page, but has a navigation
                return np.Navigation;
            }

            // No navigation found (just set the page, instead of navigation)
            return null;
        }

    }
}
