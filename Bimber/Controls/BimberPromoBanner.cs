using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Bimber.Controls
{
    public class BimberPromoBanner : Frame
    {
        public BimberPromoBanner()
        {
            Padding = new Thickness(12);
            Margin = new Thickness(8, 0);
            CornerRadius = 8;

            Build();
        }

        private void Build()
        {
            this.Content = new StackLayout
            {
                Spacing = 0,

                Children =
                {
                     new Label { TextColor = Color.FromHex("#424242"), HorizontalOptions = LayoutOptions.Center } .FormattedText (
                         new Span { FontSize = 18, FontFamily = "FontIcons" }.DynamicResource(Span.TextProperty, "AppIcon"),
                         new Span { FontSize = 16, FontFamily = "LatoBold", Text = " Bimber " },
                         new Span { FontSize = 10, FontFamily = "LatoSemiBold", Text = "PLATINUM" }),

                     new Label { TextColor = Color.FromHex("#b9b8be"), HorizontalOptions = LayoutOptions.Center, Text = "Randki w klasie premium" }
                }
            };
        }

        //public static BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(BimberPromoBanner), default(string), BindingMode.OneWay);
        //public string Description
        //{
        //    get => (string)GetValue(DescriptionProperty);
        //    set => SetValue(DescriptionProperty, value);
        //}

        //public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(BimberPromoBanner), default(Color), BindingMode.OneWay);
        //public Color AccentColor
        //{
        //    get => (Color)GetValue(AccentColorProperty);
        //    set => SetValue(AccentColorProperty, value);
        //}
    }
}