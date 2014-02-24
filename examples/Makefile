
.SUFFIXES: .dll

PATH		:= $(shell ../cfg.sh path)
GDPATH		:= $(shell ../cfg.sh gdpath)
MCS			:= $(shell ../cfg.sh compiler)
SO			:= $(shell ../cfg.sh sharedobjext)
LIBPF		:= $(shell ../cfg.sh lib)

all: libs bins

bins: hello.exe

run: bins
	(export PATH="$(PATH):$(GDPATH)/src/"; echo $$PATH) #./hello.exe)

hello.exe: hello.cs
	$(MCS) -r:net-gd.dll hello.cs

libs:
	(cd ../lib && make)
	cp  ../lib/net-gd.dll ../lib/wrapper/net-gd-glue.dll \
		../lib/wrapper/$(LIBPF)GDwrap.$(SO) .

clean:
	-rm *.exe *.dll *.$(SO)
