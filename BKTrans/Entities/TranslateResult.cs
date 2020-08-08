using Newtonsoft.Json;

namespace BKTrans.Entities
{
    public class TranslateResult
    {
        [JsonProperty("sentences")]
        public Sentence[] Sentences { get; set; }
        [JsonProperty("dict")]
        public Dict[] Dict { get; set; }
        [JsonProperty("src")]
        public string SourceLang { get; set; }
    }

    public class Sentence
    {
        [JsonProperty("trans")]
        public string Translated { get; set; }
        [JsonProperty("orig")]
        public string Origin { get; set; }
    }

    public class Dict
    {
        [JsonProperty("pos")]
        public string Pronoun { get; set; }
        [JsonProperty("terms")]
        public string[] Terms { get; set; }
        [JsonProperty("entry")]
        public dynamic Entry { get; set; }
        [JsonProperty("base_form")]
        public string BaseForm { get; set; }
    }
}
