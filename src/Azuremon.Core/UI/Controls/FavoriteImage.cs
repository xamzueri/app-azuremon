using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Azuremon.Core.UI.Controls
{
    public class FavoriteImage : Image
    {
        private bool _addedAnimation;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (_addedAnimation || GestureRecognizers.Count == 0)
                return;

            _addedAnimation = true;

            var tapGesture = GestureRecognizers[0] as TapGestureRecognizer;
            if (tapGesture == null)
                return;

            tapGesture.Tapped += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() => Grow());
            };

        }

        /// <summary>
        /// Play animation to grow and shrink
        /// </summary>
        public async Task Grow()
        {

            await this.ScaleTo(1.4, 75);
            await this.ScaleTo(1.0, 75);

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    this.ScaleTo(1.4, 75).ContinueWith((t) =>
                    {
                        try
                        {
                            this.ScaleTo(1.0, 75);
                        }
                        catch
                        {
                        }
                    },
                        scheduler: TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch
                {
                }
            });
        }
    }
}
