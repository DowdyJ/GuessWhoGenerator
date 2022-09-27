using GuessWhoGenerator.ImageGetter;


namespace GuessWhoGenerator
{
    public class Program 
    {
        public async static Task Main(string[] args) 
        {
            IImageGetter imageGetter = new PicrewImageGetter();

            await imageGetter.GetImages(10);

            return;
        }
    }
}