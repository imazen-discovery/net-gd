
MCS=../mcs.sh
NETGD_SRC=image.cs font.cs rect.cs imode.cs image_data.cs exceptions.cs

net-gd.dll: net-gd-glue.dll $(NETGD_SRC)
	$(MCS) -t:library -r:wrapper/net-gd-glue.dll -out:net-gd.dll -unsafe \
		$(NETGD_SRC)

net-gd-glue.dll:
	(cd wrapper && make)

clean:
	-rm *.exe *.dll *.so
	(cd wrapper && make clean)

