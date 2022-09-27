

namespace GuessWhoGenerator 
{
    public class Program 
    {
        public static void Main(string[] args) 
        {
            IImageGetter imageGetter = new ThisPersonDoesNotExistImageGetter();

            imageGetter.GetImages().Wait();

            return;
        }
    }
}