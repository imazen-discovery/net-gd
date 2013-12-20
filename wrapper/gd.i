%module LibGD
%{
#include "gd.h"
%}

%include "gd.h"

%inline %{
//    int gdImage_sx(gdImagePtr im) {return im->sx;}
//    int gdImage_sy(gdImagePtr im) {return im->sy;}
//    int gdImage_trueColor(gdImagePtr im) {return im->trueColor;}
%}
