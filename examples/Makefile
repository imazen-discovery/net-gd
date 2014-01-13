
.SUFFIXES: .dll

MCS=../mcs.sh

all: libs bins

bins: hello.exe

hello.exe: hello.cs
	$(MCS) -r:net-gd.dll hello.cs

libs:
	(cd ../lib && make)
	cp ../lib/net-gd.dll ../lib/wrapper/net-gd-glue.dll ../lib/wrapper/libGDwrap.so .

clean:
	-rm *.exe *.dll *.so
