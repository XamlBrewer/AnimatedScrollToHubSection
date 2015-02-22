namespace XamlBrewer.Universal.AnimatedHub
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Foundation;
    using Windows.UI.Xaml.Controls;

    // Extensions methods on the Hub Control.
    public static class HubExtensions
    {
        public static HubSection CurrentSection(this Hub hub)
        {
            if (hub.Sections.Count > 0)
            {
                return hub.SectionsInView[0];
            }
            else
            {
                return null;
            }
        }

        public static int CurrentIndex(this Hub hub)
        {
            if (hub.Sections.Count > 0)
            {
                return hub.Sections.IndexOf(hub.CurrentSection());
            }
            else
            {
                return -1;
            }
        }

        public async static Task ScrollToSectionAnimated(this Hub hub, HubSection section)
        {
            // Find the internal scrollviewer and its current horizontal offset.
            var viewer = hub.GetFirstDescendantOfType<ScrollViewer>();
            var current = viewer.HorizontalOffset;

            // Find the distance to scroll.
            var visual = section.TransformToVisual(hub);
            var point = visual.TransformPoint(new Point(0, 0));
            var offset = point.X;

            // Scroll in a more or less animated way.
            var increment = offset / 24;
            for (int i = 1; i < 25; i++)
            {
                viewer.ChangeView((i * increment) + current, null, null, true);
                await Task.Delay(TimeSpan.FromMilliseconds(i));
            }
        }
    }
}
