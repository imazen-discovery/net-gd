// Read in a JPEG image file and print out its dimensions.

// Example which loads a file via the ImageData class.
 
using System;
using System.IO;

using GD;

class Hello {
  static int Main(string[] args) {
    if (args.Length != 1) {
      Console.WriteLine("Usage: loadimg.exe <filename>");
      return 1;
    }/* if */

    // Create the filestream and reader.  (DotNet: TWO file objects
    // for the price of ONE!)
    var fs = new FileStream(args[0], FileMode.Open);
    var r = new BinaryReader(fs);

    // Suck the data into an ImageData.
    ImageData id = new ImageData(r, Enc.JPEG);
    if (id == null) {
      Console.WriteLine("Error eading image data.");
      return 1;
    }/* if */

    // And decode the file data into an Image object.
    Image img = id.decode();
    Console.WriteLine("Image {0}: {1}x{2}", args[0], img.sx, img.sy);

    return 0;
  }
}

