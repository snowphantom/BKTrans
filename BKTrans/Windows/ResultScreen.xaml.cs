using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
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

using MahApps.Metro.Controls;
using Newtonsoft.Json;
using BKTrans.Entities;
using BKTrans.Utility;
using BKTrans.ViewModel;

namespace BKTrans
{
    /// <summary>
    /// Interaction logic for ResultScreen.xaml
    /// </summary>
    public partial class ResultScreen : MetroWindow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ResultScreen()
        {
            InitializeComponent();
            _Instance = this;
            this.DataContext = new ResultScreenViewModel();
        }

        public ResultScreen(byte[] imageData, string languageOCR = "Auto")
        {
            InitializeComponent();
            _Instance = this;
            this.DataContext = new ResultScreenViewModel(imageData, languageOCR);
        }

        private static ResultScreen _Instance;
        public static ResultScreen Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ResultScreen();
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }

        public void TryOpenWindow(byte[] imageData, string languageOCR = "Auto", bool rescan = false)
        {
            try
            {
                if (Instance == null || !rescan)
                    Instance = new ResultScreen();

                this.DataContext = new ResultScreenViewModel(imageData, languageOCR);
                Instance.Show();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void TryOpenWindow(bool forceNew = false)
        {
            if (Instance == null || forceNew)
                Instance = new ResultScreen();

            Instance.Show();
        }

        private void OriginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Background = Brushes.White;
            textBox.BorderThickness = new Thickness(1);
        }

        private void OriginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Background = Brushes.WhiteSmoke;
            textBox.BorderThickness = new Thickness(0);
        }

        private void TranslatedTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            RichTextBox textBox = sender as RichTextBox;
            textBox.Background = Brushes.White;
            textBox.BorderThickness = new Thickness(1);
        }

        private void TranslatedTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            RichTextBox textBox = sender as RichTextBox;
            textBox.Background = Brushes.WhiteSmoke;
            textBox.BorderThickness = new Thickness(0);
        }
    }
}
