
GDPATH = ../../../gd-libgd/src/

SWIG=swig
SWIGFLAGS=-v -Wall -I$(GDPATH) -dllimport GDwrap.so -namespace GD.Internal -noproxy

.prefixes: .i

all: libGDwrap.so net-gd-glue.dll

net-gd-glue.dll: libGDwrap.so
	mcs -t:library -out:net-gd-glue.dll *.cs

libGDwrap.so : GDwrap.c
	gcc -fPIC -g -c -Wall GDwrap.c
	gcc -shared -Wl,-soname,GDwrap.so -o libGDwrap.so GDwrap.o -lc -lgd

GDwrap.c: gd.i
	$(SWIG) $(SWIGFLAGS) -csharp -o GDwrap.c gd.i

clean:
	-rm *.cs *.c *.so *.o *.dll

