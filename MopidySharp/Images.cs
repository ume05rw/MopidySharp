using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mopidy
{
    /// <summary>
    /// Image Util
    /// </summary>
    public class Images
    {
        /// <summary>
        /// Get C# System.Drawing.Image Object
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static async Task<Image> GetNative(Models.Image image)
        {
            if (string.IsNullOrEmpty(image.Uri))
                return null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                var url = Settings.BaseUrl + image.Uri;

                HttpResponseMessage message = null;
                try
                {
                    message = await client.GetAsync(url);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);

                    return null;
                }

                var bytes = await message.Content.ReadAsByteArrayAsync();
                using (var stream = new MemoryStream(bytes))
                {
                    try
                    {
                        return Bitmap.FromStream(stream);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        Debug.WriteLine(ex.StackTrace);

                        return null;
                    }
                }
            }
        }
    }
}
