
.SUFFIXES: .dll

PATH		:= $(shell ../cfg.sh path)
MCS			:= $(shell ../cfg.sh compiler)
SO			:= $(shell ../cfg.sh sharedobjext)
LIBPF		:= $(shell ../cfg.sh lib)

all: libs bins

bins: hello.exe

hello.exe: hello.cs
	$(MCS) -r:net-gd.dll hello.cs

libs:
	(cd ../lib && make)
	cp  ../lib/net-gd.dll ../lib/wrapper/net-gd-glue.dll \
		../lib/wrapper/$(LIBPF)GDwrap.$(SO) .

clean:
	-rm *.exe *.dll *.$(SO)
