using MileageManagerForms.DataAccess;
using MileageManagerForms.Database;
using System;
using System.Collections.Generic;
using UIKit;

namespace MileageManagerForms.Utilities
{
    public class AutoPicker : UIPickerViewModel
    {
        string[] names = new string[0];
        string[] ids = new string[0];
        int counter = 0;
        public List<Auto> resp = new List<Auto>();
        public Auto result = new Auto();
        public EventHandler ValueChanged;
        public string SelectedValue;
        public App app;

        public AutoPicker()
        {
            GetAutos();
        }


        public void GetAutos()
        {
            MileageItemRepository repository = new MileageItemRepository();
            List<AutoTableDefination> response = repository.GetAuto2();
            int i = 0;
            names = new string[response.Count];
            ids = new string[response.Count];
            counter = response.Count;           
                      
            if (response.Count > 0)
            {
                foreach (AutoTableDefination item in response)
                {
                    names[i] = item.CarDesc;
                    ids[i] = item.Id.ToString();
                    i++;
                }

                if (Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["firstTime"]))
                {
                    Xamarin.Forms.Application.Current.Properties["enter1stCar"] = false;
                }
            }
            else
            {
                Xamarin.Forms.Application.Current.Properties["enter1stCar"] = true;
            }
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return counter;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return names.Length;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return names[row];            
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            SelectedValue = ids[row];
            ValueChanged?.Invoke(null, null);
            Xamarin.Forms.Application.Current.Properties["rowId"] = row;
        }

        public override nfloat GetComponentWidth(UIPickerView picker, nint component)
        {
            return 240f;
        }

        public override nfloat GetRowHeight(UIPickerView picker, nint component)
        {
            return 95f;
        }
    }
}