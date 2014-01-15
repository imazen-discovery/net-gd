
GDPATH := $(shell ../../lib-gd-path.sh)

CC=gcc
CFLAGS=-fPIC -g -c -Wall

MCS=../../mcs.sh
SWIG=swig
SWIGFLAGS=-v -Wall -I$(GDPATH) -dllimport GDwrap.so -namespace GD.Internal -noproxy

.prefixes: .i

all: libGDwrap.so net-gd-glue.dll

net-gd-glue.dll: libGDwrap.so
	$(MCS) -t:library -out:net-gd-glue.dll ../delegates.cs *.cs

libGDwrap.so : GDwrap.o ctx.o
	gcc -shared -Wl,-soname,GDwrap.so -o libGDwrap.so GDwrap.o -lc -lgd

GDwrap.o: GDwrap.c
ctx.o: ctx.c ctx.h

GDwrap.c: gd.i
	$(SWIG) $(SWIGFLAGS) -csharp -o GDwrap.c gd.i

clean:
	-rm *.cs *.so *.o *.dll GDwrap.c


