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
    [Register ("MainMenuViewController")]
    partial class MainMenuViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView autoPicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnEnterMiles { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnHelp { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnIcloud { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnMileageSummary { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnTotalStats { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnViewCar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnViewMiles { get; set; }


        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIButton btnEnterCar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCarName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MainStoryBoard { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtMessage { get; set; }

        [Action ("BtnEnterMiles_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnEnterMiles_TouchUpInside (UIKit.UIButton sender);

        [Action ("BtnHelp_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnHelp_TouchUpInside (UIKit.UIButton sender);

        [Action ("BtnTotalStats_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnTotalStats_TouchUpInside (UIKit.UIButton sender);

        [Action ("BtnViewMiles_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnViewMiles_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton105475_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton105475_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (autoPicker != null) {
                autoPicker.Dispose ();
                autoPicker = null;
            }

            if (btnEnterMiles != null) {
                btnEnterMiles.Dispose ();
                btnEnterMiles = null;
            }

            if (btnHelp != null) {
                btnHelp.Dispose ();
                btnHelp = null;
            }

            if (btnIcloud != null) {
                btnIcloud.Dispose ();
                btnIcloud = null;
            }

            if (btnMileageSummary != null) {
                btnMileageSummary.Dispose ();
                btnMileageSummary = null;
            }

            if (btnTotalStats != null) {
                btnTotalStats.Dispose ();
                btnTotalStats = null;
            }

            if (btnViewCar != null) {
                btnViewCar.Dispose ();
                btnViewCar = null;
            }

            if (btnViewMiles != null) {
                btnViewMiles.Dispose ();
                btnViewMiles = null;
            }

            if (btnEnterCar != null)
            {
                btnEnterCar.Dispose();
                btnEnterCar = null;
            }

            if (lblCarName != null) {
                lblCarName.Dispose ();
                lblCarName = null;
            }

            if (txtMessage != null) {
                txtMessage.Dispose ();
                txtMessage = null;
            }
        }
    }
}