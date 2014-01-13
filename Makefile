
.SUFFIXES: .dll

MCS=./mcs.sh

NETGD_SRC=image.cs

all: libs test examples

libs:
	(cd lib; make)
	cp lib/net-gd.dll lib/wrapper/net-gd-glue.dll lib/wrapper/libGDwrap.so .

test: libs
	(cd unit-tests && make test)

examples: libs
	(cd examples && make)

clean:
	-rm *.exe *.dll *.so
	(cd lib && make clean)
	(cd unit-tests && make clean)
