using BKTrans.Core;
using BKTrans.Entities;
using BKTrans.Utility;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BKTrans.ViewModel
{
    public class AddLangViewModel : BaseViewModel
    {
        private bool _IsAddingLocalOCRData = false;
        public bool IsAddingLocalOCRData
        {
            get { return _IsAddingLocalOCRData; }
            set
            {
                _IsAddingLocalOCRData = value;
                OnPropertyChanged(nameof(IsAddingLocalOCRData));
            }
        }

        public ObservableCollection<LanguageModel> ItemsList
        {
            get
            {
                return App.LANGUAGEDATA;
            }
            set
            {
                App.LANGUAGEDATA = value;
                OnPropertyChanged(nameof(ItemsList));
            }
        }

        public AddLangViewModel()
        {
            
        }

        private async Task DownloadLanguageAsync(string code)
        {
            LanguageModel item = ItemsList.FirstOrDefault(x => x.Code == code);

            try
            {
                item.IsDownloading = true;

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                using (WebClient webClient = new WebClient())
                {

                    webClient.DownloadDataCompleted += delegate
                    {
                        WebClient_DownloadDataCompleted(item);
                    };
                    byte[] data = await webClient.DownloadDataTaskAsync(new Uri(item.LangURL));
                    using (Stream memory = new FileStream(Path.Combine(App.TESSDATA_DICTPATH, item.Code + ".traineddata"), FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        memory.Write(data, 0, data.Length);
                    }
                    item.IsExist = true;
                }  
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                item.IsDownloading = false;
                item.IsExist = false;
            }
        }

        private void WebClient_DownloadDataCompleted(LanguageModel model)
        {
            model.IsDownloading = false;
        }

        
        public ICommand DownloadClick
        {
            get
            {
                return new RelayCommand(param =>
                {
                    string code = param as string;
                    if (code != null && code.Length > 1)
                    {
                        DownloadLanguageAsync(code);
                    }
                });
            }
        }

        public ICommand AddLocalOCRDataClick
        {
            get
            {
                return new RelayCommand(param =>
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
                                IsAddingLocalOCRData = true;
                                foreach (string file in openFileDialog.FileNames)
                                {
                                    string dataFilePath = Path.Combine(App.TESSDATA_DICTPATH, Path.GetFileName(file));
                                    Helper.FastCopy(file, dataFilePath);
                                }
                                IsAddingLocalOCRData = false;
                                MessageBox.Show("Adding data successfully.", Application.Current.TryFindResource("ApplicationTitle").ToString());
                                App.CheckDownloaded();
                            }
                            catch (Exception ex)
                            {
                                IsAddingLocalOCRData = false;
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        });
                    }
                });
            }
        }

        public ICommand BackToGeneral
        {
            get
            {
                return new RelayCommand(param =>
                {
                    MainWindow.Instance.ChangeSettingsView(1);
                });
            }
        }
    }
}
