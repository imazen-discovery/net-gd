#!/bin/bash

# Wrapper around mcs to make it easier to switch versions.

if [`uname -o` = 'Msys' ]; then
    export PATH="/c/progra~1/MSBuild/12.0/Bin:$PATH"
    csc $*
else
    dmcs -sdk:4 $*
    exit $?
fi
