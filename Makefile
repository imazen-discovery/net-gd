
.SUFFIXES: .dll

MCS=./mcs.sh

DOCDIR=html-docs/

all: libs test examples

libs:
	(cd lib; make)
	cp lib/net-gd.dll lib/wrapper/net-gd-glue.dll lib/wrapper/libGDwrap.so .

doc:
	(cd lib; make doc)
	mkdir $(DOCDIR)
	cp -a lib/net-gd-html/* $(DOCDIR)

test: libs
	(cd unit-tests && make test)

examples: libs
	(cd examples && make)

clean:
	-rm *.exe *.dll *.so
	-rm -rf $(DOCDIR)
	(cd lib && make clean)
	(cd unit-tests && make clean)
