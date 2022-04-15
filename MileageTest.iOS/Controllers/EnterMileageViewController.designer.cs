// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MileageManagerForms.iOS
{
    [Register ("EnterMileageViewController")]
    partial class EnterMileageViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnCalculate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker dpDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField GasUsed { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField MilesDriven { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField MilesPerGallon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbError { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbNote { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbPrice { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbResults { get; set; }

        [Action ("BtnCalculate_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnCalculate_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton18419_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton18419_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (BtnCalculate != null) {
                BtnCalculate.Dispose ();
                BtnCalculate = null;
            }

            if (dpDate != null) {
                dpDate.Dispose ();
                dpDate = null;
            }

            if (GasUsed != null) {
                GasUsed.Dispose ();
                GasUsed = null;
            }

            if (MilesDriven != null) {
                MilesDriven.Dispose ();
                MilesDriven = null;
            }

            if (MilesPerGallon != null) {
                MilesPerGallon.Dispose ();
                MilesPerGallon = null;
            }

            if (tbError != null) {
                tbError.Dispose ();
                tbError = null;
            }

            if (tbNote != null) {
                tbNote.Dispose ();
                tbNote = null;
            }

            if (tbPrice != null) {
                tbPrice.Dispose ();
                tbPrice = null;
            }

            if (tbResults != null) {
                tbResults.Dispose ();
                tbResults = null;
            }
        }
    }
}