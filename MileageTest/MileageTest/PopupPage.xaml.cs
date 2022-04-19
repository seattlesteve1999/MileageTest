using MileageManagerForms.DataAccess.Interfaces;
using MileageManagerForms.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;

namespace MileageManagerForms
{
    public partial class PopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {       
        public enum EnumAction
        {
            Delete,
            Cancel
        }

        private TaskCompletionSource<EnumAction> task;

        public PopupPage(TaskCompletionSource<EnumAction> taskCompletion)
        {
            InitializeComponent();
            task = taskCompletion;
        }

        public PopupPage()
        {
            InitializeComponent();
        }

        void DeleteAutoData(object sender, EventArgs args)
        {
            UpdateCarsViewModel ucvm = new UpdateCarsViewModel();
            ucvm.DeleteCar();
        }

        void Cancel(object sender, EventArgs args)
        {
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}