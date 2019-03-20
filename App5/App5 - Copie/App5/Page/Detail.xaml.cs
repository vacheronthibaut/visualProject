using Akavache;
using App5.Objet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;

namespace App5.Page
{
	public partial class Detail : Xamarin.Forms.TabbedPage
    {
        private ObservableCollection<Place> model;
        private int index;

        public Detail ()
		{
			InitializeComponent ();
            Init();
        }

        public async void Init()
        {
            model = await BlobCache.LocalMachine.GetObject<ObservableCollection<Place>>("model");
            index = await BlobCache.LocalMachine.GetObject<int>("index");

            Map map = new Map(MapSpan.FromCenterAndRadius(new Position(model[index].latitude,model[index].longitude), Distance.FromMiles(10)));

            var pin = new Pin()
            {
                Position = new Position(model[index].latitude, model[index].longitude),
                Label = model[index].title
            };
            map.Pins.Add(pin);
            mymap.Children.Add(map);
        }
    }
}