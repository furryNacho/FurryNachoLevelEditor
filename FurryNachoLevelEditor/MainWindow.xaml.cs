using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Button = System.Windows.Controls.Button;
using Image = System.Windows.Controls.Image;
using MessageBox = System.Windows.MessageBox;
using TabControl = System.Windows.Controls.TabControl;




namespace FurryNachoLevelEditor
{
    public partial class MainWindow : Window
    {
        //Global var's
        private ScriptingHelper SH = null;
        public bool Development { get; set; } = false;
        public string AbsolutPath { get; set; } = @"C:\FurryNachoLevelEditor\View.html";

        //Init
        public MainWindow()
        {
            InitializeComponent();// Nåt wpf bara gör.
            DataContext = new MainWindowViewModel();

            SH = new ScriptingHelper();// init SH

            #region Ikon
            string fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory, @"Content\Settings\favIconNacho.png");
            if (!File.Exists(fileLocation))
            {
                // Log
            }
            Uri iconUri =
                new Uri(
                    fileLocation,
                    UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            #endregion
        }


        //När fönstret är laddat 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // För att avgöra vilken mappstruktur som sak användas.
            string fileLocation = "";
            if (Development)
            {
                //absolut 
                fileLocation = AbsolutPath;
            }
            else
            {
                //relative
                fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory, @"View.html");
            }

            if (!File.Exists(fileLocation))
            {
                // Log
            }


            browser.Navigate(new Uri(fileLocation, UriKind.Absolute));

            // lägger på event för att kunna köra när laddningen är klar.
            browser.LoadCompleted += webBrowser1_LoadCompleted;

            // tilldela script helper på browser objektet.
            browser.ObjectForScripting = SH;
        }


        void webBrowser1_LoadCompleted(object sender, NavigationEventArgs e)
        {
            dynamic document = browser.Document;

            // Ett sätt att hantera felmeddelande är att tysta felmedelande. 
            try
            {
                dynamic script = document.createElement("script");
                script.type = @"text/javascript";
                script.text = @"window.onerror = function(msg,url,line){return true;}";
                document.head.appendChild(script);

            }
            catch (Exception exception)
            {
                // Log
            }

        }
    }


    // Saker som kallas på från js
    [ComVisible(true)]
    public class ScriptingHelper
    {
        public SettingsObj obj { get; set; } = new SettingsObj();
        private Worker worker { get; set; }

        private string SavedMapValTiles { get; set; }
        private string SavedMapValAttributes { get; set; }

        public ScriptingHelper()
        {
            worker = new Worker(); // init worker
            worker.SetUpSprites(); // laddar sprite sheet // TODO tror jag behöver denna i sh också eller motsvarande, som kanske tar parameter också
            SetSettingsObj(worker);
        }

        // Sätt värde på det objekt som skickas till vyn
        public void SetSettingsObj(Worker workerParam)
        {
            obj.LvlHeight = workerParam.currentTile.nHeight;
            obj.LvlWidth = workerParam.currentTile.nWidth;
            obj.NumberOfTilesWidth = workerParam.currentTile.NumberOfTilesWidth;
            obj.NumberOfTilesHeight = workerParam.currentTile.NumberOfTilesHeight;
            obj.TileWidthPX = 16;
            obj.TileHeightPX = 16;
        }




        // js vill ha värden för att rita grid. Ropas på ifrån view.
        public string InitJs(string message)
        {
            var proxyJson = "LvlHeight:" + obj.LvlHeight.ToString() + "/";
            proxyJson += "LvlWidth:" + obj.LvlWidth.ToString() + "/";
            proxyJson += "NumberOfTilesWidth:" + obj.NumberOfTilesWidth.ToString() + "/";
            proxyJson += "NumberOfTilesHeight:" + obj.NumberOfTilesHeight.ToString() + "/";
            proxyJson += "TileWidthPX:" + obj.TileWidthPX.ToString() + "/";
            proxyJson += "TileHeightPX:" + obj.TileHeightPX.ToString();


            return proxyJson;
        }

        // js vill ha värden för att rita grid. Ropas på ifrån view.
        public string GetValueSavedMap()
        {




            var proxyJson = "LvlHeight:" + obj.LvlHeight.ToString() + "/";
            proxyJson += "LvlWidth:" + obj.LvlWidth.ToString() + "/";
            proxyJson += "NumberOfTilesWidth:" + obj.NumberOfTilesWidth.ToString() + "/";
            proxyJson += "NumberOfTilesHeight:" + obj.NumberOfTilesHeight.ToString() + "/";
            proxyJson += "TileWidthPX:" + obj.TileWidthPX.ToString() + "/";
            proxyJson += "TileHeightPX:" + obj.TileHeightPX.ToString();


            return proxyJson;
        }


        // Knappar som klickas på från vyn som inte kräver haxiga parametrar. Ropas på ifrån view.
        public string ButtonClick(string nameOfButton)
        {
            var Msg = "";
            switch (nameOfButton)
            {

                // TODO kanske räcker med en ändå, få in värde på vilken map och sprite sheet so mska användas och gör om laddningen
                case "load":
                    break;
                case "loadmap":
                    break;


                case "export":
                    // Kommer inte ska. Har fått en egen metod
                    break;
                case "exit":
                    //Environment.Exit(0);
                    System.Windows.Application.Current.Shutdown();
                    break;
                default:
                    Msg += "Could not find matching switch value on 'ButtonClick'";
                    break;
            }

            return Msg;
        }

        // Funktion för att importera värden sparad map. Ropas på från vyn
        public string Import()
        {
            try
            {
                string fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory, @"Content\Export\mapoutput.json");
                if (!File.Exists(fileLocation))
                {
                    return "Could not find [map].json file.";
                }
                else
                {
                    LevelJsonObj SavedMap = JsonConvert.DeserializeObject<LevelJsonObj>(File.ReadAllText(fileLocation));
                    
                    SavedMapValTiles = string.Join(",", SavedMap.TileIndex);
                    SavedMapValAttributes = string.Join(",", SavedMap.AttributeIndex);

                    return "OK";
                }

            }
            catch (Exception e)
            {
                return "Exception Import: "+e.ToString();
            }
        }
        // Funktion för att hämta värden sparad map. Ropas på från vyn
        public string GetSavedMapVal(string nameOfStringToGet)
        {
            if (nameOfStringToGet == "tiles")
            {
                return SavedMapValTiles;
            }
            else if (nameOfStringToGet == "attributes")
            {
                return SavedMapValAttributes;
            }
            else
            {
                return "";
            }
        }

        // Knapp som kräver haxiga parametrar, Export av griden till en json som går att användas i spelet. Ropas på ifrån view.
        public bool Export(dynamic dynamicObj)
        {
            try
            {
                var lvlJsonObj = new LevelJsonObj();

                lvlJsonObj.Width = int.Parse(dynamicObj.width);
                lvlJsonObj.Height = int.Parse(dynamicObj.height);

                var tilesIndexString = (string)dynamicObj.indextileprop;
                var sArrayTiles = tilesIndexString.Split(';');
                lvlJsonObj.TileIndex = Array.ConvertAll(sArrayTiles, int.Parse);

                var attIndexString = (string)dynamicObj.indexAttprop;
                var sArrayAtt = attIndexString.Split(';');
                lvlJsonObj.AttributeIndex = Array.ConvertAll(sArrayAtt, int.Parse); // TODO: undefined?! // sista blir undefined 1600, tycks inte funka som tänkt


                var givenName = (string)dynamicObj.indexAttprop;


                string fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory, @"Content\Export\mapoutput.json");
                if (!File.Exists(fileLocation))
                {
                    // Log
                }

                string json = JsonConvert.SerializeObject(lvlJsonObj);
                System.IO.File.WriteAllText(fileLocation, json);




                return true;
            }
            catch (Exception e)
            {
                // Log
                return false;
            }

        }


        public class LevelJsonObj
        {
            public int[] TileIndex { get; set; }
            public int[] AttributeIndex { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }


    }

}
