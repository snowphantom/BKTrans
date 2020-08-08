using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BKTrans.Entities
{
    public class RegionCapture
    {
        public RegionCapture() { }

        public BitmapImage ScreenCapture { get; set; }
        public Point Position { get; set; }
        public Rectangle SelectedRegion { get; set; }
        public string LanguageOCR { get; set; }
    }
}
