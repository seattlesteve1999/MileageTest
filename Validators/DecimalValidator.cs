using System;
using Xamarin.Forms;

namespace MileageManagerForms.Validators
{
    internal class DecimalValidator : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            decimal result;

            if (args.NewTextValue.Contains(","))
                ((Entry)sender).TextColor = Color.Red;
            else
            {                
                bool isValid = decimal.TryParse(args.NewTextValue, out result);
                ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
            }
        }
    }
}
