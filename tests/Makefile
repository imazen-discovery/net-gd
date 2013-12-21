
SRC=image_test.cs
TESTS=tests.dll

test: $(TESTS)
#	nunit-console 

$(TESTS): $(SRC)
	mcs -t:library -r:../lib/net-gd.dll -pkg:nunit -out:$(TESTS) $(SRC)


clean:
	-rm *.dll *.xml

