using BKTrans.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKTrans.Utility
{
    public static class LanguageDictionary
    {
        public static Dictionary<string, string> MappingLanguageOCR = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {

            #region Dictionary of OCR api
            {"Auto", "eng" },
            {"Afrikaans","afr"},
            {"Amharic","amh"},
            {"Arabic","ara"},
            {"Assamese","asm"},
            {"Azerbaijani","aze"},
            {"Azerbaijani - Cyrillic","aze_cyrl"},
            {"Belarusian","bel"},
            {"Bengali","ben"},
            {"Tibetan","bod"},
            {"Bosnian","bos"},
            {"Bulgarian","bul"},
            {"Catalan; Valencian","cat"},
            {"Cebuano","ceb"},
            {"Czech","ces"},
            {"Chinese - Simplified","chi_sim"},
            {"Chinese - Traditional","chi_tra"},
            {"Cherokee","chr"},
            {"Welsh","cym"},
            {"Danish","dan"},
            {"German","deu"},
            {"Dzongkha","dzo"},
            {"Greek, Modern (1453-)","ell"},
            {"English","eng"},
            {"English, Middle (1100-1500)","enm"},
            {"Esperanto","epo"},
            {"Estonian","est"},
            {"Basque","eus"},
            {"Persian","fas"},
            {"Finnish","fin"},
            {"French","fra"},
            {"German Fraktur","frk"},
            {"French, Middle (ca. 1400-1600)","frm"},
            {"Irish","gle"},
            {"Galician","glg"},
            {"Greek, Ancient (-1453)","grc"},
            {"Gujarati","guj"},
            {"Haitian; Haitian Creole","hat"},
            {"Hebrew","heb"},
            {"Hindi","hin"},
            {"Croatian","hrv"},
            {"Hungarian","hun"},
            {"Inuktitut","iku"},
            {"Indonesian","ind"},
            {"Icelandic","isl"},
            {"Italian","ita"},
            {"Italian - Old","ita_old"},
            {"Javanese","jav"},
            {"Japanese","jpn"},
            {"Kannada","kan"},
            {"Georgian","kat"},
            {"Georgian - Old","kat_old"},
            {"Kazakh","kaz"},
            {"Central Khmer","khm"},
            {"Kirghiz; Kyrgyz","kir"},
            {"Korean","kor"},
            {"Kurdish","kur"},
            {"Lao","lao"},
            {"Latin","lat"},
            {"Latvian","lav"},
            {"Lithuanian","lit"},
            {"Malayalam","mal"},
            {"Marathi","mar"},
            {"Macedonian","mkd"},
            {"Maltese","mlt"},
            {"Malay","msa"},
            {"Burmese","mya"},
            {"Nepali","nep"},
            {"Dutch; Flemish","nld"},
            {"Norwegian","nor"},
            {"Oriya","ori"},
            {"Panjabi; Punjabi","pan"},
            {"Polish","pol"},
            {"Portuguese","por"},
            {"Pushto; Pashto","pus"},
            {"Romanian; Moldavian; Moldovan","ron"},
            {"Russian","rus"},
            {"Sanskrit","san"},
            {"Sinhala; Sinhalese","sin"},
            {"Slovak","slk"},
            {"Slovenian","slv"},
            {"Spanish; Castilian","spa"},
            {"Spanish; Castilian - Old","spa_old"},
            {"Albanian","sqi"},
            {"Serbian","srp"},
            {"Serbian - Latin","srp_latn"},
            {"Swahili","swa"},
            {"Swedish","swe"},
            {"Syriac","syr"},
            {"Tamil","tam"},
            {"Telugu","tel"},
            {"Tajik","tgk"},
            {"Tagalog","tgl"},
            {"Thai","tha"},
            {"Tigrinya","tir"},
            {"Turkish","tur"},
            {"Uighur; Uyghur","uig"},
            {"Ukrainian","ukr"},
            {"Urdu","urd"},
            {"Uzbek","uzb"},
            {"Uzbek - Cyrillic","uzb_cyrl"},
            {"Vietnamese","vie"},
            {"Yiddish","yid"},
            #endregion

        };

        public static Dictionary<string, string> MappingLanguageTranslate = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            #region Dictionary of Language code
            {"Afrikaans","af"},
            {"Irish","ga"},
            {"Albanian","sq"},
            {"Italian","it"},
            {"Arabic","ar"},
            {"Japanese","ja"},
            {"Azerbaijani","az"},
            {"Kannada","kn"},
            {"Basque","eu"},
            {"Korean","ko"},
            {"Bengali","bn"},
            {"Latin","la"},
            {"Belarusian","be"},
            {"Latvian","lv"},
            {"Bulgarian","bg"},
            {"Lithuanian","lt"},
            {"Catalan","ca"},
            {"Macedonian","mk"},
            {"Chinese(Simplified)", "zh-CN"},
            {"Malay","ms"},
            {"Chinese(Traditional)","zh-TW"},
            {"Maltese","mt"},
            {"Croatian","hr"},
            {"Norwegian","no"},
            {"Czech","cs"},
            {"Persian","fa"},
            {"Danish","da"},
            {"Polish","pl"},
            {"Dutch","nl"},
            {"Portuguese","pt"},
            {"English","en"},
            {"Romanian","ro"},
            {"Esperanto","eo"},
            {"Russian","ru"},
            {"Estonian","et"},
            {"Serbian","sr"},
            {"Filipino","tl"},
            {"Slovak","sk"},
            {"Finnish","fi"},
            {"Slovenian","sl"},
            {"French","fr"},
            {"Spanish","es"},
            {"Galician","gl"},
            {"Swahili","sw"},
            {"Georgian","ka"},
            {"Swedish","sv"},
            {"German","de"},
            {"Tamil","ta"},
            {"Greek","el"},
            {"Telugu","te"},
            {"Gujarati","gu"},
            {"Thai","th"},
            {"Haitian Creole","ht"},
            {"Turkish","tr"},
            {"Hebrew","iw"},
            {"Ukrainian","uk"},
            {"Hindi","hi"},
            {"Urdu","ur"},
            {"Hungarian","hu"},
            {"Vietnamese","vi"},
            {"Icelandic","is"},
            {"Welsh","cy"},
            {"Indonesian","id"},
            {"Yiddish","yi"},

            #endregion
        };

        public static Dictionary<string, string> MappingLanguageOCR_URL = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            #region Dictionary of LanguageOCR URL 

            {"Afrikaans","https://github.com/tesseract-ocr/tessdata/raw/4.00/afr.traineddata"},
            {"Amharic","https://github.com/tesseract-ocr/tessdata/raw/4.00/amh.traineddata"},
            {"Arabic","https://github.com/tesseract-ocr/tessdata/raw/4.00/ara.traineddata"},
            {"Assamese","https://github.com/tesseract-ocr/tessdata/raw/4.00/asm.traineddata"},
            {"Azerbaijani","https://github.com/tesseract-ocr/tessdata/raw/4.00/aze.traineddata"},
            {"Azerbaijani - Cyrillic","https://github.com/tesseract-ocr/tessdata/raw/4.00/aze_cyrl.traineddata"},
            {"Belarusian","https://github.com/tesseract-ocr/tessdata/raw/4.00/bel.traineddata"},
            {"Bengali","https://github.com/tesseract-ocr/tessdata/raw/4.00/ben.traineddata"},
            {"Tibetan","https://github.com/tesseract-ocr/tessdata/raw/4.00/bod.traineddata"},
            {"Bosnian","https://github.com/tesseract-ocr/tessdata/raw/4.00/bos.traineddata"},
            {"Bulgarian","https://github.com/tesseract-ocr/tessdata/raw/4.00/bul.traineddata"},
            {"Catalan; Valencian","https://github.com/tesseract-ocr/tessdata/raw/4.00/cat.traineddata"},
            {"Cebuano","https://github.com/tesseract-ocr/tessdata/raw/4.00/ceb.traineddata"},
            {"Czech","https://github.com/tesseract-ocr/tessdata/raw/4.00/ces.traineddata"},
            {"Chinese - Simplified","https://github.com/tesseract-ocr/tessdata/raw/4.00/chi_sim.traineddata"},
            {"Chinese - Traditional","https://github.com/tesseract-ocr/tessdata/raw/4.00/chi_tra.traineddata"},
            {"Cherokee","https://github.com/tesseract-ocr/tessdata/raw/4.00/chr.traineddata"},
            {"Welsh","https://github.com/tesseract-ocr/tessdata/raw/4.00/cym.traineddata"},
            {"Danish","https://github.com/tesseract-ocr/tessdata/raw/4.00/dan.traineddata"},
            {"German","https://github.com/tesseract-ocr/tessdata/raw/4.00/deu.traineddata"},
            {"Dzongkha","https://github.com/tesseract-ocr/tessdata/raw/4.00/dzo.traineddata"},
            {"Greek, Modern (1453-)","https://github.com/tesseract-ocr/tessdata/raw/4.00/ell.traineddata"},
            {"English","https://github.com/tesseract-ocr/tessdata/raw/4.00/eng.traineddata"},
            {"English, Middle (1100-1500)","https://github.com/tesseract-ocr/tessdata/raw/4.00/enm.traineddata"},
            {"Esperanto","https://github.com/tesseract-ocr/tessdata/raw/4.00/epo.traineddata"},
            {"Estonian","https://github.com/tesseract-ocr/tessdata/raw/4.00/est.traineddata"},
            {"Basque","https://github.com/tesseract-ocr/tessdata/raw/4.00/eus.traineddata"},
            {"Persian","https://github.com/tesseract-ocr/tessdata/raw/4.00/fas.traineddata"},
            {"Finnish","https://github.com/tesseract-ocr/tessdata/raw/4.00/fin.traineddata"},
            {"French","https://github.com/tesseract-ocr/tessdata/raw/4.00/fra.traineddata"},
            {"German Fraktur","https://github.com/tesseract-ocr/tessdata/raw/4.00/frk.traineddata"},
            {"French, Middle (ca. 1400-1600)","https://github.com/tesseract-ocr/tessdata/raw/4.00/frm.traineddata"},
            {"Irish","https://github.com/tesseract-ocr/tessdata/raw/4.00/gle.traineddata"},
            {"Galician","https://github.com/tesseract-ocr/tessdata/raw/4.00/glg.traineddata"},
            {"Greek, Ancient (-1453)","https://github.com/tesseract-ocr/tessdata/raw/4.00/grc.traineddata"},
            {"Gujarati","https://github.com/tesseract-ocr/tessdata/raw/4.00/guj.traineddata"},
            {"Haitian; Haitian Creole","https://github.com/tesseract-ocr/tessdata/raw/4.00/hat.traineddata"},
            {"Hebrew","https://github.com/tesseract-ocr/tessdata/raw/4.00/heb.traineddata"},
            {"Hindi","https://github.com/tesseract-ocr/tessdata/raw/4.00/hin.traineddata"},
            {"Croatian","https://github.com/tesseract-ocr/tessdata/raw/4.00/hrv.traineddata"},
            {"Hungarian","https://github.com/tesseract-ocr/tessdata/raw/4.00/hun.traineddata"},
            {"Inuktitut","https://github.com/tesseract-ocr/tessdata/raw/4.00/iku.traineddata"},
            {"Indonesian","https://github.com/tesseract-ocr/tessdata/raw/4.00/ind.traineddata"},
            {"Icelandic","https://github.com/tesseract-ocr/tessdata/raw/4.00/isl.traineddata"},
            {"Italian","https://github.com/tesseract-ocr/tessdata/raw/4.00/ita.traineddata"},
            {"Italian - Old","https://github.com/tesseract-ocr/tessdata/raw/4.00/ita_old.traineddata"},
            {"Javanese","https://github.com/tesseract-ocr/tessdata/raw/4.00/jav.traineddata"},
            {"Japanese","https://github.com/tesseract-ocr/tessdata/raw/4.00/jpn.traineddata"},
            {"Kannada","https://github.com/tesseract-ocr/tessdata/raw/4.00/kan.traineddata"},
            {"Georgian","https://github.com/tesseract-ocr/tessdata/raw/4.00/kat.traineddata"},
            {"Georgian - Old","https://github.com/tesseract-ocr/tessdata/raw/4.00/kat_old.traineddata"},
            {"Kazakh","https://github.com/tesseract-ocr/tessdata/raw/4.00/kaz.traineddata"},
            {"Central Khmer","https://github.com/tesseract-ocr/tessdata/raw/4.00/khm.traineddata"},
            {"Kirghiz; Kyrgyz","https://github.com/tesseract-ocr/tessdata/raw/4.00/kir.traineddata"},
            {"Korean","https://github.com/tesseract-ocr/tessdata/raw/4.00/kor.traineddata"},
            {"Kurdish","https://github.com/tesseract-ocr/tessdata/raw/4.00/kur.traineddata"},
            {"Lao","https://github.com/tesseract-ocr/tessdata/raw/4.00/lao.traineddata"},
            {"Latin","https://github.com/tesseract-ocr/tessdata/raw/4.00/lat.traineddata"},
            {"Latvian","https://github.com/tesseract-ocr/tessdata/raw/4.00/lav.traineddata"},
            {"Lithuanian","https://github.com/tesseract-ocr/tessdata/raw/4.00/lit.traineddata"},
            {"Malayalam","https://github.com/tesseract-ocr/tessdata/raw/4.00/mal.traineddata"},
            {"Marathi","https://github.com/tesseract-ocr/tessdata/raw/4.00/mar.traineddata"},
            {"Macedonian","https://github.com/tesseract-ocr/tessdata/raw/4.00/mkd.traineddata"},
            {"Maltese","https://github.com/tesseract-ocr/tessdata/raw/4.00/mlt.traineddata"},
            {"Malay","https://github.com/tesseract-ocr/tessdata/raw/4.00/msa.traineddata"},
            {"Burmese","https://github.com/tesseract-ocr/tessdata/raw/4.00/mya.traineddata"},
            {"Nepali","https://github.com/tesseract-ocr/tessdata/raw/4.00/nep.traineddata"},
            {"Dutch; Flemish","https://github.com/tesseract-ocr/tessdata/raw/4.00/nld.traineddata"},
            {"Norwegian","https://github.com/tesseract-ocr/tessdata/raw/4.00/nor.traineddata"},
            {"Oriya","https://github.com/tesseract-ocr/tessdata/raw/4.00/ori.traineddata"},
            {"Panjabi; Punjabi","https://github.com/tesseract-ocr/tessdata/raw/4.00/pan.traineddata"},
            {"Polish","https://github.com/tesseract-ocr/tessdata/raw/4.00/pol.traineddata"},
            {"Portuguese","https://github.com/tesseract-ocr/tessdata/raw/4.00/por.traineddata"},
            {"Pushto; Pashto","https://github.com/tesseract-ocr/tessdata/raw/4.00/pus.traineddata"},
            {"Romanian; Moldavian; Moldovan","https://github.com/tesseract-ocr/tessdata/raw/4.00/ron.traineddata"},
            {"Russian","https://github.com/tesseract-ocr/tessdata/raw/4.00/rus.traineddata"},
            {"Sanskrit","https://github.com/tesseract-ocr/tessdata/raw/4.00/san.traineddata"},
            {"Sinhala; Sinhalese","https://github.com/tesseract-ocr/tessdata/raw/4.00/sin.traineddata"},
            {"Slovak","https://github.com/tesseract-ocr/tessdata/raw/4.00/slk.traineddata"},
            {"Slovenian","https://github.com/tesseract-ocr/tessdata/raw/4.00/slv.traineddata"},
            {"Spanish; Castilian","https://github.com/tesseract-ocr/tessdata/raw/4.00/spa.traineddata"},
            {"Spanish; Castilian - Old","https://github.com/tesseract-ocr/tessdata/raw/4.00/spa_old.traineddata"},
            {"Albanian","https://github.com/tesseract-ocr/tessdata/raw/4.00/sqi.traineddata"},
            {"Serbian","https://github.com/tesseract-ocr/tessdata/raw/4.00/srp.traineddata"},
            {"Serbian - Latin","https://github.com/tesseract-ocr/tessdata/raw/4.00/srp_latn.traineddata"},
            {"Swahili","https://github.com/tesseract-ocr/tessdata/raw/4.00/swa.traineddata"},
            {"Swedish","https://github.com/tesseract-ocr/tessdata/raw/4.00/swe.traineddata"},
            {"Syriac","https://github.com/tesseract-ocr/tessdata/raw/4.00/syr.traineddata"},
            {"Tamil","https://github.com/tesseract-ocr/tessdata/raw/4.00/tam.traineddata"},
            {"Telugu","https://github.com/tesseract-ocr/tessdata/raw/4.00/tel.traineddata"},
            {"Tajik","https://github.com/tesseract-ocr/tessdata/raw/4.00/tgk.traineddata"},
            {"Tagalog","https://github.com/tesseract-ocr/tessdata/raw/4.00/tgl.traineddata"},
            {"Thai","https://github.com/tesseract-ocr/tessdata/raw/4.00/tha.traineddata"},
            {"Tigrinya","https://github.com/tesseract-ocr/tessdata/raw/4.00/tir.traineddata"},
            {"Turkish","https://github.com/tesseract-ocr/tessdata/raw/4.00/tur.traineddata"},
            {"Uighur; Uyghur","https://github.com/tesseract-ocr/tessdata/raw/4.00/uig.traineddata"},
            {"Ukrainian","https://github.com/tesseract-ocr/tessdata/raw/4.00/ukr.traineddata"},
            {"Urdu","https://github.com/tesseract-ocr/tessdata/raw/4.00/urd.traineddata"},
            {"Uzbek","https://github.com/tesseract-ocr/tessdata/raw/4.00/uzb.traineddata"},
            {"Uzbek - Cyrillic","https://github.com/tesseract-ocr/tessdata/raw/4.00/uzb_cyrl.traineddata"},
            {"Vietnamese","https://github.com/tesseract-ocr/tessdata/raw/4.00/vie.traineddata"},
            {"Yiddish","https://github.com/tesseract-ocr/tessdata/raw/4.00/yid.traineddata"},

            #endregion
        };

        public static string GetLanguageOCRCode(string key)
        {
            string temp;
            string OCRLangCode = MappingLanguageOCR.TryGetValue(key, out temp) ? temp : "eng";

            return OCRLangCode;
        }

        public static string GetLanguageOCR_URL(string key)
        {
            string temp;
            string OCRLangUrl = MappingLanguageOCR.TryGetValue(key, out temp) ? temp : "eng";

            return OCRLangUrl;
        }

        public static string GetTranslateLanguageCode(string key)
        {
            string temp;
            string translateLanguageCode = LanguageDictionary.MappingLanguageTranslate.TryGetValue(key, out temp) ? temp : "vi";

            return translateLanguageCode;
        }

        public static string GetLanguageOCRCodeByValue(string value)
        {
            return MappingLanguageOCR.FirstOrDefault(x => x.Value == value).Key;
        }

        public static string GetTranslateLanguageByValue(string value)
        {
            return MappingLanguageTranslate.FirstOrDefault(x => x.Value == value).Key;
        }

    }
}
