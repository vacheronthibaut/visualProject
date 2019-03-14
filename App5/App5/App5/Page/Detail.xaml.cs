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

        public Detail ()
		{
			InitializeComponent ();
            Init();
        }

        public void Init()
        {
            Map map = new Map();
            mymap.Children.Add(map);
        }
    }
}