using Newtonsoft.Json;
using BKTrans.Core;
using BKTrans.Entities;
using BKTrans.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BKTrans.ViewModel
{
    public class ResultScreenViewModel : BaseViewModel
    {
        private BitmapImage _CapturedImage;
        public BitmapImage CapturedImage
        {
            get
            {
                return _CapturedImage;
            }
            set
            {
                this._CapturedImage = value;
                OnPropertyChanged(nameof(CapturedImage));
            }
        }

        private string _OriginText;
        public string OriginText
        {
            get { return _OriginText; }
            set
            {
                this._OriginText = value;
                OnPropertyChanged(nameof(OriginText));
            }
        }

        private string _TranslatedText;
        public string TranslatedText
        {
            get { return _TranslatedText; }
            set
            {
                _TranslatedText = value;
                OnPropertyChanged(nameof(TranslatedText));
            }
        }

        private string _TargetLang;
        public string TargetLang
        {
            get { return _TargetLang; }
            set
            {
                _TargetLang = value;
                OnPropertyChanged(nameof(TargetLang));
            }
        }

        private string _ORCLang = "Auto";
        public string ORCLang
        {
            get { return _ORCLang; }
            set
            {
                _ORCLang = value;
                OnPropertyChanged(nameof(ORCLang));
            }
        }

        private string SourceLang = "en";

        private string[] _POS;
        public string[] POS
        {
            get { return _POS; }
            set { _POS = value; OnPropertyChanged(nameof(POS)); }
        }

        private string[] _POS_TERMS;
        public string[] POS_TERMS
        {
            get { return _POS_TERMS; }
            set { _POS_TERMS = value; OnPropertyChanged(nameof(POS_TERMS)); }
        }

        private bool _IsErrorOccurred = false;
        public bool IsErrorOccurred
        {
            get { return _IsErrorOccurred; }
            set
            {
                _IsErrorOccurred = value;
                OnPropertyChanged(nameof(IsErrorOccurred));
            }
        }

        private string _ErrorMessage = "";
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                _ErrorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private string translateLanguageCode;

        Timer Timer = new Timer()
        {
            Interval = 1000,
        };

        public ResultScreenViewModel()
        {
            translateLanguageCode = Settings.ReadSettings("General", "TransLang");
            TargetLang = LanguageDictionary.GetTranslateLanguageByValue(translateLanguageCode);
            Timer.Elapsed += Timer_Elapsed;

        }


        public ResultScreenViewModel(byte[] imageMemory, string languageOCR = "Auto")
        {
            translateLanguageCode = Settings.ReadSettings("General", "TransLang");
            TargetLang = LanguageDictionary.GetTranslateLanguageByValue(translateLanguageCode);

            ORCLang = languageOCR != null ? languageOCR : "auto";

            ParseImageAndTranslate(imageMemory);

            Timer.Elapsed += Timer_Elapsed;
        }



        private async void ParseImageAndTranslate(byte[] imageData)
        {
            await ParseImage(imageData);
            TranslateText();
        }




        private async Task ParseImage(byte[] imageData)
        {
            try
            {
                string parseText = await UtilityHelper.GetParseImageResult(imageData, ORCLang);

                OriginText = parseText;
            }
            catch (Exception e)
            {
                IsErrorOccurred = true;
                ErrorMessage = e.Message;
            }
        }

        private async void TranslateText()
        {
            if (OriginText != null && OriginText.Length > 0)
            {
                string translatedText = "";
                string originText = OriginText;

                TranslateResult translateResult = await UtilityHelper.GetTranslateResult(originText, translateLanguageCode);
                //TranslatedText = await TranslateText(OriginText);
                if (translateResult != null)
                {
                    foreach (var sentence in translateResult.Sentences)
                    {
                        translatedText = translatedText + sentence.Translated.Replace("\n", "").Replace("\r", "") + "\n";
                    }

                    if (translateResult.Dict != null)
                    {
                        List<string> POS_LIST = new List<string>();
                        List<string> POS_TERMS_LIST = new List<string>();
                        foreach (var item in translateResult.Dict)
                        {
                            POS_LIST.Add(item.Pronoun + ":");
                            POS_TERMS_LIST.Add(string.Join(",", item.Terms));
                        }
                        POS = POS_LIST.ToArray();
                        POS_TERMS = POS_TERMS_LIST.ToArray();
                    }
                    SourceLang = translateResult.SourceLang != null ? translateResult.SourceLang : "en";
                    TranslatedText = translatedText;
                }
                else
                {
                    TranslatedText = "*Some errors have occurred. Please try again*";
                }
            }
        }

        private void ResetTranslateBox()
        {
            TranslatedText = null;
            POS = null;
            POS_TERMS = null;
        }


        #region Command area

        public ICommand CloseWindowCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    ResultScreen.Instance.Hide();
                });
            }
        }

        public ICommand PlayOriginText
        {
            get
            {
                return new RelayCommand(param =>
                {
                    try
                    {
                        UtilityHelper.PlayMp3FromUrl(@"http://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&total=1&idx=0&tl=" + SourceLang + "&q=" + Uri.EscapeDataString(OriginText));
                    }
                    catch
                    {
                    }
                });
            }
        }

        public ICommand PlayTranslatedText
        {
            get
            {
                return new RelayCommand(param =>
                {
                    try
                    {
                        UtilityHelper.PlayMp3FromUrl(@"http://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&total=1&idx=0&tl=" + translateLanguageCode + "&q=" + Uri.EscapeDataString(TranslatedText));
                    }
                    catch
                    {
                    }
                });
            }
        }

        public ICommand TranslateCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    ResetTranslateBox();
                    TranslateText();
                });
            }
        }

        public ICommand RescanTextCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    MainWindow.Instance.StartScanScreen();
                });
            }
        }

        public ICommand Translate
        {
            get
            {
                return new RelayCommand(param =>
                {
                    if (OriginText != null)
                    {
                        Timer.Stop();
                        Timer.Start();
                    }
                });
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer.Stop();
            ResetTranslateBox();
            if (TranslatedText == null || TranslatedText.Length < 1)
                TranslateText();
        }

        #endregion
    }
}
