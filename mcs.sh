#!/bin/bash

# Wrapper around mcs to make it easier to switch versions.

dmcs -sdk:4 $*
exit $?
