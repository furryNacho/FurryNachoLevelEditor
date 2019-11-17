using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using FurryNachoLevelEditor.olcAssets;

namespace FurryNachoLevelEditor
{
    public class Assets
    {
        Assets()
        {
        }
        private static readonly object padlock = new object();
        private static Assets instance = null;
        public static Assets Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Assets();
                    }
                    return instance;
                }
            }
        }
       // private Dictionary<string, Sprite> m_mapSprites { get; set; } = new Dictionary<string, Sprite>();

        private Dictionary<string, Bitmap> m_mapSpritesBitMap { get; set; } = new Dictionary<string, Bitmap>();

        public Bitmap GetSprite(string name)
        {

            Bitmap sprite;
            if (m_mapSpritesBitMap.TryGetValue(name, out sprite))
            {
                return sprite;
            }
            else
            {
                return null;
            }

        }

        //public Sprite GetSprite(string name)
        //{
        //    Sprite sprite;
        //    if (m_mapSprites.TryGetValue(name, out sprite))
        //    {
        //        return sprite;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public void LoadSprites()
        {
         
           // string basePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            string fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory, @"Content\Load\testtilesheet.bmp");

            if (!File.Exists(fileLocation))
            {
                try
                {
                
                }
                catch (Exception e)
                {
                    
                    throw;
                }
            }


            //Load("TileSheet", @"C:\Users\kim_k\source\repos\FurryNachoLevelEditor\FurryNachoLevelEditor\Content\Load\testtilesheet.bmp");
            Load("TileSheet", fileLocation);
        }
        private void Load(string sName, string sFileName)
        {

            Bitmap myBitmap = new Bitmap(sFileName);
            m_mapSpritesBitMap.Add(sName, myBitmap);

            //var sprite = Sprite.Load(sFileName);
            //m_mapSprites.Add(sName, sprite);
        }

       
    }
}
