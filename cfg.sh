#!/bin/bash

# Script to encapsulate all of the config information.  Think of this
# as a bad, hacky reinvention of pkg-config.


#
# Edit these as necessary:
#

# ID of the platform we're running on.  Override if your uname does
# the wrong thing.
PLATFORM="`uname -o`"

# Path to your .net compiler on Windows
DOTNET_PATH="/c/progra~1/MSBuild/12.0"

# Name+args of your preferred Mono compiler
MONO_MCS='dmcs -sdk:4'

# Path (relative to the location of this script) of the directory
# containing the GD source tree we're using.
GD_DIR=../gd-libgd/

# Path to the installation directory of nunit on Windows.  Needs to be
# a Windows-style filename.
WIN_NUNIT_PATH='C:\\Program Files\\NUnit 2.6.3'

# Path to nunit-console on Linux.  (It's safe to just put
# 'nunit-console' here.  However, this is a wrapper script on most
# *nixes and on some (e.g. Ubuntu 12.04), it breaks the
# filename/line-number reporting in the backtrace.  This command
# bypasses that and fixes the problem.)
LINUX_NUNIT_PATH='mono --debug /usr/lib/nunit/nunit-console.exe -config=Debug'

# ---------------------------------------------------------------------------

# MSys doesn't have cygpath, so I need to do this idiocy:
WIN_NUNIT_PATH_MSYS=$(echo $WIN_NUNIT_PATH | \
    perl -npe 's{^(\w)\:}{/\L$1\E/}g; s{\\}{/}g; s{/+}{/}g')


# Set per-platform values
if [ "$PLATFORM" = 'Msys' ]; then
    TEST_DEPS=$(find "$WIN_NUNIT_PATH_MSYS" -name '*.dll')
    PATHVAL="$DOTNET_PATH/Bin:$PATH"
    MCS='csc -unsafe -checked- -nologo -platform:x86'
#    MCS_TEST_FLAGS="-r:nunit.core.dll -r:nunit.util.dll -r:nunit.framework.dll -r:nunit.core.interfaces.dll -lib:\"$WIN_NUNIT_PATH\\bin\\lib\" -lib:\"$WIN_NUNIT_PATH\\bin\\framework\" "
    MCS_TEST_FLAGS="
-r:\"$WIN_NUNIT_PATH\\bin\\lib\\nunit.core.dll\"
-r:\"$WIN_NUNIT_PATH\\bin\\lib\\nunit.util.dll\"
-r:\"$WIN_NUNIT_PATH\\bin\\framework\\nunit.framework.dll\"
-r:\"$WIN_NUNIT_PATH\\bin\\lib\\nunit.core.interfaces.dll\"
"
    NUNIT="\"$WIN_NUNIT_PATH\\bin\\nunit-console.exe\""
    SO=dll
    CFLAGS='-g -Wall'
    LIB_PFX=""
    SO_LDFLAGS='-Wl,--add-stdcall-alias,--kill-at,--export-all-symbols'
elif [ "$PLATFORM" = "GNU/Linux" ]; then
    PATHVAL="$PATH"
    MCS=$MONO_MCS
    MCS_TEST_FLAGS="-pkg:nunit"   # Yay, pkg-config!
    NUNIT="MONO_PATH=.. $LINUX_NUNIT_PATH"
    SO=so
    CFLAGS='-fPIC -g -Wall'
    LIB_PFX="lib"
    TEST_DEPS=""
    SO_LDFLAGS=''
else
    echo "Unknown platform: '$PLATFORM'.  Edit cfg.sh to match your platform."
    exit 1
fi

# Figure out what the caller wants and output it
case $* in
    path)
        # the required PATH
        echo $PATHVAL
        ;;
    compiler)
        # Name and default args of the C# compiler
        echo $MCS
        ;;
    csflags_test)
        # C# compile command for compiling unit tests
        echo $MCS_TEST_FLAGS
        ;;
    gdpath)
        # Path to the GD installation.
        (cd `dirname $0`; readlink -m "$GD_DIR")
        ;;
    nunit)
        # Command-line prefix for the nunit runner
        echo $NUNIT
        ;;
    sharedobjext)
        # Extension for shared object file (.so or .dll)
        echo $SO
        ;;
    cflags)
        # C compiler flags for building the wrapper shared lib
        echo $CFLAGS
        ;;
    lib)
        # Required prefix for library name
        echo $LIB_PFX
        ;;
    test_deps)
        # Files to copy into the test directory.
        echo "$TEST_DEPS"
        ;;
    so_ldflags)
        # Extra linker options
        echo "$SO_LDFLAGS"
        ;;
    *)
        echo "Invalid argument."
        exit 1
        ;;
esac

exit 0
