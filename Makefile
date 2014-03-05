
.SUFFIXES: .dll .zip

SO			:= $(shell ./cfg.sh sharedobjext)
LIBPF		:= $(shell ./cfg.sh lib)

ZIP=zip -9r

DOCZIP=netgd-docs.zip
DIST=net-gd.zip

all: libs examples

libs:
	(cd lib; make)
	cp lib/net-gd.dll lib/wrapper/net-gd-glue.dll lib/wrapper/$(LIBPF)GDwrap.$(SO) .
dist: libs examples
	(cd unit-tests; make $(DIST))
	cp unit-tests/$(DIST) .

doc:
	-rm $(DOCZIP)
	(cd lib; make doc; $(ZIP) ../$(DOCZIP) net-gd-html/)

test: libs
	(cd unit-tests && make test)

examples: libs
	(cd examples && make)

clean:
	-rm *.exe *.dll *.$(SO) $(DOCZIP) 
	(cd lib && make clean)
	(cd unit-tests && make clean)
