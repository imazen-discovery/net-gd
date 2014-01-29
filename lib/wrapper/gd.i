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

// Affine matrices are stored in arrays of double[6]
%apply double INOUT[] {double [6]}

%include "gd.h"
%include "gdfontg.h"
%include "gdfontl.h"
%include "gdfontmb.h"
%include "gdfonts.h"
%include "gdfontt.h"

// Arg for gdAffineApplyToPointF_WRAP
%apply double INOUT[] {double points[2]}


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

    /* Wrapper around gdAffineApplyToPointF so I don't have to proxy
     * the gdPointFP struct. */
    int gdAffineApplyToPointF_WRAP(double points[2], const double affine[6]) {
        gdPointF src, dst;
        int status;

        src.x = points[0];
        src.y = points[1];
        status = gdAffineApplyToPointF(&dst, &src, affine);

        points[0] = dst.x;
        points[1] = dst.y;

        return status;
    }/* gdAffineApplyToPointF_WRAP*/

    int gdTransformAffineCopy_WRAP(gdImagePtr dst, int dst_x, int dst_y,
                                   const gdImagePtr src,
                                   int src_x, int src_y, int src_w, int src_h,
                                   const double affine[6]) {
        gdRect srcR;
        srcR.x = src_x;
        srcR.y = src_y;
        srcR.width = src_w;
        srcR.height = src_h;

        return gdTransformAffineCopy(dst, dst_x, dst_y, src, &srcR, affine);
    }/* gdTransformAffineCopy_WRAP*/
%}
