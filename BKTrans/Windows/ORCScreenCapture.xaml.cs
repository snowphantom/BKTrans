using BKTrans.Entities;
using BKTrans.Utility;
using BKTrans.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BKTrans
{
    /// <summary>
    /// Interaction logic for ScreenCapture.xaml
    /// </summary>
    public partial class OCRScreenCapture : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public RegionCapture RegionCaptureData { get; set; }


        private Point RectStartPoint;
        public Point RectPosition { get; set; }
        public Rectangle SelectedRegion { get; set; }

        private bool _IsMouseDown = false;
        public bool IsMouseDown
        {
            get { return _IsMouseDown; }
            set
            {
                _IsMouseDown = value;
                OnPropertyChanged(nameof(IsMouseDown));
            }
        }

        private string _LanguageOCR;
        public string LanguageOCR
        {
            get { return _LanguageOCR; }
            set 
            {
                _LanguageOCR = value;
                OnPropertyChanged(nameof(LanguageOCR));
            }
        }

        private BitmapImage _ScreenCaptureFull;
        public BitmapImage ScreenCaptureFull
        {
            get { return _ScreenCaptureFull; }
            set
            {
                if (_ScreenCaptureFull != null)
                    _ScreenCaptureFull.StreamSource.Dispose();
                _ScreenCaptureFull = value;
                OnPropertyChanged(nameof(ScreenCaptureFull));
            }
        }

        private bool Rescan;

        public List<string> LanguageOCRList { get { return App.LANGUAGEDATA.Select(x => x.Name).ToList(); } }

        public OCRScreenCapture()
        {
            InitializeComponent();
            Instance = this;
            this.DataContext = this;
            this.Activate();
        }

        private static OCRScreenCapture _Instance;
        public static OCRScreenCapture Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new OCRScreenCapture();
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }

        public void TryOpenWindow(bool rescan = false,bool forceNewInstance = false)
        {
            try
            {
                if (forceNewInstance)
                    Instance = new OCRScreenCapture();

                string langOcrCode = Settings.ReadSettings("General", "ScanLang");
                LanguageOCR = App.LANGUAGEDATA.FirstOrDefault(x => x.Code == langOcrCode).Name;

                Rescan = rescan;
                ScreenCaptureFull = UtilityHelper.GetScreenCapture();
                Instance.Show();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ScreenCaptureGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RectStartPoint = e.GetPosition(ScreenCaptureGrid);
            IsMouseDown = true;
        }

        private void ScreenCaptureGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                Point tempEndPoint = e.GetPosition(ScreenCaptureGrid);

                double width = Math.Abs(RectStartPoint.X - tempEndPoint.X);
                double height = Math.Abs(RectStartPoint.Y - tempEndPoint.Y);



                if (RectStartPoint.X > tempEndPoint.X && RectStartPoint.Y > tempEndPoint.Y)
                {
                    Canvas.SetLeft(selectionBox, tempEndPoint.X);
                    Canvas.SetTop(selectionBox, tempEndPoint.Y);
                }
                else if (RectStartPoint.X > tempEndPoint.X && RectStartPoint.Y < tempEndPoint.Y)
                {
                    Canvas.SetLeft(selectionBox, tempEndPoint.X);
                    Canvas.SetTop(selectionBox, RectStartPoint.Y);
                }
                else if (RectStartPoint.X < tempEndPoint.X && RectStartPoint.Y > tempEndPoint.Y)
                {
                    Canvas.SetLeft(selectionBox, RectStartPoint.X);
                    Canvas.SetTop(selectionBox, tempEndPoint.Y);
                }
                else
                {
                    Canvas.SetLeft(selectionBox, RectStartPoint.X);
                    Canvas.SetTop(selectionBox, RectStartPoint.Y);
                }

                selectionBox.Width = width;
                selectionBox.Height = height;

            }
        }

        private void ScreenCaptureGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = false;

            SelectedRegion = new Rectangle()
            {
                Width = selectionBox.Width,
                Height = selectionBox.Height
            };
            RectPosition = new Point()
            {
                X = Canvas.GetLeft(selectionBox),
                Y = Canvas.GetTop(selectionBox)
            };

            RegionCaptureData = new RegionCapture()
            {
                ScreenCapture = ScreenCaptureFull,
                Position = RectPosition,
                SelectedRegion = this.SelectedRegion,
                LanguageOCR = this.LanguageOCR,
            };

            this.Hide();

            ResetSelectionBox();

            ShowResult(Rescan);
        }

        private void ShowResult(bool rescan)
        {
            try
            {
                byte[] capturedImageData = UtilityHelper.GetCroppedImageData(RegionCaptureData);

                if (capturedImageData != null && !rescan)
                {
                    ResultScreen.Instance = new ResultScreen(capturedImageData, RegionCaptureData.LanguageOCR);
                    ResultScreen.Instance.Show();
                }
                else if (capturedImageData != null && rescan)
                {
                    ResultScreen.Instance.TryOpenWindow(capturedImageData, RegionCaptureData.LanguageOCR, true);
                }

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ResetSelectionBox()
        {
            Canvas.SetLeft(selectionBox, 0);
            Canvas.SetTop(selectionBox, 0);
            selectionBox.Width = 0;
            selectionBox.Height = 0;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #region Command Region

        public ICommand CloseWindow
        {
            get
            {
                return new RelayCommand(param =>
                {
                    this.Hide();
                });
            }
        }

        #endregion

        private void SelectedLanguageOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = (sender as System.Windows.Controls.ComboBox).SelectedValue.ToString();
            Settings.SaveSettings("General", "ScanLang", LanguageDictionary.GetLanguageOCRCode(selectedValue));
        }
    }
}
