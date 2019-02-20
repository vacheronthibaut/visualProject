using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App3.Object
{
    class ListeLieux
    {
        public static ObservableCollection<Lieu> LL { get; }

        static ListeLieux()
        {
            LL = new ObservableCollection<Lieu>()
                {
                    new Lieu("l1","d1"),
                    new Lieu("l2","d2"),
                    new Lieu("l3","d3"),
                    new Lieu("l4","d4"),
                    new Lieu("l5","d5"),
                };
        }
    }
}
