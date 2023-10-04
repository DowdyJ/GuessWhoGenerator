# GuessWhoGenerator
This is a simple tool used to generate composite images from image generator sites programatically. 

## Usage
Currently, either Picrew or [This Person Does Not Exist](https://thispersondoesnotexist.com/) can be used, though the image downloader class must be manually selected before running/compiling.

To install, run `dotnet restore`, then run `dotnet run [your link here]`. It will then download the images from the site, stitch them together and output it in your build folder under downloadedPictures/
