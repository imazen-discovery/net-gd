// Hello World! : hello.cs
 
using System;

using GD;

class Hello {
    static void Main() {
        Console.WriteLine ("Hello, World!");
        Console.WriteLine (Image.versionString);

        Image foo = Image.createFromFile("greenbox.png");
        if (foo == null) {
          Console.WriteLine("Image load failed.");
        } else {
          Console.WriteLine(foo.sx);
          Console.WriteLine(foo.sy);
        }

        Console.WriteLine(Font.small.w);
    }
}

