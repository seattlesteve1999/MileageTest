// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MileageManagerForms.iOS
{
    [Register ("EnterAutoViewController")]
    partial class EnterAutoViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView btnSave { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSubmit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtCarName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtResults { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtYear { get; set; }
        
        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UISwitch swDefault { get; set; }

        [Action ("BtnSubmit_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnSubmit_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton15073_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton15073_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnSave != null) {
                btnSave.Dispose ();
                btnSave = null;
            }

            if (btnSubmit != null) {
                btnSubmit.Dispose ();
                btnSubmit = null;
            }

            if (txtCarName != null) {
                txtCarName.Dispose ();
                txtCarName = null;
            }

            if (txtResults != null) {
                txtResults.Dispose ();
                txtResults = null;
            }

            if (txtYear != null) {
                txtYear.Dispose ();
                txtYear = null;
            }

            if (swDefault != null)
            {
                swDefault.Dispose();
                swDefault = null;
            }
        }
    }
}