using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Quicky.SportsApp.UserControls
{
    public class CustomPicker : Picker
    {
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == Picker.SelectedIndexProperty.PropertyName)
            {
                this.InvalidateMeasure();
            }
        }
    }
}
