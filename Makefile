
export MCS=dmcs -sdk:4

NETGD_SRC=image.cs


hello.exe: hello.cs net-gd.dll
	$(MCS) -r:net-gd.dll hello.cs

net-gd.dll:
	(cd lib; make)
	cp lib/net-gd.dll lib/wrapper/net-gd-glue.dll lib/wrapper/libGDwrap.so .

test: net-gd.dll
	(cd unit-tests && make test)

clean:
	-rm *.exe *.dll *.so
	(cd lib && make clean)
	(cd unit-tests && make clean)
