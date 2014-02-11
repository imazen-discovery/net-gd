
GDPATH		:= $(shell ../../cfg.sh gdpath)/src
PATH		:= $(shell ../../cfg.sh path)
SO			:= $(shell ../../cfg.sh sharedobjext)
CF_EXTRA	:= $(shell ../../cfg.sh cflags)

CC=gcc
CFLAGS=-I$(GDPATH) $(CF_EXTRA)

LD=gcc
LDFLAGS=-shared -Wl,-soname,GDwrap.$(SO)
LIBS= -lgd -L ../../../gd-libgd/src/ # -lc -lgd

MCS := $(shell ../../cfg.sh compiler)
SWIG=swig
SWIGFLAGS=-v -Wall -I$(GDPATH) -dllimport GDwrap.$(SO) -namespace GD.Internal -noproxy

.SUFFIXES: .i .dll .exe

all: libGDwrap.$(SO) net-gd-glue.dll

net-gd-glue.dll: libGDwrap.$(SO)
	$(MCS) -t:library -out:net-gd-glue.dll *.cs

libGDwrap.$(SO) : GDwrap.o
	$(LD) $(LDFLAGS) -o libGDwrap.$(SO) GDwrap.o $(LIBS)

#gcc -shared -Wl,-soname,GDwrap.$(SO) -o libGDwrap.$(SO) GDwrap.o  -lgd.3.0.1 -L ../../../gd-libgd/src/

GDwrap.c: gd.i
	$(SWIG) $(SWIGFLAGS) -csharp -o GDwrap.c gd.i

GDwrap.o : GDwrap.c

clean:
	-rm *.cs GDwrap.c *.$(SO) *.o *.dll

