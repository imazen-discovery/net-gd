
PATH		:= $(shell ../cfg.sh path)
MCS			:= $(shell ../cfg.sh compiler)
CSFLAGS		:= $(shell ../cfg.sh csflags_test)	
SO			:= $(shell ../cfg.sh sharedobjext)
NUNIT		:= $(shell ../cfg.sh nunit)

SRC=image_test.cs affine_test.cs
TESTS=tests.dll

all:
	(cd ..; make $(TESTS))

test: $(TESTS)
	"$(NUNIT)" $(TESTS)
#	We invoke nunit-console.exe directly instead of using the script
#	because this is the only way to get the backtrace.  Original line:
#	MONO_PATH=../lib:../lib/wrapper/ nunit-console $(TESTS)

$(TESTS): $(SRC)
	$(MCS) $(CSFLAGS) -target:library	\
		-r:../net-gd.dll				\
		-r:../net-gd-glue.dll			\
		-out:$(TESTS) $(SRC)

clean:
	-rm *.dll *.xml *.png *.mdb

