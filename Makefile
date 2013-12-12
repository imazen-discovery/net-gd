

hello.exe: hello.cs
	(cd wrapper; make)
	mcs -lib:wrapper/ -pkg:GD hello.cs

