
NETGD_SRC=image.cs


net-gd.dll: net-gd-glue.dll $(NETGD_SRC)
	mcs -t:library -r:wrapper/net-gd-glue.dll -out:net-gd.dll $(NETGD_SRC)

net-gd-glue.dll:
	(cd wrapper; make)
#	cp wrapper/net-gd-glue.dll .
#	cp wrapper/libGDwrap.so .

clean:
	-rm *.exe *.dll *.so
	(cd wrapper; make clean)

