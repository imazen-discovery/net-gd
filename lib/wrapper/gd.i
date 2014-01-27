%module LibGD
%{
#include "gd.h"

#include "gdfontg.h"
#include "gdfontl.h"
#include "gdfontmb.h"
#include "gdfonts.h"
#include "gdfontt.h"
%}

%include arrays_csharp.i
%include typemaps.i

// Marshal the array pointer in gdImageString{FT,FTEx,TTF} into a C#
// array.
//
// Note: renaming the argument in gd.h will break this.  If this
// happens, the best option is probably to write a wrapper function
// below and bind to that.
%apply int INOUT[] {int *brect}

// Marshal size outputs in gdImage*Ptr
%apply int *OUTPUT{int *size}

// Void pointers should be turned int IntPtr
%apply void *VOID_INT_PTR { void * }

%include "gd.h"
%include "gdfontg.h"
%include "gdfontl.h"
%include "gdfontmb.h"
%include "gdfonts.h"
%include "gdfontt.h"

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

    /* Wrapper around gdImageGd2Ptr which uses a boolean for its
     * second argument.  (There are only two choices--compressed and
     * not--and the alternative is to import #define's from gd.h.) */
    void *gdImageGd2PtrWRAP (gdImagePtr im, int cs, int compress, int *size) {
        return gdImageGd2Ptr(im, cs,
                             compress ? GD2_FMT_COMPRESSED : GD2_FMT_RAW,
                             size);
    }/* gdImageGd2PtrWRAP */


%}
