using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GuessWhoGenerator
{
    internal class ThisPersonDoesNotExistImageGetter : IImageGetter
    {
        public async Task GetImages(int numberOfImages)
        {
            List<Task> makeImagesTasks = new List<Task>();

            for (int i = 0; i < numberOfImages; ++i)
            {
                await Task.Delay(2000);
                makeImagesTasks.Add(CreateSingleImage($"TEST{i}"));
            }

            await Task.WhenAll(makeImagesTasks);

            return;
        }


        private async Task CreateSingleImage(string fileName) 
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("image/jpeg"));
                client.MaxResponseContentBufferSize = 256000000;
                client.DefaultRequestHeaders.ConnectionClose = true;
                client.DefaultRequestHeaders.UserAgent.ParseAdd($"Mozilla/5.0 (compatible; {fileName}/1.0)");
                string outDir = @$"{Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)}\downloadedPictures\";
                string requestUrl = @"https://thispersondoesnotexist.com/image";

                Directory.CreateDirectory(outDir);

                try
                {
                    using (Stream streamFromServer = await client.GetStreamAsync(requestUrl))
                    {
                        using (FileStream fs = new FileStream(outDir + $"{ fileName }.jpg", FileMode.Create, FileAccess.Write))
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
