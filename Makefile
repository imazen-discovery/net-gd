
NETGD_SRC=image.cs


hello.exe: hello.cs net-gd.dll
	mcs -r:net-gd.dll hello.cs

net-gd.dll:
	(cd lib; make)
	cp lib/net-gd.dll lib/wrapper/net-gd-glue.dll lib/wrapper/libGDwrap.so .

clean:
	-rm *.exe *.dll *.so
	(cd lib && make clean)

