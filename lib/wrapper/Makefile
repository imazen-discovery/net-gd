
GDPATH := $(shell ../../cfg.sh gdpath)/src

CC=gcc
CFLAGS=-I$(GDPATH) -fPIC -g -Wall

LD=gcc
LDFLAGS=-shared -Wl,-soname,GDwrap.so
LIBS=-lc -lgd

MCS := $(shell ../../cfg.sh compiler)
SWIG=swig
SWIGFLAGS=-v -Wall -I$(GDPATH) -dllimport GDwrap.so -namespace GD.Internal -noproxy

.SUFFIXES: .i .dll .exe

all: libGDwrap.so net-gd-glue.dll

net-gd-glue.dll: libGDwrap.so
	$(MCS) -t:library -out:net-gd-glue.dll *.cs

libGDwrap.so : GDwrap.o
	$(LD) $(LDFLAGS) -o libGDwrap.so GDwrap.o $(LIBS)

GDwrap.c: gd.i
	$(SWIG) $(SWIGFLAGS) -csharp -o GDwrap.c gd.i

GDwrap.o : GDwrap.c

clean:
	-rm *.cs GDwrap.c *.so *.o *.dll

