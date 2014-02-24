#!/bin/bash

# Script to fetch dependency DLLs.  (This ends up being simpler than
# monkeying with the PATH.)

set -e 

[ `uname -o` = "Msys" ] || exit 0

(ls -1 ../*.dll; ../cfg.sh test_deps) | \
while read f; do
    bf=`basename "$f"`
    [ -f "$bf" ] && continue
    echo "Fetching '$f'"
    cp "$f" .
done

if [ ! -f libgd.dll ]; then
    unzip -o "$(../cfg.sh gdpath)/libgd-win.zip"
fi



