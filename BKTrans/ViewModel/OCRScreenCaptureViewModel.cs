using BKTrans.Core;
using BKTrans.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BKTrans.ViewModel
{
    public class OCRScreenCaptureViewModel : BaseViewModel
    {
        private Point RectStartPoint;
        private bool IsMouseDown = false;
        public Rectangle SelectedRegionRect = new Rectangle();
        public string LanguageOCR = LanguageDictionary.MappingLanguageOCR.Keys.LastOrDefault();
        public BitmapImage ScreenCaptureFull { get { return UtilityHelper.GetScreenCapture(); } set { } }
        public List<string> LanguageOCRList { get { return LanguageDictionary.MappingLanguageOCR.Keys.ToList(); } }
    }
}
