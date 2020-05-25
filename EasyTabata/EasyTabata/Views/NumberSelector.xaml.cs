using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTabata.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumberSelector : StackLayout
    {
        public int CurrentValue
        {
            get { return (int)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }
        public static readonly BindableProperty CurrentValueProperty = BindableProperty.Create("CurrentValue", typeof(int), typeof(NumberSelector));

        public int MinimumValue
        {
            get { return (int)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }
        public static readonly BindableProperty MinimumValueProperty = BindableProperty.Create("MinimumValue", typeof(int), typeof(NumberSelector));

        public int MaximumValue
        {
            get { return (int)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }
        public static readonly BindableProperty MaximumValueProperty = BindableProperty.Create("MaximumValue", typeof(int), typeof(NumberSelector));
        public bool Editable
        {
            get { return (bool)GetValue(EditableProperty); }
            set { SetValue(EditableProperty, value); }
        }
        public static readonly BindableProperty EditableProperty = BindableProperty.Create("Editable", typeof(bool), typeof(NumberSelector), true);

        public NumberSelector()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private void Plus_Clicked(object sender, EventArgs e)
        {
            if (CurrentValue < MaximumValue)
                CurrentValue++;
        }
        private void Minus_Clicked(object sender, EventArgs e)
        {
            if (CurrentValue > MinimumValue)
                CurrentValue--;
        }
    }
}