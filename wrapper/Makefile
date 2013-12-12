
GDPATH = ../../gd-libgd/src/

SWIG=swig
SWIGFLAGS=-v -I$(GDPATH)


all: libgd-wrapper.so

libgd-wrapper.so : libgd-wrapper.c
	gcc -fPIC -g -c -Wall libgd-wrapper.c
	gcc -shared -Wl,-soname,libgd-wrapper.so -o libgd-wrapper.so libgd-wrapper.o -lc

libgd-wrapper.c: gd.i
	$(SWIG) $(SWIGFLAGS) -csharp -o libgd-wrapper.c gd.i

clean:
	-rm *.cs *.c *.so *.o


