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

# ---------------------------------------------------------------------------

NUNIT_DIR=/usr/lib/nunit/nunit-console.exe

# Set per-platform values
if [ "$PLATFORM" = 'Msys' ]; then
    PATHVAL="$DOTNET_PATH/Bin:$PATH"
#    MCS=csc
    MCS='csc -unsafe -checked-'
    NUNIT="$NUNIT_DIR"  # probably doesn't work
    SO=dll
    CFLAGS='-g -Wall'
    LIB_PFX=""
elif [ "$PLATFORM" = "GNU/Linux" ]; then
    PATHVAL="$PATH"
    MCS="$MONO_MCS"
    NUNIT="MONO_PATH=.. mono --debug $NUNIT_DIR"
    SO=so
    CFLAGS='-fPIC -g -Wall'
    LIB_PFX="lib"
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
    *)
        echo "Invalid argument."
        exit 1
        ;;
esac

exit 0
