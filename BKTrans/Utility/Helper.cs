using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BKTrans.Utility
{
    public static class Helper
    {

        public const string ENCRYPT_KEY = "3oT/9#ia*28^@-Scpemgail9uzpgzl88";

        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "pemgail9uzpgzl88";
        private const int keysize = 256;

        public static BitmapImage ReadImageFromBase64(string base64String)
        {
            byte[] binaryData = Convert.FromBase64String(base64String);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(binaryData);
            bitmapImage.EndInit();

            return bitmapImage;
        }

        public static string WriteImageToBase64(BitmapImage image, string format = "png")
        {
            using (MemoryStream memory = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(memory);

                byte[] imageData = memory.ToArray();

                return Convert.ToBase64String(imageData);
            }
                
        }

        public static BitmapImage GetBitmapImageFromBitmapSource(BitmapSource source)
        {
            MemoryStream memory = new MemoryStream();

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(source));
            encoder.Save(memory);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = memory;
            image.EndInit();

            return image;
        }

        public static byte[] GetMemoryFromBitmapSource(BitmapSource source)
        {
            using(MemoryStream memory = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(source));
                encoder.Save(memory);
                return memory.ToArray();
            }
        }

        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void FastCopy(string inputPath, string outputPath)
        {
            byte[] buffer = new byte[4 * 1024];
            int len;

            using(Stream input = new FileStream(inputPath, FileMode.Open,FileAccess.ReadWrite ))
            {
                using (Stream output = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, len);
                    }
                }
            }
        }
        public static byte[] EncryptStringToByte(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return cipherTextBytes;
        }

        public static string DecryptByteToString(byte[] cipherTextBytes, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
