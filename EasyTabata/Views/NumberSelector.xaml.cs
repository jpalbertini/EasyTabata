using EasyTabata.Models;
using System.Reflection;

namespace EasyTabata.Views;
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
    public int FontSize
    {
        get { return (int)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(int), typeof(TimeSelector), 12);

    public NumberSelector()
    {
        BindingContext = this;
        InitializeComponent();
    }

    private void Plus_Clicked(object sender, EventArgs e)
    {
        if (CurrentValue < MaximumValue)
            CurrentValue++;
        UpdateParentTabata();
    }
    private void Minus_Clicked(object sender, EventArgs e)
    {
        if (CurrentValue > MinimumValue)
            CurrentValue--;
        UpdateParentTabata();
    }

    private void UpdateParentTabata()
    {
        UpdateParentTabata(Parent);
    }

    private void UpdateParentTabata(Element parent)
    {
        Type myType = parent.GetType();
        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
        foreach (PropertyInfo prop in props)
        {
            if (prop.PropertyType == typeof(Tabata))
            {
                Tabata tabata = prop.GetValue(parent, null) as Tabata;
                if (tabata != null)
                    tabata.UpdateDuration();
            }
        }

        if (parent.Parent != null)
            UpdateParentTabata(parent.Parent);
    }
}