using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessWhoGenerator
{
    public interface IImageGetter
    {
        public Task GetImages(string outputDirectory, int numberOfImages);
    }
}
