#!/bin/bash

# Print the absolute path to the header files to use when building the
# lib.  Edit as needed.

# Path relative to this script
dir=../gd-libgd/src/

set -e

cd `dirname $0`

readlink -m "$dir"



