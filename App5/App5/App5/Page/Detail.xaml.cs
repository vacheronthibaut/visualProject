using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace App5.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Detail : Xamarin.Forms.TabbedPage
    {
		public Detail ()
		{
			InitializeComponent ();

            /*
            var position = new Position(37, -122); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "custom pin",
                Address = "custom detail info"
            };
            MyMap.Pins.Add(pin);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(5 * 1.5)));*/
        }
    }
}