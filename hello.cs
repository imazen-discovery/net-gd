// Hello World! : hello.cs
 
using System;
using GD.Internal;

class Hello {
    static void Main() {
        Console.WriteLine ("Hello, World!");
        Console.WriteLine (LibGD.gdVersionString());
        
//        gdImage foo = LibGD.gdImageCreate(300, 300);
//        Console.WriteLine(foo.sx);
    }
}

