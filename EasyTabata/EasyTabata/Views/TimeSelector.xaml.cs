﻿using EasyTabata.Models;
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
            get { 
                return (Duration)GetValue(TimeValueProperty); 
            }
            set {
                if (TimeValue != value)
                    SetValue(TimeValueProperty, value);
            }
        }
        public static readonly BindableProperty TimeValueProperty = BindableProperty.Create("TimeValue", typeof(Duration), typeof(TimeSelector), new Duration());

        public TimeSelector()
        {
            BindingContext = this;
            InitializeComponent();
        }
    }
}