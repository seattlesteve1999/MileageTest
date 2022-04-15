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
    [Register ("iCloudProcessViewController")]
    partial class iCloudProcessViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView actyIndicator { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnBackup { get; set; }

        [Action ("BtnBackup_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnBackup_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton106730_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton106730_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton111632_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton111632_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton133708_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton133708_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (actyIndicator != null) {
                actyIndicator.Dispose ();
                actyIndicator = null;
            }

            if (btnBackup != null) {
                btnBackup.Dispose ();
                btnBackup = null;
            }
        }
    }
}