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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Horse worker { get; set; }

        private ScriptingHelper HS = null;

        public MainWindow()
        {
            InitializeComponent();



            DataContext = new MainWindowViewModel();

            worker = new Horse();

            worker.SetUpSprites();

            HS = new ScriptingHelper();


            
            string fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory, @"Content\Settings\favIconNacho.png");

            if (!File.Exists(fileLocation))
            {
                try
                {
                    //@"C:\Users\kim_k\source\repos\FurryNachoLevelEditor\FurryNachoLevelEditor\Content\Settings\favIconNacho.png"
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            Uri iconUri =
                new Uri(
                    fileLocation,
                    UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }




        //void selectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        //{

        //    var getValue = e.AddedCells[0].Item.ToString();

        //    //MessageBox.Show("Clicked "+ getValue);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            string fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory, @"View.html");

            if (!File.Exists(fileLocation))
            {
                // "C:\\Users\\kim_k\\source\\repos\\FurryNachoLevelEditor\\FurryNachoLevelEditor\\bin\\Debug\\View.html"
            }


            //string uri = AppDomain.CurrentDomain.BaseDirectory + "View.html";
            //string output = uri.Substring(0, uri.IndexOf("bin"));
            //output = output + "View.html";


            browser.Navigate(new Uri(fileLocation, UriKind.Absolute));
            //browser.Navigate(new Uri(output, UriKind.Absolute));
            browser.LoadCompleted += webBrowser1_LoadCompleted;


            //browser.ObjectForScripting = new ScriptingHelper();

            browser.ObjectForScripting = HS;

        }

        void webBrowser1_LoadCompleted(object sender, NavigationEventArgs e)
        {
            dynamic document = browser.Document;



            try
            {
                // Be silent 
                dynamic script = document.createElement("script");
                script.type = @"text/javascript";
                script.text = @"window.onerror = function(msg,url,line){return true;}";
                document.head.appendChild(script);

            }
            catch (Exception exception)
            {

            }




            try
            {
               

            }
            catch (Exception exception)
            {

            }




            try
            {
                // när man klickar på flikarna
            }
            catch (Exception exception)
            {

            }

            try
            {
 



            }
            catch (Exception exception)
            {

            }





            try
            {
                // knappar attribut
            }
            catch (Exception exception)
            {

            }

            try
            {
                // knappar settings



            }
            catch (Exception exception)
            {

            }



            HS.obj = new SettingsObj()
            {
                LvlHeight = worker.currentTile.nHeight,
                LvlWidth = worker.currentTile.nWidth,
                NumberOfTilesWidth = worker.currentTile.NumberOfTilesWidth,
                NumberOfTilesHeight = worker.currentTile.NumberOfTilesHeight,
                TileWidthPX = 16,
                TileHeightPX = 16
            };


        }



    }




    //

    [ComVisible(true)]
    public class ScriptingHelper
    {
        //  public int antaltiles = 0;

        public SettingsObj obj = null;

        // https://stackoverflow.com/questions/470222/hosting-and-interacting-with-a-webpage-inside-a-wpf-app


        public string InitJs(string message)
        {

            var proxyJson = "LvlHeight:" + obj.LvlHeight.ToString() + "/";
            proxyJson += "LvlWidth:" + obj.LvlWidth.ToString() + "/";
            proxyJson += "NumberOfTilesWidth:" + obj.NumberOfTilesWidth.ToString() + "/";
            proxyJson += "NumberOfTilesHeight:" + obj.NumberOfTilesHeight.ToString() + "/";
            proxyJson += "TileWidthPX:" + obj.TileWidthPX.ToString() + "/";
            proxyJson += "TileHeightPX:" + obj.TileHeightPX.ToString();


            return proxyJson;
            //return antaltiles;
        }


        public void ShowMessage(string message)
        {

            MessageBox.Show(message);
        }



        public bool ButtonClick(string nameOfButton)
        {
            switch (nameOfButton)
            {
                case "save":
                    break;

                case "load":

                    break;

                case "export":
                    break;
                case "exit":
                    //Environment.Exit(0);
                    System.Windows.Application.Current.Shutdown();
                    break;
                default:
                    return false;
            }

            return true;
        }

        private int[] arr1 = new int[5];
        private int[] arr2 = new int[5];
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
                lvlJsonObj.AttributeIndex = Array.ConvertAll(sArrayAtt, int.Parse);

                string json = JsonConvert.SerializeObject(lvlJsonObj);
                System.IO.File.WriteAllText(@"C:\Users\kim_k\source\repos\FurryNachoLevelEditor\FurryNachoLevelEditor\Content\Export\mapoutput.txt", json);

                return true;
            }
            catch (Exception e)
            {
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
    //

}
