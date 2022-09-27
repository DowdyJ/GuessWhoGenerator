

namespace GuessWhoGenerator 
{
    public class Program 
    {
        public async static Task Main(string[] args) 
        {
            IImageGetter imageGetter = new ThisPersonDoesNotExistImageGetter();

            await imageGetter.GetImages(10);

            return;
        }
    }
}