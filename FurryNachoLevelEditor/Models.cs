using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;



using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FurryNachoLevelEditor
{
    public class Tile : INotifyPropertyChanged
    {
        private int _TileNumber;
        private int _AttributeNumber;

        public int TileNumber
        {
            get { return _TileNumber; }
            set
            {
                _TileNumber = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public int AttributeNumber
        {
            get { return _AttributeNumber; }
            set
            {
                _AttributeNumber = value;
                NotifyPropertyChanged("Image");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    public class IntToImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource result = null;
            var intValue = (int)value;


            result = SwitchGetUrl(intValue);


            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public ImageSource SwitchGetUrl(int intalue)
        {
            ImageSource result = null;
            switch (intalue)
            {
                case 0:
                    {
                        result = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"\Content\Attributes\zero.bmp")));
                        break;
                    }

                case 1:
                    {
                        result = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"\Content\Attributes\one.bmp")));
                        break;
                    }
                case 2:
                    {
                     
                        result = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"\Content\Attributes\two.bmp")));
                        break;
                    }
                case 3:
                    {
                        result = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"\Content\Attributes\three.bmp")));
                        break;
                    }
                case 4:
                    {
                        result = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"\Content\Attributes\four.bmp")));
                        break;
                    }
                case 5:
                    {
                        result = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"\Content\Attributes\five.bmp")));
                        break;
                    }
                case 6:
                    {
                        result = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"\Content\Attributes\six.bmp")));
                        break;
                    }
                case 7:
                    {
                        result = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"\Content\Attributes\seven.bmp")));
                        break;
                    }
                case 8:
                    {
                        result = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"\Content\Attributes\eight.bmp")));
                        break;
                    }
                case 9:
                    {
                        result = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, @"\Content\Attributes\nine.bmp")));
                        break;
                    }
            }
            return result;
        }

    }

}
