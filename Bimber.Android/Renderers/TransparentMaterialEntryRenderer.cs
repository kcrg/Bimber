using Android.Content;
using Bimber.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;
using Xamarin.Forms.Platform.Android;

// Workaround for https://github.com/xamarin/Xamarin.Forms/issues/13229
[assembly: ExportRenderer(typeof(Entry), typeof(TransparentMaterialEntryRenderer), new[] { typeof(VisualMarker.MaterialVisual) })]
namespace Bimber.Droid.Renderers
{
    public class TransparentMaterialEntryRenderer : MaterialEntryRenderer
    {
        public TransparentMaterialEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control?.EditText.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}