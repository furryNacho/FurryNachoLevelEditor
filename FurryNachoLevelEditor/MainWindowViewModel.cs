using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

namespace FurryNachoLevelEditor
{
    public class MainWindowViewModel
    {

        public ICollectionView Customers { get; private set; }
   //     public ICollectionView GroupedCustomers { get; private set; }


        public MainWindowViewModel()
        {

            var _customers = new List<int>
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
            };

            //var _customers = new List<Tile>
            //{
            //    new Tile
            //    {
            //        AttributeNumber = 1,
            //        TileNumber = 0

            //    },
            //    new Tile
            //    {
            //        AttributeNumber = 2,
            //        TileNumber = 0
            //    },
            //    new Tile
            //    {
            //        AttributeNumber = 3,
            //        TileNumber = 0
            //    },
            //    new Tile
            //    {
            //        AttributeNumber = 3,
            //        TileNumber = 0
            //    }
            //};

            Customers = CollectionViewSource.GetDefaultView(_customers);

          //  GroupedCustomers = new ListCollectionView(_customers);
         //   GroupedCustomers.GroupDescriptions.Add(new PropertyGroupDescription("Gender"));

        }
    }
}
