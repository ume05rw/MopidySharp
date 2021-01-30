using System;
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
        /// Get System.Drawing.Image Object
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Task<Image> GetNative(Models.Image image)
        {
            if (image == null)
                return null;

            return Images.GetNative(image.Uri);
        }

        /// <summary>
        /// Get System.Drawing.Image Object
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<Image> GetNative(string uri)
        {
            if (string.IsNullOrEmpty(uri))
                return null;

            var fullUri = (uri.ToLowerInvariant().StartsWith("http"))
                ? uri
                : Settings.BaseUrl + uri;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                HttpResponseMessage message = null;
                try
                {
                    message = await client.GetAsync(fullUri);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);

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
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                        return null;
                    }
                }
            }
        }
    }
}
