using GuessWhoGenerator.ImageGetter;
using GuessWhoGenerator.BoardMaker;
using System.Reflection;

namespace GuessWhoGenerator
{
    public class Program 
    {
        public async static Task Main(string[] args) 
        {
            IImageGetter imageGetter = new PicrewImageGetter(@"https://picrew.me/image_maker/516657");
            IBoardMaker boardMaker = new StandardBoardMaker();

            string outDir = @$"{Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)}/downloadedPictures/";
            int numberOfImagesToGenerate = 10;
            await imageGetter.GetImages(outDir, numberOfImagesToGenerate);
            
            boardMaker.MakeBoard(outDir, "png", outDir, numberOfImagesToGenerate);

            return;
        }
    } 
}