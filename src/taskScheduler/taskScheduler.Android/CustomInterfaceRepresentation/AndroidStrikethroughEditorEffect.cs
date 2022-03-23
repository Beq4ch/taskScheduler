using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taskScheduler.Droid;
using taskScheduler.Droid.CustomInterfaceRepresentation;
using taskScheduler.Views.RendererAndEffects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("taskScheduler.Views.RendererAndEffects")]
[assembly: ExportEffect(typeof(AndroidStrikethroughEditorEffect), nameof(StrikethroughEditor))]

namespace taskScheduler.Droid.CustomInterfaceRepresentation
{
    public class AndroidStrikethroughEditorEffect : PlatformEffect
    {
        private PaintFlags _originalFlags;
        protected override void OnAttached()
        {
            var editText = Control as EditText;

            if (editText is null)
                return;

            _originalFlags = editText.PaintFlags;

            editText.PaintFlags = PaintFlags.StrikeThruText;
        }

        protected override void OnDetached()
        {
            var editText = Control as EditText;

            if (editText is null)
                return;

            editText.PaintFlags = _originalFlags;
        }
    }
}