
.SUFFIXES: .dll .exe

MCS=../mcs.sh
NETGD_SRC=image.cs font.cs rect.cs imode.cs image_data.cs exceptions.cs \
	affine.cs

DOCDIR=net-gd
HTMLDOCDIR=$(DOCDIR)-html

net-gd.dll: net-gd-glue.dll $(NETGD_SRC)
	$(MCS) -t:library -r:wrapper/net-gd-glue.dll -out:net-gd.dll -unsafe \
		-doc:net-gd-doc.xml \
		$(NETGD_SRC)

doc: net-gd.dll
	mdoc update -i net-gd-doc.xml -o $(DOCDIR)/en net-gd.dll
	mdoc export-html -o $(HTMLDOCDIR) $(DOCDIR)/en

net-gd-glue.dll:
	(cd wrapper && make)
	(cp wrapper/net-gd-glue.dll .)

clean:
	-rm *.exe *.dll *.so *.xml
	-rm -rf $(DOCDIR) $(HTMLDOCDIR)
	(cd wrapper && make clean)

