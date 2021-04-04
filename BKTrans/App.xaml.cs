using IniParser.Model;
using IniParser.Parser;
using IniParser;
using BKTrans.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Threading;
using BKTrans.Entities;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BKTrans
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);
        private static readonly Int32 SW_SHOW = 10;

        public static readonly string SCANFILE_PATH = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), ".bktrans\\appdata");
        public static readonly string TESSDATA_DICTPATH = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), ".bktrans\\tessdata");
        public static readonly string SETTINGS_PATH = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), ".bktrans\\appdata", "settings.ini");
        public static readonly string BASEDIRECTORY = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string AUTO_TRAINNED_PATH = Path.Combine(BASEDIRECTORY, "eng.traineddata");

        public static ObservableCollection<LanguageModel> LANGUAGEDATA = new ObservableCollection<LanguageModel>();

        private static Mutex _mutex = null;

        public void SaveData()
        {
            string fileName = "data.json";
            string fileNameEncrypted = "data.dat";
            string fileDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileNameEncrypted);

            string dataString = JsonConvert.SerializeObject(LANGUAGEDATA);


            byte[] dataEncrypted = Helper.EncryptStringToByte(dataString, Helper.ENCRYPT_KEY);
            File.WriteAllBytes(fileDataPath, dataEncrypted);

            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName), dataString);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            CheckRunningInstance();
            CheckDataSource();
            ReadSettings();
            ReadData();
            CheckDownloaded();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        private void ReadData()
        {
            string fileName = "data.json";
            string fileNameEncrypted = "data.dat";

            string fileDataPath = Path.Combine(BASEDIRECTORY, fileNameEncrypted);
            if (!File.Exists(fileDataPath))
            {
                string data = File.ReadAllText(Path.Combine(BASEDIRECTORY, fileName));
                byte[] encrypted = Helper.EncryptStringToByte(data, Helper.ENCRYPT_KEY);
                File.WriteAllBytes(fileNameEncrypted, encrypted);
            }
            byte[] dataEncrypt = File.ReadAllBytes(fileDataPath);
            LANGUAGEDATA = JsonConvert.DeserializeObject<ObservableCollection<LanguageModel>>(Helper.DecryptByteToString(dataEncrypt, Helper.ENCRYPT_KEY));
        }

        private void ReadSettings()
        {
            if (!File.Exists(SETTINGS_PATH))
            {
                FileIniDataParser parser = new FileIniDataParser();
                IniData data = new IniData();
                data["General"]["TransLang"] = "vi";
                data["General"]["ScanLang"] = "eng";
                data["General"]["StartupWindow"] = "False";
                data["ScanText"]["Key"] = "63";
                data["ScanText"]["ModifierKeys"] = "1,2";
                data["OpenTrans"]["Key"] = "68";
                data["OpenTrans"]["ModifierKeys"] = "1,2";
                data["OpenMain"]["Key"] = "64";
                data["OpenMain"]["ModifierKeys"] = "1,2";

                parser.WriteFile(SETTINGS_PATH, data);
            }
        }

        private static void CheckDataSource()
        {
            if (!Directory.Exists(SCANFILE_PATH))
            {
                Directory.CreateDirectory(SCANFILE_PATH);
            }

            if (!Directory.Exists(TESSDATA_DICTPATH))
            {
                Directory.CreateDirectory(TESSDATA_DICTPATH);
            }

            if (File.Exists(AUTO_TRAINNED_PATH))
            {
                File.Copy(AUTO_TRAINNED_PATH, Path.Combine(TESSDATA_DICTPATH, "eng.traineddata"));
            }
        }

        public static void CheckDownloaded()
        {
            DirectoryInfo tessFolder = new DirectoryInfo(App.TESSDATA_DICTPATH);
            List<string> listCode = tessFolder.GetFiles().Select(x => Path.GetFileNameWithoutExtension(x.FullName)).ToList();
            var result = LANGUAGEDATA.Select(x =>
            {
                if (listCode.Contains(x.Code))
                {
                    x.IsExist = true;
                }

                return x;
            });
            LANGUAGEDATA = new ObservableCollection<LanguageModel>(result);
        }

        public static void CheckRunningInstance()
        {
            if (SingleInstance.AlreadyRunning())
                App.Current.Shutdown();
        }
    }
}
