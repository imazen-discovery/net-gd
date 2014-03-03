# net-gd

This is a Mono/DotNet binding to LibGD, currently in a very early
stage of development.

Right now, it implements about 40% of the API.  (I'm currently unclear
on how much of the rest is actually useful to have.)

I have it compiling against Mono on Linux (Ubuntu, to be specific).
I'll likely have it working on some form of Windows relatively soon.

## Compiling on Linux and Mono

0. Install mono, nunit, gcc and gmake.
1. Get the latest dev version of LibGD from
   <https://bitbucket.org/libgd/gd-libgd> and build+install it.
   Ideally, it should be in the same parent directory as this source
   tree.
2. Skip ahead to the next step.  If that doesn't work, edit cfg.sh.
   Specifically, MONO_MCS should contain the C# compile command to use
   and GD_DIR should contain the path to the GD source tree.  You may
   also need to edit other settings.
3. Type `make && make test`.
4. (Optional) Type `make doc` to build HTML documentation.

If everything works, you should now have a DotNet assembly suitable
for linking against your Mono project.


## Compiling on Windows and DotNet 4.5

0. Build LibGD from source according to the instructions in
   `gd-libgd/windows/msys/README.MSYS.md`.  You will need to obtain
   the source via git from <https://bitbucket.org/libgd/gd-libgd> and
   use the latest version in the trunk.  Ideally, the GD source tree
   and this one should share the same parent directory.  This step
   also entails installing MinGW and Msys.
1. Install Visual Studio 2013 for C#.  (I use Professional but others
   should work too.)
2. Install NUnit 2.6.x from
   <https://launchpadlibrarian.net/153448476/NUnit-2.6.3.msi> or
   thereabouts.
3. Skip ahead to the next step.  If that fails, edit cfg.sh to the
   appropriate values and try again.  (In theory, you should only need
   to edit things before the line of dashes.)
4. In an Msys session, `cd` to the build directory and type
   `make && make test`.

### Hints

 This assumes a 32-bit Windows systems.  64-bit systems will require
paths to be edited.

Currently, creating documentation only works on mono.


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







