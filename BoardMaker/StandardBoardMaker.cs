using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace GuessWhoGenerator.BoardMaker;

public class StandardBoardMaker : IBoardMaker
{
    public void MakeBoard(string imageLocation, string imageFormat, string outputDirectory, int numberOfImages)
    {
        List<Image<Rgba32>> imagesToAdd = new List<Image<Rgba32>>();
        Image<Rgba32>? resultantImage = null;

        try 
        {
            int width = 0;
            int height = 0;

            for(int i = 0; i < numberOfImages; ++i) 
            {
                Image<Rgba32> image = Image.Load<Rgba32>(imageLocation + $"{i}.{imageFormat}");
                imagesToAdd.Add(image);

                width += image.Width;
                height = image.Height > height ? image.Height : height;
            }
            
            resultantImage = new Image<Rgba32>(width, height);
            
            int xOffset = 0;
            foreach (Image<Rgba32> image in imagesToAdd) 
            {
                resultantImage.Mutate(r => r.DrawImage(image, new Point(xOffset, 0), 1));
                xOffset += image.Width;
            }

            resultantImage.Save(imageLocation + "THINGS.jpg");
        } 
        catch (Exception e)
        {
            Console.WriteLine(e);
        } 
        finally
        {
            foreach(Image<Rgba32> image in imagesToAdd)
            {
                image.Dispose();
            }

            resultantImage?.Dispose();
        }
    }
}