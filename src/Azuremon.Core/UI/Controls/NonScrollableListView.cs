using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Azuremon.Core.UI.Controls
{
    public class NonScrollableListView : ListView
    {

        public NonScrollableListView()
            : base(ListViewCachingStrategy.RecycleElement)
        {
            
        }
    }
}
