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
    [Register ("EditMileageViewController1")]
    partial class EditMileageViewController1
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnUpdate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfCost { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfGas { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfMiles { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UITextField tfNote { get; set; }

        [Action ("BtnCancel_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnCancel_TouchUpInside (UIKit.UIButton sender);

        [Action ("BtnUpdate_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnUpdate_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnCancel != null) {
                btnCancel.Dispose ();
                btnCancel = null;
            }

            if (btnUpdate != null) {
                btnUpdate.Dispose ();
                btnUpdate = null;
            }

            if (tfCost != null) {
                tfCost.Dispose ();
                tfCost = null;
            }

            if (tfDate != null) {
                tfDate.Dispose ();
                tfDate = null;
            }

            if (tfGas != null) {
                tfGas.Dispose ();
                tfGas = null;
            }

            if (tfMiles != null) {
                tfMiles.Dispose ();
                tfMiles = null;
            }

            if (tfNote != null)
            {
                tfNote.Dispose();
                tfNote = null;
            }
        }
    }
}