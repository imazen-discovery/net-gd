

hello.exe: hello.cs
	(cd wrapper; make)
	mcs -r:wrapper/net-gd.dll hello.cs
#	mcs -r:wrapper/net-gd.dll -r:wrapper/libgd-wrapper.so hello.cs

clean:
	rm *.exe

