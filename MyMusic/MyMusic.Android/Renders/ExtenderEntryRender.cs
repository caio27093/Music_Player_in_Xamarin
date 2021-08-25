using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using MyMusic.Droid.Renders;
using MyMusic.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(ExtenderEntry), typeof(ExtenderEntryRender))]
namespace MyMusic.Droid.Renders
{
    public class ExtenderEntryRender: EntryRenderer
    {

        public ExtenderEntryRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {

            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            }
            else
            {
                Control.Background.SetColorFilter(Android.Graphics.Color.Transparent, PorterDuff.Mode.SrcAtop);

            }
        }

    }
}