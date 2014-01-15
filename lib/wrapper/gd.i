%module LibGD
%{
#include "gd.h"

#include "gdfontg.h"
#include "gdfontl.h"
#include "gdfontmb.h"
#include "gdfonts.h"
#include "gdfontt.h"

#include "fn_ptrs.h"
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

%include "fn_ptrs.h"

/* %typemap(csin) int (*)( int ) "$csinput"; */
/* %typemap(cstype) int (*)( int ) "GetGlueFn"; */
/* %typemap(imtype) int (*)( int ) "GetGlueFn"; */

/* %pragma(csharp) moduleimports = */
/* %{ */
/*      public delegate int getCdelegate(SWIGTYPE_p_gdIOCtx ptr); */
/* %} */

%typemap(csin)   getCptr "GD.Internal.getCdelegate";
%typemap(cstype) getCptr "GD.Internal.getCdelegate";
%typemap(imtype) getCptr "GD.Internal.getCdelegate";

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


    struct gdIOCtx *newCtx(getCptr cp) {
        struct gdIOCtx *result;

        result = calloc(1, sizeof(struct gdIOCtx));
        if (!result) return NULL;

        result->getC = cp;

        return result;
    }/* newCtx*/

    int blammo(struct gdIOCtx *ctx) {
        return 42;
    }/* foo*/


    /* typedef int (*GetGluePtr)(int); */
    /* int ctxGlue(GetGluePtr fn) {return fn(42);} */

/*
    BGD_DECLARE(struct gdIOCtx *)ctxGlue(int (*IOCtxGet)(  int)) {//struct gdIOCtx *)) {
        return NULL;
    }
*/
%}


