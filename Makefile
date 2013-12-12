

hello.exe: hello.cs net-gd.dll
	(cd wrapper; make)
	mcs -r:wrapper/net-gd.dll hello.cs

net-gd.dll:
	(cd wrapper; make)
	cp wrapper/net-gd.dll .
	cp wrapper/libGDwrap.so .

clean:
	rm *.exe *.dll
	(cd wrapper; make clean)

