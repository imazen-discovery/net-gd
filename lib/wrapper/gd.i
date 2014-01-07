%module LibGD
%{
#include "gd.h"

#include "gdfontg.h"
#include "gdfontl.h"
#include "gdfontmb.h"
#include "gdfonts.h"
#include "gdfontt.h"
%}

%include "gd.h"
%include "gdfontg.h"
%include "gdfontl.h"
%include "gdfontmb.h"
%include "gdfonts.h"
%include "gdfontt.h"

%inline %{
//    int gdImage_sx(gdImagePtr im) {return im->sx;}
//    int gdImage_sy(gdImagePtr im) {return im->sy;}
//    int gdImage_trueColor(gdImagePtr im) {return im->trueColor;}
%}
