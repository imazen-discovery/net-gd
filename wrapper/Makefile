
SWIG=swig
SWIGFLAGS=-v
GD_H = ../gd-libgd/src/gd.h

wrapper.c: $(GD_H)
	$(SWIG) $(SWIGFLAGS) -csharp -module GD -o libgd-wrapper.c ../gd-libgd/src/gd.h
