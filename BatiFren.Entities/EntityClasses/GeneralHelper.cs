using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BatiFren.Entities.EntityClasses
{
    public class GeneralHelper
    {
        BatiFrenDBEntities db = new BatiFrenDBEntities();

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        


        public string GetVisitorIPAddress()
        {
            string IPAdd = string.Empty;
            IPAdd = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAdd))
                IPAdd = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return IPAdd;
        }

        // generate module url
        public string GetModuleSlug(String ModuleName, int ParentModuleID)
        {
            string result = string.Empty;
            ModuleName = ModuleName.Replace(" ", "");
            if (String.IsNullOrEmpty(result))
                result = ModuleName;
            if (ParentModuleID > 0)
                result = GetModuleByID(ParentModuleID).ModuleName.Replace(" ", "") + "_" + result;
            result = "li" + result;
            return RemoveIllegalCharacters(result);
        }

        public Module GetModuleByID(int? CategoryID)
        {
            var module = (from c in db.Modules
                          where c.ModuleID == CategoryID
                          select c).FirstOrDefault();
            return module;
        }

        private string RemoveExtraHyphen(string text)
        {
            if (text.Contains("__"))
            {
                text = text.Replace("__", "_");
                return RemoveExtraHyphen(text);
            }
            return text;
        }

        private string RemoveDiacritics(string text)
        {
            string Normalize = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= Normalize.Length - 1; i++)
            {
                char c = Normalize[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public string RemoveIllegalCharacters(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace("'", string.Empty);
            //text = text.Replace("_", string.Empty);
            text = text.Replace(" ", "-");
            text = RemoveDiacritics(text);
            text = RemoveExtraHyphen(text);

            return HttpUtility.UrlEncode(text.ToLower()).Replace("%", string.Empty);
        }

        public Bitmap GetScaledPicture(Stream streamImage, int maxWidth)
        {
            Bitmap originalImage = new Bitmap(streamImage);
            Double ratio = (Double)originalImage.Width / maxWidth;
            var newWidth = (int)maxWidth;
            var newHeight = (int)(originalImage.Height / ratio);

            Bitmap newImage = new Bitmap(originalImage, newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(newImage);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(originalImage, imageRectangle);
            originalImage.Dispose();

            return newImage;
        }

        public String GetTimeAgo(DateTime date)
        {
            String str = "";
            TimeSpan ts = DateTime.Now - date;

            if (ts.Days < 1)
            {
                if (ts.Hours < 1)
                {
                    if (ts.Minutes < 1)
                        str = "Just now";
                    else if (ts.Minutes > 0 && ts.Minutes < 61)
                        str = ts.Minutes + " mins ago";
                }
                else
                    str = ts.Hours + " hours ago";
            }
            else if ((ts.Days) < 7)
                str = ts.Days + " days ago";
            else
                str = date.ToString("MMM dd,yyyy");

            return str;
        }
    }
}
