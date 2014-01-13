// Hello World! : hello.cs
 
using System;

using GD;

class Hello {
    static void Main() {
        Console.WriteLine ("Hello, World!");
        Console.WriteLine (Image.versionString);

        Image foo = Image.createFromFile("../grlibs-misc/img/greenbox.png");
        Console.WriteLine(foo.sx);
        Console.WriteLine(foo.sy);

        Console.WriteLine(Font.small.w);
    }
}

