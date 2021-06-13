using System;
using Xamarin.Forms;

namespace Bimber.Views
{
    public partial class ProfilePage : ContentView
    {
        public ProfilePage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(5), (Func<bool>)(() =>
            {
                carouselView.Position = (carouselView.Position + 1) % 8;

                return true;
            }));
        }
    }
}