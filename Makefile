
.SUFFIXES: .dll

SO			:= $(shell ./cfg.sh sharedobjext)
LIBPF		:= $(shell ./cfg.sh lib)

DOCDIR=html-docs/

all: libs test examples

libs:
	(cd lib; make)
	cp lib/net-gd.dll lib/wrapper/net-gd-glue.dll lib/wrapper/$(LIBPF)GDwrap.$(SO) .

doc:
	(cd lib; make doc)
	mkdir $(DOCDIR)
	cp -a lib/net-gd-html/* $(DOCDIR)

test: libs
	(cd unit-tests && make test)

examples: libs
	(cd examples && make)

clean:
	-rm *.exe *.dll *.$(SO)
	-rm -rf $(DOCDIR)
	(cd lib && make clean)
	(cd unit-tests && make clean)
