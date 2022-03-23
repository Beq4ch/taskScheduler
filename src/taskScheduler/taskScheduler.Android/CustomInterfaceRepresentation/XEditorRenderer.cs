using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.BehaviorsPack;
using Xamarin.Forms.Xaml;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using taskScheduler.CustomInterfaceRepresentation;

[assembly: ExportRenderer(typeof(XEditor), typeof(XEditorRenderer))]
namespace taskScheduler.CustomInterfaceRepresentation
{
    [Obsolete]
    public class XEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var gradientDrawble = new GradientStrokeDrawable();
                gradientDrawble.SetCornerRadius(40f);
                gradientDrawble.SetStroke(5, Android.Graphics.Color.Transparent);
                gradientDrawble.SetColor(Android.Graphics.Color.White);
                Control.SetBackground(gradientDrawble);
            }
        }
    }
}
