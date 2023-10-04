using GuessWhoGenerator.ImageGetter;
using GuessWhoGenerator.BoardMaker;
using System.Reflection;

namespace GuessWhoGenerator
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            IImageGetter imageGetter = new PicrewImageGetter(args[0]);
            IBoardMaker boardMaker = new StandardBoardMaker();

            string outDir = @$"{Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)}/downloadedPictures/";
            int numberOfImagesToGenerate = 10;
            await imageGetter.GetImages(outDir, numberOfImagesToGenerate);

            boardMaker.MakeBoard(outDir, "png", outDir, numberOfImagesToGenerate);

            return;
        }
    }
}
