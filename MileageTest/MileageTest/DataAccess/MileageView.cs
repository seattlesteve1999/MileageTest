using System;
using System.ComponentModel;
using UIKit;

namespace MileageManagerForms.DataAccess
{
    public class MileageView
    {
        public string Id { get; set; }
        public UILabel Date { get; set; }
        public UILabel Miles { get; set; }
        public UILabel Gas { get; set; }
        public UILabel MPG { get; set; }
        public UILabel Cost { get; set; }
        public Action<object, PropertyChangedEventArgs> PropertyChanged { get; internal set; }
    }
}