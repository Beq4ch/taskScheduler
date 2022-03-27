using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taskScheduler.Droid.CustomInterfaceRepresentation;
using taskScheduler.Views.RendererAndEffects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(XButton), typeof(XButtonRenderer))]

namespace taskScheduler.Droid.CustomInterfaceRepresentation
{
    public class XButtonRenderer : ButtonRenderer
    {
        public XButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            var button = this.Control;
            button.SetAllCaps(false);
            button.Gravity = GravityFlags.ClipHorizontal;
        }
    }
}