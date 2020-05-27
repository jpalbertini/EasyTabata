using EasyTabata.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTabata.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeSelector : StackLayout
    {
        public Duration TimeValue
        {
            get { return (Duration)GetValue(TimeValueProperty); }
            set { SetValue(TimeValueProperty, value); }
        }
        public static readonly BindableProperty TimeValueProperty = BindableProperty.Create("TimeValue", typeof(Duration), typeof(TimeSelector), new Duration());

        public bool Editable
        {
            get { return (bool)GetValue(EditableProperty); }
            set { SetValue(EditableProperty, value); }
        }
        public static readonly BindableProperty EditableProperty = BindableProperty.Create("Editable", typeof(bool), typeof(TimeSelector), true);
        public int FontSize
        {
            get { return (int)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(int), typeof(TimeSelector), 12);

        public TimeSelector()
        {
            BindingContext = this;
            InitializeComponent();
        }
    }
}