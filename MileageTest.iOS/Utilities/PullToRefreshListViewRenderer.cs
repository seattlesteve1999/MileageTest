using MileageManagerForms.Utilities;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRendererAttribute(typeof(PullToRefreshListViewRenderer), typeof(PullToRefreshListViewRenderer))]
namespace MileageManagerForms.Utilities
{
    public class PullToRefreshListViewRenderer : ListViewRenderer
    {
        public PullToRefreshListViewRenderer()
        {       
        }

        public PullToRefreshListView refreshControl;
        
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (refreshControl != null)
                return;
                
            var pullToRefreshListView = (PullToRefreshListView)Element;

            refreshControl = new RefreshControl();
            refreshControl.RefreshCommand = pullToRefreshListView.RefreshCommand;
            refreshControl.Message = pullToRefreshListView.Message;
            this.refreshControl.AddSubview(refreshControl);
        }

        public static explicit operator PullToRefreshListViewRenderer(ListView v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Raises the element property changed event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var pullToRefreshListView = this.Element as PullToRefreshListView;
            if (pullToRefreshListView == null)
                return;

            if (e.PropertyName == PullToRefreshListView.IsRefreshingProperty.PropertyName)
            {
                refreshControl.IsRefreshing = pullToRefreshListView.IsRefreshing;
            }
            else if (e.PropertyName == PullToRefreshListView.MessageProperty.PropertyName)
            {
                refreshControl.Message = pullToRefreshListView.Message;
            }
            else if (e.PropertyName == PullToRefreshListView.RefreshCommandProperty.PropertyName)
            {
                refreshControl.RefreshCommand = pullToRefreshListView.RefreshCommand;
            }
        }
    }
}
	
			
