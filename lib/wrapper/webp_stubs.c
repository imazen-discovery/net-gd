
/* Stubs around the WebP library.  We don't support WebP on Windows
 * (and anyway, it doesn't really work in GD yet) but there's no way
 * to make Swig ignore them.  Instead, we provide minimal stubs for
 * them here.
 */

#include "gd.h"


/* Mono+Linux tolerates missing functions in a shared library if
 * they're not used, so we only need these when building under
 * Windows. */
#if defined(_WIN32) || defined(CYGWIN) || defined(_WIN32_WCE)

// Diagnostic.
#warning "****** Using WebP stubs. ******"

BGD_DECLARE(gdImagePtr) gdImageCreateFromWebp (FILE * inFile) {
    return NULL;
}/* gdImageCreateFromWebp */

BGD_DECLARE(gdImagePtr) gdImageCreateFromWebpPtr (int size, void *data) {
    return NULL;
}/* gdImageCreateFromWebpPtr */

BGD_DECLARE(gdImagePtr) gdImageCreateFromWebpCtx (gdIOCtx * infile) {
    return NULL;
}/* gdImageCreateFromWebpCtx */

BGD_DECLARE(void) gdImageWebpEx (gdImagePtr im, FILE * outFile,
                                 int quantization) {
}/* gdImageWebpEx */

BGD_DECLARE(void) gdImageWebp (gdImagePtr im, FILE * outFile) {
}/* gdImageWebp */

BGD_DECLARE(void *) gdImageWebpPtr (gdImagePtr im, int *size) {
    *size = 0;
    return NULL;
}/* gdImageWebpPtr */

BGD_DECLARE(void *) gdImageWebpPtrEx (gdImagePtr im, int *size,
                                      int quantization) {
    *size = 0;
    return NULL;
}/* gdImageWebpPtrEx */

BGD_DECLARE(void) gdImageWebpCtx (gdImagePtr im, gdIOCtx * outfile,
                                  int quantization) {
}/* gdImageWebpCtx */

#endif
