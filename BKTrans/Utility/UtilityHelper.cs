using Microsoft.Win32;
using NAudio.Wave;
using Newtonsoft.Json;
using BKTrans.Entities;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tesseract;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace BKTrans.Utility
{
    public static class UtilityHelper
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public static Rectangle DesktopRectangle => SystemInformation.VirtualScreen;

        public static BitmapImage GetScreenCapture()
        {
            Bitmap screenshotBtm = new Bitmap(DesktopRectangle.Width, DesktopRectangle.Height);

            using (Graphics graphics = Graphics.FromImage(screenshotBtm))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, screenshotBtm.Size);
            }

            IntPtr handle = IntPtr.Zero;
            try
            {
                handle = screenshotBtm.GetHbitmap();

                ImageSourceConverter cv = new ImageSourceConverter();
                BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()); ;

                BitmapImage image = Helper.GetBitmapImageFromBitmapSource(bitmapSource);

                return image;
            }
            catch (Exception ea)
            {
                System.Windows.MessageBox.Show(ea.Message);
            }
            finally
            {
                DeleteObject(handle);
            }
            return null;
        }

        public static BitmapImage GetCroppedImage(RegionCapture regionCapture)
        {
            BitmapSource bitmapSource = new CroppedBitmap(regionCapture.ScreenCapture, new Int32Rect((int)regionCapture.Position.X, (int)regionCapture.Position.Y,
                (int)regionCapture.SelectedRegion.Width, (int)regionCapture.SelectedRegion.Height));
            return Helper.GetBitmapImageFromBitmapSource(bitmapSource);
        }

        public static byte[] GetCroppedImageData(RegionCapture regionCapture)
        {
            BitmapSource bitmapSource = new CroppedBitmap(regionCapture.ScreenCapture, new Int32Rect((int)regionCapture.Position.X, (int)regionCapture.Position.Y,
                (int)regionCapture.SelectedRegion.Width, (int)regionCapture.SelectedRegion.Height));
            return Helper.GetMemoryFromBitmapSource(bitmapSource);
        }

        public static void PlayMp3FromUrl(string url)
        {
            Task.Factory.StartNew(() =>
            {
                using (Stream ms = new MemoryStream())
                {
                    using (Stream stream = System.Net.WebRequest.Create(url)
                        .GetResponse().GetResponseStream())
                    {
                        byte[] buffer = new byte[32768];
                        int read;
                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                    }
                    ms.Position = 0;
                    using (WaveStream blockAlignedStream =
                        new BlockAlignReductionStream(
                            WaveFormatConversionStream.CreatePcmStream(
                                new Mp3FileReader(ms))))
                    {
                        using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                        {
                            waveOut.Init(blockAlignedStream);
                            waveOut.Play();
                            while (waveOut.PlaybackState == PlaybackState.Playing)
                            {
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                    }
                }
            });
        }

        public static async Task<TranslateResult> GetTranslateResult(string text, string targetLangCode = "vi")
        {
            if (text.Length > 0)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.Timeout = TimeSpan.FromMinutes(5);

                    string translateUrl = @"http://translate.google.com/translate_a/single?client=gtx&dt=t&dt=bd&dj=1" +
                        "&source=input&sl=auto&tl=" + targetLangCode + "&q=" + Uri.EscapeDataString(text);

                    Stream response = await client.GetStreamAsync(translateUrl);
                    using (StreamReader reader = new StreamReader(response))
                    {
                        string responseString = reader.ReadToEnd();
                        TranslateResult result = JsonConvert.DeserializeObject<TranslateResult>(responseString);
                        return result;
                    }
                }
                catch
                {
                    return null;
                }
                //return translateText;
            }
            else
            {
                //MessageBox.Show("Vui lòng chọn lại.");
                return null;
            }
        }

        public static async Task<string> GetParseImageResult(byte[] imageData, string ocrLang)
        {
            try
            { 
                var langCode = LanguageDictionary.GetLanguageOCRCode(ocrLang);

                using (var engine = new TesseractEngine(App.TESSDATA_DICTPATH, langCode, EngineMode.Default))
                {
                    using (var img = Pix.LoadFromMemory(imageData))
                    {
                        using (var page = engine.Process(img))
                        {
                            var input = page.GetText();

                            string pattern = @"\n([^A-Z^0-9])";
                            string substitution = @" $1";
                            RegexOptions options = RegexOptions.Multiline;
                            Regex regex = new Regex(pattern, options);

                            string result = regex.Replace(input, substitution).Trim();
                            if (result.StartsWith("\n"))
                                result = result.Substring(2);
                            if (result == null || result.Length < 1)
                                throw new Exception("Try again");
                            return result;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Failed to initialise tesseract engine"))
                    throw new Exception("Can't find data of " + ocrLang);
                throw e;
            }
        }

        static RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public static void RegisterInStartup(bool? isChecked)
        {
            
            if (isChecked == true)
            {
                registryKey.SetValue(System.Windows.Application.Current.TryFindResource("ApplicationTitle").ToString(), System.Reflection.Assembly.GetExecutingAssembly().Location);
            }   
            else if (registryKey.GetValue(System.Windows.Application.Current.TryFindResource("ApplicationTitle").ToString()) != null)
            {
                registryKey.DeleteValue(System.Windows.Application.Current.TryFindResource("ApplicationTitle").ToString());
            }
        }
    }
}
