// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MileageManagerForms.iOS.Controllers
{
    [Register ("TotalsViewController")]
    partial class TotalsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbAvgPrice { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbCost { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbGas { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbHeading { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbMiles { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbMPG { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbYear { get; set; }

        [Action ("UIButton115315_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton115315_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton116238_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton116238_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton27046_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton27046_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (tbAvgPrice != null) {
                tbAvgPrice.Dispose ();
                tbAvgPrice = null;
            }

            if (tbCost != null) {
                tbCost.Dispose ();
                tbCost = null;
            }

            if (tbGas != null) {
                tbGas.Dispose ();
                tbGas = null;
            }

            if (tbHeading != null) {
                tbHeading.Dispose ();
                tbHeading = null;
            }

            if (tbMiles != null) {
                tbMiles.Dispose ();
                tbMiles = null;
            }

            if (tbMPG != null) {
                tbMPG.Dispose ();
                tbMPG = null;
            }

            if (tbYear != null) {
                tbYear.Dispose ();
                tbYear = null;
            }
        }
    }
}