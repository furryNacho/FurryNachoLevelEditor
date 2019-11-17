using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FurryNachoLevelEditor.olcAssets;
using Newtonsoft.Json;

namespace FurryNachoLevelEditor
{
    public class Horse
    {

        public TilesModel currentTile { get; set; }

        public Horse()
        {
    
        }



        public void ReadSettings()
        {
           

            SettingsObj movie1 = JsonConvert.DeserializeObject<SettingsObj>(File.ReadAllText(@"C:\Users\kim_k\source\repos\FurryNachoLevelEditor\FurryNachoLevelEditor\Content\Settings\settings.json"));

        }

        public void WriteSettings()
        {
            SettingsObj defaultTestObj = new SettingsObj();


            

            // serialize JSON to a string and then write string to a file
            File.WriteAllText(@"C:\Users\kim_k\source\repos\FurryNachoLevelEditor\FurryNachoLevelEditor\Content\Settings\settings.json", JsonConvert.SerializeObject(defaultTestObj));


        }



        public void WriteTxtToDisk()
        {


         
            string text = "Skapa ett objekt som har all min data och skyffla ut den i en text";


           
            System.IO.File.WriteAllText(@"C:\Users\kim_k\source\repos\FurryNachoLevelEditor\FurryNachoLevelEditor\Content\Export\mapoutput.txt", text);
        }


        #region Ladda bilder

        public void SetUpSprites()
        {

            Assets.Instance.LoadSprites();

             currentTile = new TilesModel();

            // har nu höjd och bredd på gridden som ska ritas ut
            var höjdpågriddensomskaritas = currentTile.nHeight;
            var breddpågriddensomskaritas = currentTile.nWidth;

            // Hur många tiles som ska kunna väljas
            var antaltilesattväljapå = currentTile.NumberOfTiles;



        }

        #endregion

    }


    public class SettingsObj
    {
        public int LvlWidth { get; set; } = 64;
        public int LvlHeight { get; set; } = 32;


        public int TileWidthPX { get; set; } = 16;
        public int TileHeightPX { get; set; } = 16;

        public int NumberOfTilesWidth { get; set; } = 5;
        public int NumberOfTilesHeight { get; set; } = 5;
    }

}
