
MCS=dmcs -sdk:4
NETGD_SRC=image.cs



net-gd.dll: net-gd-glue.dll $(NETGD_SRC)
	$(MCS) -t:library -r:wrapper/net-gd-glue.dll -out:net-gd.dll $(NETGD_SRC)

net-gd-glue.dll:
	(cd wrapper && make)

clean:
	-rm *.exe *.dll *.so
	(cd wrapper && make clean)

