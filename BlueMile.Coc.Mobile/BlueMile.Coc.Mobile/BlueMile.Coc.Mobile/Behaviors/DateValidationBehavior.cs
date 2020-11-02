using System;
using Xamarin.Forms;

namespace BlueMile.Coc.Mobile.Behaviours
{
    public class DateValidationBehavior : Behavior<Entry>
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
            DateTime result;
            _ = DateTime.TryParse(args.NewTextValue, out result);
            bool isValid = DateTime.Compare(DateTime.Today.AddMonths(6), result) < 0;
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
