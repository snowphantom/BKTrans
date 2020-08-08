using BKTrans.Core;
using Newtonsoft.Json;

namespace BKTrans.Entities
{
    public class LanguageModel : BaseViewModel
    {
        public LanguageModel() { }

        private bool _IsHideDownload = false;
        public bool IsHideDownload
        {
            get { return _IsHideDownload; }
            set { _IsHideDownload = value; OnPropertyChanged(nameof(IsHideDownload)); }
        }

        private bool _IsExist = false;
        [JsonProperty("existed")]
        public bool IsExist
        {
            get { return _IsExist; }
            set { _IsExist = value; IsHideDownload = value ? true : false; OnPropertyChanged(nameof(IsExist)); }
        }

        private bool _IsDownloading = false;
        public bool IsDownloading
        {
            get { return _IsDownloading; }
            set { _IsDownloading = value; if (value) IsHideDownload = true; OnPropertyChanged(nameof(IsDownloading)); }
        }

        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("langurl")]
        public string LangURL { get; set; }
    }
}
