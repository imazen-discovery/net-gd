
PATH		:= $(shell ../cfg.sh path)
MCS			:= $(shell ../cfg.sh compiler)
CSFLAGS		:= $(shell ../cfg.sh csflags_test)
SO			:= $(shell ../cfg.sh sharedobjext)
NUNIT		:= $(shell ../cfg.sh nunit)
GDROOT		:= $(shell ../cfg.sh gdpath)


SRC=image_test.cs affine_test.cs
TESTS=tests.dll


all: test
#	(cd ..; make $(TESTS))

test: localdeps $(TESTS)
	"$(NUNIT)" $(TESTS)
#	We invoke nunit-console.exe directly instead of using the script
#	because this is the only way to get the backtrace.  Original line:
#	MONO_PATH=../lib:../lib/wrapper/ nunit-console $(TESTS)

$(TESTS): $(SRC)
	$(MCS) $(CSFLAGS) -target:library	\
		-r:../net-gd.dll				\
		-r:../net-gd-glue.dll			\
		-out:$(TESTS) $(SRC)

# dotnet can't find the test framework for some reason, so we copy
# them here.  (After wasting several days trying to track down the
# reason, I'm chalking it up to some Msys or dotnet wierdness and just
# copying the files.)
localdeps:
	./fetch_deps.sh

clean:
	-rm *.dll *.xml *.png *.mdb *.a

