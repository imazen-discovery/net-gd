

hello.exe: hello.cs
	(cd wrapper; make)
	mcs -r:wrapper/net-gd.dll hello.cs

clean:
	rm *.exe

