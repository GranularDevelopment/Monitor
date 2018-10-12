using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using CoreGraphics;
<<<<<<< HEAD:Monitor.iOS/Renderers/NoHelpEntryRenderer.cs
using Monitor.iOS;
using Monitor.Renderers;
=======
<<<<<<< Updated upstream:iOS/Renderers/NoHelpEntryRenderer.cs
using GranularMonitorSystem.iOS;
using GranularMonitorSystem.Common.Renderers;
=======
using Monitor.iOS;
using Monitor.Renderers;
using Akavache;
>>>>>>> Stashed changes:Monitor.iOS/Renderers/NoHelpEntryRenderer.cs
>>>>>>> master:iOS/Renderers/NoHelpEntryRenderer.cs

[assembly: ExportRenderer(typeof(NoHelperEntry), typeof(NoHelperEntryRenderer))]
namespace Monitor.iOS
{
    public class NoHelperEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged (e);
            if (Control != null) {
                Control.SpellCheckingType = UITextSpellCheckingType.No;             // No Spellchecking
                Control.AutocorrectionType = UITextAutocorrectionType.No;           // No Autocorrection
                Control.AutocapitalizationType = UITextAutocapitalizationType.None; // No Autocapitalization
            }
        }
    }
}