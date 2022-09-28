
namespace GuessWhoGenerator.BoardMaker;

public interface IBoardMaker 
{
    public abstract void MakeBoard(string imageLocation, string imageFormat, string outputDirectory, int numberOfImages);
}