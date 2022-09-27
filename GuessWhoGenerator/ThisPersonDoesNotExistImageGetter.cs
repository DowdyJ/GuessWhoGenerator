using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GuessWhoGenerator
{
    internal class ThisPersonDoesNotExistImageGetter : IImageGetter
    {
        public async Task<List<Image>?> GetImages()
        {
            List<Task> makeImagesTasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(2000);
                makeImagesTasks.Add(CreateSingleImage($"TEST{i}"));
            }

            await Task.WhenAll(makeImagesTasks);
            
            return new List<Image>();
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
                string outDir = @$"C:\Users\joeld\OneDrive\Documents\Programming\GuessWhoGenerator\{fileName}.jpg";
                string requestUrl = @"https://thispersondoesnotexist.com/image";
                try
                {
                    using (Stream streamFromServer = await client.GetStreamAsync(requestUrl))
                    {
                        using (FileStream fs = new FileStream(outDir, FileMode.Create, FileAccess.Write))
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
