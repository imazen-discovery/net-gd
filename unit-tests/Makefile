
MCS=../mcs.sh -debug+
SRC=image_test.cs
TESTS=tests.dll

all:
	(cd ..; make test)

test: $(TESTS)
	MONO_PATH=../lib:../lib/wrapper/ nunit-console $(TESTS)

$(TESTS): $(SRC)
	$(MCS) -t:library -r:../net-gd.dll -r:../net-gd-glue.dll \
		-pkg:nunit -out:$(TESTS) $(SRC)


clean:
	-rm *.dll *.xml *.png

