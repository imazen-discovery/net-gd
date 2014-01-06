
MCS=mcs -sdk:4
SRC=image_test.cs
TESTS=tests.dll

test: $(TESTS)
	MONO_PATH=../lib:../lib/wrapper/ nunit-console $(TESTS)

$(TESTS): $(SRC)
	$(MCS) -t:library -r:../lib/net-gd.dll -pkg:nunit -out:$(TESTS) $(SRC)


clean:
	-rm *.dll *.xml

