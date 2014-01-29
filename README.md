# net-gd

This is a Mono/DotNet binding to LibGD, currently in a very early
stage of development.

Right now, it implements about 40% of the API.  (I'm currently unclear
on how much of the rest is actually useful to have.)

I have it compiling against Mono on Linux (Ubuntu, to be specific).
I'll likely have it working on some form of Windows relatively soon.

## Compiling

0. Install mono, gcc and gmake.
1. Get the latest dev version of LibGD from
   <https://bitbucket.org/libgd/gd-libgd> and build+install it.
2. Edit lib-gd-path.sh (variable "dir") to point to the directory
   containing the GD header files.  (I use the 'src/' subdirectory of the
   build tree, but the installation directory should also work.)
3. If necessary, edit mcs.sh to run your preferred C# compiler.
4. Type `make && make test`.

If everything works, you should now have a DotNet assembly suitable
for linking against your Mono project.

## Documentation

There is now some basic documentation for GD.  To generate it, enter
the project root directory and type:

    make docs

This will create HTML documention in the subdirectory html-docs.

Note that this manual is pretty minimal.  It is written with the
assumption that have the original GD manual on hand an can refer to it
as needed.

(Unfortunately, the GD manual is currently woefully incomplete, so you
may need to delve into the GD source code.  Sorry about that.)







