using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FurryNachoLevelEditor.olcAssets;
using Newtonsoft.Json;

namespace FurryNachoLevelEditor
{
    public class TileSheet
    {
        public int nWidth;
        public int nHeight;
        public string sName;
        public Bitmap pSprite;


        public int NumberOfTilesWidth;
        public int NumberOfTilesHeight;
        public int NumberOfTiles;


        private int[] m_solids = null;
        private int[] m_indices = null;





        public bool Create(string fileData, Bitmap sprite, string name)
        {

            sName = name; // namnet på tilesheet
            pSprite = sprite; // Sätt sprite att vara tilesheet


            SettingsObj settingsObj = new SettingsObj();

            using (StreamReader r = new StreamReader(fileData)) // Läs settingsfilen
            {
                string json = r.ReadToEnd();
                settingsObj = JsonConvert.DeserializeObject<SettingsObj>(json);
            }

            nHeight = settingsObj.LvlHeight; // det som ska rita ut griddens antal rader 
            nWidth = settingsObj.LvlWidth;  // det som ska rita ut griddens bredd på rader


            NumberOfTilesWidth = settingsObj.NumberOfTilesWidth;
            NumberOfTilesHeight = settingsObj.NumberOfTilesHeight;
            NumberOfTiles = settingsObj.NumberOfTilesWidth * settingsObj.NumberOfTilesHeight; // Antalet tiles som ska visas i tilestabben


            m_solids = new int[settingsObj.LvlWidth * settingsObj.LvlHeight]; // det som ska innehålla indexerat om tile är solid
            m_indices = new int[settingsObj.LvlWidth * settingsObj.LvlHeight]; // det som indexerat ska innehålla vilken tile (index på spritesheet) som ska visas i cell



            int currIdx = 0;
            for (int i = 0; i < settingsObj.NumberOfTilesHeight; i++)
            {

                for (int j = 0; j < settingsObj.NumberOfTilesWidth; j++)
                {
                    // Jävligt osäker på den här
                    var y = i * settingsObj.TileWidthPX;
                    var x = j * settingsObj.TileHeightPX;
                    Rectangle cloneRect = new Rectangle(x, y, settingsObj.TileWidthPX, settingsObj.TileHeightPX);
                    System.Drawing.Imaging.PixelFormat format =
                        pSprite.PixelFormat;
                    Image cloneBitmap = (Image)pSprite.Clone(cloneRect, format);

                    //TODO: path

                    string fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory, @"Content\Load\Tiles");

                    bool exists = System.IO.Directory.Exists(fileLocation);

                    if (!exists)
                    {
                        try
                        {

                        }
                        catch (Exception e)
                        {

                            throw;
                        }
                    }


                    var imgFilePath =
                        fileLocation +
                        @"\img" + currIdx + ".jpg";

                    cloneBitmap.Save(imgFilePath, ImageFormat.Jpeg);




                    currIdx++;
                }
            }


            return true;
        }


    }


    public class TilesModel : TileSheet
    {
        public CreateObj CreateObj { get; set; }
        public TilesModel()
        {


            string fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory, @"Content\Settings\settings.json");
            if (!File.Exists(fileLocation))
            {
                // Log
            }


            this.CreateObj = new CreateObj()
            {
                fileData = fileLocation,
                spriteAsBitmap = Assets.Instance.GetSprite("TileSheet"),
                name = "Tile sheet assets",
            };

            CrateFromChild();
        }
        public bool CrateFromChild()
        {
            return this.Create(CreateObj.fileData, CreateObj.spriteAsBitmap, CreateObj.name);
        }
    }


    public class CreateObj
    {
        public string fileData { get; set; }

        public string name { get; set; }

        public Bitmap spriteAsBitmap { get; set; }

    }
}
