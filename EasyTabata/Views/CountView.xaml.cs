namespace EasyTabata.Views;
public partial class CountView : AbsoluteLayout
{
    public string ValueName
    {
        get { return (string)GetValue(ValueNameProperty); }
        set { SetValue(ValueNameProperty, value); }
    }
    public static readonly BindableProperty ValueNameProperty = BindableProperty.Create("ValueName", typeof(string), typeof(PlayingTabataView));

    public int CurrentValue
    {
        get { return (int)GetValue(CurrentValueProperty); }
        set { SetValue(CurrentValueProperty, value); }
    }
    public static readonly BindableProperty CurrentValueProperty = BindableProperty.Create("CurrentValue", typeof(int), typeof(PlayingTabataView));
    public int MaxValue
    {
        get { return (int)GetValue(MaxValueProperty); }
        set { SetValue(MaxValueProperty, value); }
    }
    public static readonly BindableProperty MaxValueProperty = BindableProperty.Create("MaxValue", typeof(int), typeof(PlayingTabataView));
    public string BoxColor
    {
        get { return (string)GetValue(BoxColorProperty); }
        set { SetValue(BoxColorProperty, value); }
    }
    public static readonly BindableProperty BoxColorProperty = BindableProperty.Create("BoxColor", typeof(string), typeof(PlayingTabataView));

    public CountView()
    {
        BindingContext = this;
        InitializeComponent();
    }
}