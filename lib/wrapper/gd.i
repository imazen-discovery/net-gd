%module LibGD
%{
#include "gd.h"

#include "gdfontg.h"
#include "gdfontl.h"
#include "gdfontmb.h"
#include "gdfonts.h"
#include "gdfontt.h"
%}

%include "arrays_csharp.i"

 // Marshal the array pointer in gdImageString{FT,FTEx,TTF} into a C# array
%apply int INOUT[] {int *brect}


%include "gd.h"
%include "gdfontg.h"
%include "gdfontl.h"
%include "gdfontmb.h"
%include "gdfonts.h"
%include "gdfontt.h"


%typemap(cstype) void (*getC)(struct gdIOCtx *) "IOCtxGet";
%typemap(imtype) void (*getC)(struct gdIOCtx *) "IOCtxGet";

%inline %{

    /* SWIG treats signed or unsigned chars as integers instead of
     * strings, so the gdImageString*() functions don't take C#
     * strings.  These wrappers fix that. */
    void gdImageStringCharStar(gdImagePtr im, gdFontPtr f, int x, int y,
                               char *s, int color) {
        gdImageString(im, f, x, y, (unsigned char *)s, color);
    }
       
    void gdImageStringUpCharStar(gdImagePtr im, gdFontPtr f, int x, int y,
                                 char *s, int color) {
        gdImageStringUp (im, f, x, y, (unsigned char *)s, color);
    }

    struct gdIOCtx *ctxGlue(int (*IOCtxGet)(struct gdIOCtx *)) {
        return NULL;
    }

%}
