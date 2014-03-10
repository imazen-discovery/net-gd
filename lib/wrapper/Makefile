
GDPATH		:= $(shell ../../cfg.sh gdpath)/src
PATH		:= $(shell ../../cfg.sh path)
SO			:= $(shell ../../cfg.sh sharedobjext)
CF_EXTRA	:= $(shell ../../cfg.sh cflags)
MCS			:= $(shell ../../cfg.sh compiler)
LIBPF		:= $(shell ../../cfg.sh lib)
SO_LDFLAGS	:= $(shell ../../cfg.sh so_ldflags)

CC=gcc
CFLAGS=-I$(GDPATH) $(CF_EXTRA)

LD=gcc
LDFLAGS=-shared $(SO_LDFLAGS) -Wl,-soname,GDwrap.$(SO) 
LIBS= -lgd -L ../../../gd-libgd/src/

SWIG=swig
SWIGFLAGS=-v -Wall -I$(GDPATH) -dllimport $(LIBPF)GDwrap.$(SO) \
	-namespace GD.Internal -noproxy

.SUFFIXES: .i .dll .exe

all: $(LIBPF)GDwrap.$(SO) net-gd-glue.dll

net-gd-glue.dll: $(LIBPF)GDwrap.$(SO)
	$(MCS) -t:library -out:net-gd-glue.dll *.cs

$(LIBPF)GDwrap.$(SO) : GDwrap.o
	$(LD) $(LDFLAGS) -o $(LIBPF)GDwrap.$(SO) GDwrap.o $(LIBS)

GDwrap.c: gd.i
	$(SWIG) $(SWIGFLAGS) -csharp -o GDwrap.c gd.i

GDwrap.o : GDwrap.c

clean:
	-rm *.cs GDwrap.c *.$(SO) *.o *.dll

