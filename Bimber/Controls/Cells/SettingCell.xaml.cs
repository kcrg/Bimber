using Xamarin.Forms;

namespace Bimber.Controls.Cells
{
    public partial class SettingCell : StackLayout
    {
        public SettingCell()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SettingCell), default(string), BindingMode.OneWay);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(SettingCell), default(string), BindingMode.OneWay);
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        protected override void OnPropertyChanged(string? propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
            {
                titleLabel.Text = Title;
            }
            else if (propertyName == DescriptionProperty.PropertyName)
            {
                descriptionLabel.Text = Description;
            }
        }
    }
}