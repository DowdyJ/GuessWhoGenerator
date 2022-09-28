using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GuessWhoGenerator.ImageGetter
{
    public class PicrewImageGetter : IImageGetter
    {
        private string _picrewUrl;

        public PicrewImageGetter(string picrewUrl) 
        {
            this._picrewUrl = picrewUrl;
        }

        public async Task GetImages(string saveToPath, int numberOfImages)
        {
            await GetRandomPicrewImages(saveToPath, _picrewUrl, numberOfImages, true, false);
            return;
        }


        private async Task GetRandomPicrewImages(string saveToPath, string picrewUrl, int numberOfImages, bool randomizeAll = true, bool randomizeItems = false) 
        {
            Directory.CreateDirectory(saveToPath);

            using BrowserFetcher browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            using Page page = await browser.NewPageAsync();

            await page.GoToAsync(picrewUrl);

            ElementHandle[] elementHandles = await page.QuerySelectorAllAsync("canvas");
            string base64Image = "";
            if (elementHandles.Length == 1)
            {
                await elementHandles[0].EvaluateFunctionAsync("() => {document.getElementsByClassName(\"imagemaker_menu_btn\")[0].dispatchEvent(new Event('click'));}");
                
                for (int i = 0; i < numberOfImages; ++i) 
                {
                    await Task.Delay(500);

                    if (randomizeAll)
                        await elementHandles[0].EvaluateFunctionAsync("() => {document.getElementsByClassName(\"imagemaker_btn_random\")[0].dispatchEvent(new Event('click'));}");
                    if (randomizeItems)
                        await elementHandles[0].EvaluateFunctionAsync("() => {document.getElementsByClassName(\"imagemaker_btn_random\")[1].dispatchEvent(new Event('click'));}");

                    base64Image = (await elementHandles[0].EvaluateFunctionAsync("() => {return document.querySelector(\"canvas\").toDataURL();}")).ToString();
                    base64Image = base64Image.Substring(base64Image.IndexOf("base64,") + 7);

                    byte[] imageAsBytes = Convert.FromBase64String(base64Image);
                    File.WriteAllBytes(saveToPath + $"{i}.png", imageAsBytes);
                }

            }

        }
    }
}
