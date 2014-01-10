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


 //void gdImageStringXX (gdImagePtr im, gdFontPtr f, int x, int y,
 //                      char *s, int color);



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

#if 0
    void gdImageString16CharStar(gdImagePtr im, gdFontPtr f, int x, int y,
                                 short *s, int color);
    void gdImageStringUp16CharStar(gdImagePtr im, gdFontPtr f, int x, int y,
                                   unsigned short *s, int color);
#endif

%}
