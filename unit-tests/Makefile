
MCS := $(shell ../cfg.sh compiler)
SRC=image_test.cs affine_test.cs
TESTS=tests.dll

all:
	(cd ..; make test)

test: $(TESTS)
	MONO_PATH=.. \
		mono --debug /usr/lib/nunit/nunit-console.exe -config=Debug tests.dll
#	We invoke nunit-console.exe directly instead of using the script
#	because this is the only way to get the backtrace.  Original line:
#	MONO_PATH=../lib:../lib/wrapper/ nunit-console $(TESTS)

$(TESTS): $(SRC)
	$(MCS) -t:library -r:../net-gd.dll -r:../net-gd-glue.dll \
		-pkg:nunit -out:$(TESTS) $(SRC)


clean:
	-rm *.dll *.xml *.png *.mdb

