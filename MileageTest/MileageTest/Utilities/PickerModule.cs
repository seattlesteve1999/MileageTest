using MileageManagerForms.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace MileageManagerForms.Utilities
{
    public class PickerModule : UIPickerViewModel
    {
        readonly List<Auto> Auto;
        public EventHandler ValueChanged;
        public string SelectedValue;
        private readonly Task<List<Auto>> resp;

        public PickerModule(List<Auto> Auto)
        {
            this.Auto = Auto;
        }

        public PickerModule(Task<List<Auto>> resp)
        {
            this.resp = resp;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return Auto.Count;
        }
        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }
        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return Auto[(int)row].Name;
        }
        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            int auto = Auto[(int)row].Id;
            SelectedValue = auto.ToString();
            ValueChanged?.Invoke(null, null);
        }
    }
}
