using BKTrans.Core;
using BKTrans.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BKTrans.ViewModel
{
    public class GeneralViewModel : BaseViewModel
    {


        public GeneralViewModel() { }

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
                    MainWindow.Instance.ChangeSettingsView(2);
                });
            }
        }
    }
}
