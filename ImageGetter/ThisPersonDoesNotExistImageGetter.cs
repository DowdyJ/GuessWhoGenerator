using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GuessWhoGenerator.ImageGetter
{
    internal class ThisPersonDoesNotExistImageGetter : IImageGetter
    {
        public async Task GetImages(string outDir, int numberOfImages)
        {
            List<Task> makeImagesTasks = new List<Task>();

            for (int i = 0; i < numberOfImages; ++i)
            {
                await Task.Delay(2000);
                makeImagesTasks.Add(CreateSingleImage(outDir, $"{i}"));
            }

            await Task.WhenAll(makeImagesTasks);

            return;
        }


        private async Task CreateSingleImage(string outdir, string fileName)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("image/jpeg"));
                client.MaxResponseContentBufferSize = 256000000;
                client.DefaultRequestHeaders.ConnectionClose = true;
                client.DefaultRequestHeaders.UserAgent.ParseAdd($"Mozilla/5.0 (compatible; {fileName}/1.0)");
                string requestUrl = @"https://thispersondoesnotexist.com/image";

                Directory.CreateDirectory(outdir);

                try
                {
                    using (Stream streamFromServer = await client.GetStreamAsync(requestUrl))
                    {
                        using (FileStream fs = new FileStream(outdir + $"{fileName}.jpg", FileMode.Create, FileAccess.Write))
                        {
                            streamFromServer.CopyTo(fs);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }

        }





    }
}
