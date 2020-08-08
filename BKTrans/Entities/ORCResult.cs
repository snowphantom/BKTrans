using Newtonsoft.Json;

namespace BKTrans.Entities
{
    public class ORCResult
    {
        [JsonProperty("ParsedResults")]
        public ParseResult[] ParsedResults { get; set; }
        [JsonProperty("OCRExitCode")]
        public object OCRExitCode { get; set; }
        [JsonProperty("IsErroredOnProcessing")]
        public bool IsErroredOnProcessing { get; set; }
        [JsonProperty("ProcessingTimeInMilliseconds")]
        public object ProcessingTimeInMilliseconds { get; set; }

    }

    public class ParseResult
    {
        [JsonProperty("FileParseExitCode")]
        public object FileParseExitCode { get; set; }
        [JsonProperty("ParsedText")]
        public string ParsedText { get; set; }
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("ErrorDetails")]
        public string ErrorDetails { get; set; }
    }
}
