using IniParser.Model;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using BKTrans.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using NHotkey.Wpf;
using NHotkey;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls.Dialogs;
using System.Threading;
using BKTrans.ViewModel;

namespace BKTrans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ContentControl GeneralControl = new GeneralControl() { };
        private ContentControl AddLangControl = new AddLanguageControl() {};

        private ContentControl _SettingsContentControl;
        public ContentControl SettingsContentControl
        {
            get { return _SettingsContentControl; }
            set
            {
                _SettingsContentControl = value;
                OnPropertyChanged(nameof(SettingsContentControl));
            }
        }

        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private IniData settings;

        private bool _IsScanTextEnable = true;
        public bool IsScanTextEnable
        {
            get { return _IsScanTextEnable; }
            set
            {
                _IsScanTextEnable = value;
                OnPropertyChanged(nameof(IsScanTextEnable));
            }
        }

        private bool _IsOpenTransEnable = true;
        public bool IsOpenTransEnable
        {
            get { return _IsOpenTransEnable; }
            set
            {
                _IsOpenTransEnable = value;
                OnPropertyChanged(nameof(IsOpenTransEnable));
            }
        }

        private bool _IsOpenMainEnable = true;
        public bool IsOpenMainEnable
        {
            get { return _IsOpenMainEnable; }
            set
            {
                _IsOpenMainEnable = value;
                OnPropertyChanged(nameof(IsOpenMainEnable));
            }
        }

        private HotKey _ScanTextHotKey;
        public HotKey ScanTextHotKey
        {
            get { return _ScanTextHotKey; }
            set
            {
                _ScanTextHotKey = value;
                OnPropertyChanged(nameof(ScanTextHotKey));
            }
        }

        private HotKey _OpenTranslateHotKey;
        public HotKey OpenTranslateHotKey
        {
            get { return _OpenTranslateHotKey; }
            set
            {
                _OpenTranslateHotKey = value;
                OnPropertyChanged(nameof(OpenTranslateHotKey));
            }
        }

        private HotKey _OpenMainWindowHotKey;
        public HotKey OpenMainWindowHotKey
        {
            get { return _OpenMainWindowHotKey; }
            set
            {
                _OpenMainWindowHotKey = value;
                OnPropertyChanged(nameof(OpenMainWindowHotKey));
            }
        }

        private bool _IsAddingOCRData = false;
        public bool IsAddingOCRData
        {
            get { return _IsAddingOCRData; }
            set
            {
                _IsAddingOCRData = value;
                OnPropertyChanged(nameof(IsAddingOCRData));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            ChangeSettingsView(1);
            settings = Settings.ReadSettings();

            ScanTextHotKey = Settings.GetHotKeyFromSetting(settings["ScanText"]);
            OpenTranslateHotKey = Settings.GetHotKeyFromSetting(settings["OpenTrans"]);
            OpenMainWindowHotKey = Settings.GetHotKeyFromSetting(settings["OpenMain"]);

            //Settings.SaveHotKey("OpenMain", ScanTextHotKey);

            TranslateLanguageSelected = LanguageDictionary.MappingLanguageTranslate.FirstOrDefault(x => x.Value == settings["General"]["TransLang"]).Key;
            ScanLanguageSelected = LanguageDictionary.MappingLanguageOCR.FirstOrDefault(x => x.Value == settings["General"]["ScanLang"]).Key;
            IsStartupWindow = bool.Parse(settings["General"]["StartupWindow"]);
            //IsStartupWindow = ;
            Instance = this;
            this.DataContext = this;
            System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();

            contextMenu.MenuItems.Add("Scan text.", OnScanText);
            contextMenu.MenuItems.Add("Translate pop-up.", OnOpenTrans);
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add("Exit.", OnExit);


            NotifyIcon = new System.Windows.Forms.NotifyIcon()
            {
                BalloonTipTitle = Application.Current.TryFindResource("ApplicationTitle").ToString(),
                BalloonTipText = "Find me in the toolbar.",
                Text = "Click to open main window.",
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().Location),
                Visible = true,
                ContextMenu = contextMenu
            };
            NotifyIcon.BalloonTipClicked += (s, ev) => { this.Show(); };
            NotifyIcon.Click += (s, ev) => { this.Show(); };
        }

        private void OnOpenTrans(object sender, EventArgs e)
        {
            ResultScreen.Instance.TryOpenWindow(true);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnScanText(object sender, EventArgs e)
        {
            StartScanScreen();
        }

        public void ChangeSettingsView(int screen)
        {
            switch (screen)
            {
                case 1:
                    SettingsContentControl = GeneralControl;
                    break;
                case 2:
                    SettingsContentControl = AddLangControl;
                    break;
            }
        }

       public List<string> TranslateLanguageList { get { return LanguageDictionary.MappingLanguageTranslate.Keys.ToList(); } }
        public List<string> ScanLanguageList { get { return App.LANGUAGEDATA.Select(x=>x.Name).ToList(); } }

        private string _TranslateLanguageSelected;
        public string TranslateLanguageSelected
        {
            get { return _TranslateLanguageSelected; }
            set
            {
                _TranslateLanguageSelected = value;
                OnPropertyChanged(nameof(TranslateLanguageSelected));
            }
        }

        private string _ScanLanguageSelected;
        public string ScanLanguageSelected
        {
            get { return _ScanLanguageSelected; }
            set
            {
                _ScanLanguageSelected = value;
                OnPropertyChanged(nameof(ScanLanguageSelected));
            }
        }

        private bool _isStartupWindow = true;
        public bool IsStartupWindow
        {
            get { return _isStartupWindow; }
            set
            {
                _isStartupWindow = value;
                OnPropertyChanged(nameof(IsStartupWindow));
            }
        }



        private static MainWindow _Instance;

        public static MainWindow Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new MainWindow();
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }


        public void RegisterHotKeys()
        {
            try
            {
                if (IsScanTextEnable)
                    HotkeyManager.Current.AddOrReplace("ghkScanText", ScanTextHotKey.Key, ScanTextHotKey.ModifierKeys, new EventHandler<NHotkey.HotkeyEventArgs>(ScanText_HotKey));
                else
                    HotkeyManager.Current.AddOrReplace("ghkScanText", 0, 0, new EventHandler<NHotkey.HotkeyEventArgs>(ScanText_HotKey));

                if (IsOpenTransEnable)
                    HotkeyManager.Current.AddOrReplace("ghkOpenTrans", OpenTranslateHotKey.Key, OpenTranslateHotKey.ModifierKeys, new EventHandler<NHotkey.HotkeyEventArgs>(OpenTrans_HotKey));
                else
                    HotkeyManager.Current.AddOrReplace("ghkOpenTrans", 0, 0, new EventHandler<NHotkey.HotkeyEventArgs>(OpenTrans_HotKey));

                if (IsOpenMainEnable)
                    HotkeyManager.Current.AddOrReplace("ghkOpenMain", OpenMainWindowHotKey.Key, OpenMainWindowHotKey.ModifierKeys, new EventHandler<NHotkey.HotkeyEventArgs>(OpenMain_HotKey));
                else
                    HotkeyManager.Current.AddOrReplace("ghkOpenMain", 0, 0, new EventHandler<NHotkey.HotkeyEventArgs>(OpenMain_HotKey));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void OpenMain_HotKey(object sender, HotkeyEventArgs e)
        {
            this.Show();
        }

        public void ScanText_HotKey(object sender, NHotkey.HotkeyEventArgs e)
        {
            StartScanScreen();
        }

        private void OpenTrans_HotKey(object sender, HotkeyEventArgs e)
        {
            ResultScreen.Instance.TryOpenWindow(true);
        }



        private async void ScanText_Click(object sender, RoutedEventArgs e)
        {
            await StartScanScreen();
            this.Show();
        }

        public async void GetScreenCapturae()
        {
            this.Hide();
            OCRScreenCapture.Instance.Hide();
            ResultScreen.Instance.Close();
            await Task.Delay(150);
            OCRScreenCapture.Instance.TryOpenWindow();
        }

        public async Task StartScanScreen()
        {
            this.Hide();
            OCRScreenCapture.Instance.Hide();
            ResultScreen.Instance.Hide();
            await Task.Delay(150);
            OCRScreenCapture.Instance.TryOpenWindow(true);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            UtilityHelper.RegisterInStartup(IsStartupWindow);
            Settings.SaveSettings("General", "StartupWindow", IsStartupWindow.ToString());
            SaveHotKey();
            RegisterHotKeys();
            this.Hide();
        }

        private void SaveHotKey()
        {
            Settings.SaveHotKey("ScanText", ScanTextHotKey);
            Settings.SaveHotKey("OpenTrans", OpenTranslateHotKey);
            Settings.SaveHotKey("OpenMain", OpenMainWindowHotKey);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterHotKeys();
        }

        private void ShowTrayIcon()
        {
            this.Hide();
            NotifyIcon.Visible = true;
            NotifyIcon.ShowBalloonTip(1);
        }

        private void TitleBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == Mouse.LeftButton)
            {
                DragMove();
            }
        }

        private void SystemTray_Click(object sender, RoutedEventArgs e)
        {
            ShowTrayIcon();
        }

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private async void ScanFileButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(Support Files)|*.doc;*.docx;*.odf;*.pdf;*.ppt;*.pptx;*.ps;*.rtf;*.txt;*.xls;*.xlsx";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                ProgressDialogController progressDialog = await this.ShowProgressAsync("Processing...", "Always smile and happy :)");
                progressDialog.SetIndeterminate();
                try
                {
                    if (progressDialog.IsOpen)
                    {
                        using (System.Net.WebClient webClient = new System.Net.WebClient())
                        {
                            byte[] response = await webClient.UploadFileTaskAsync(new Uri("https://translate.googleusercontent.com/translate_f?tl=" + LanguageDictionary.GetTranslateLanguageCode(TranslateLanguageSelected)), filePath);
                            string dataFilePath = Path.Combine(App.SCANFILE_PATH, fileName + "_" + LanguageDictionary.GetTranslateLanguageCode(TranslateLanguageSelected) + ".html");
                            File.WriteAllBytes(dataFilePath, response);
                            await Task.Delay(4000);
                            Process.Start(dataFilePath);
                            await progressDialog.CloseAsync();
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {
                    await progressDialog.CloseAsync();
                    MessageBox.Show("Scan file Error: " + ex.Message, "Error", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    await progressDialog.CloseAsync();
                    MessageBox.Show("Scan file Error: " + ex.Message, "Error", MessageBoxButton.OK);
                }
            }
        }



        private void OpenTranslatePopup_Click(object sender, RoutedEventArgs e)
        {
            ResultScreen.Instance.TryOpenWindow(true);
        }

        private void TranslateLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = (sender as System.Windows.Controls.ComboBox).SelectedValue.ToString();
            Settings.SaveSettings("General", "TransLang", LanguageDictionary.GetTranslateLanguageCode(selectedValue));
        }

        private void ScanLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = (sender as System.Windows.Controls.ComboBox).SelectedValue.ToString();
            Settings.SaveSettings("General", "ScanLang", LanguageDictionary.GetLanguageOCRCode(selectedValue));
        }



        private void AddOCRData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Add more OCR data.",
                DefaultExt = "traineddata",
                Multiselect = true,
                Filter = "Trained File (*.traineddata)|*.traineddata|" +
                            "All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {

                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        IsAddingOCRData = true;
                        foreach (string file in openFileDialog.FileNames)
                        {
                            string dataFilePath = Path.Combine(App.TESSDATA_DICTPATH, Path.GetFileName(file));
                            Helper.FastCopy(file, dataFilePath);
                        }
                        IsAddingOCRData = false;
                        MessageBox.Show("Adding data successfully.", Application.Current.TryFindResource("ApplicationTitle").ToString());

                    }
                    catch (Exception ex)
                    {
                        IsAddingOCRData = false;
                        MessageBox.Show("Error: " + ex.Message);
                    }
                });
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }


        public ICommand ScanLangSelectionChanged
        {
            get
            {
                return new RelayCommand(param =>
                {
                    string selectedValue = param as string;
                    Settings.SaveSettings("General", "ScanLang", LanguageDictionary.GetLanguageOCRCode(selectedValue));
                });
            }
        }

        public ICommand TranslateLangSelectionChanged
        {
            get
            {
                return new RelayCommand(param =>
                {
                    string selectedValue = param as string;
                    Settings.SaveSettings("General", "TransLang", LanguageDictionary.GetTranslateLanguageCode(selectedValue));
                });
            }
        }

        public ICommand AddOCRData
        {
            get
            {
                return new RelayCommand(param =>
                {
                    ChangeSettingsView(2);
                });
            }
        }
    }
}
