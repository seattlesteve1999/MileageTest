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
    [Register ("ViewNotesViewController")]
    partial class ViewNotesViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MileageManagerForms.iOS.ViewNotesViewController btnCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfNote { get; set; }

        [Action ("BtnCancel_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnCancel_TouchUpInside (MileageManagerForms.iOS.ViewNotesViewController sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnCancel != null) {
                btnCancel.Dispose ();
                btnCancel = null;
            }

            if (tfDate != null) {
                tfDate.Dispose ();
                tfDate = null;
            }

            if (tfNote != null) {
                tfNote.Dispose ();
                tfNote = null;
            }
        }
    }
}