
#include <stdlib.h>

#include "gd.h"

#include "ctx.h"

#if 0
static void
ctx_free(struct gdIOCtx *ptr) {
    free(ptr);
}/* ctx_free*/
#endif

struct gdIOCtx *
newCtx(int tag, getCptr gcp, getBufPtr gbp, putCptr pcp,
       putBufPtr pbp, seekPtr sp, tellPtr tp) {
    struct gdIOCtx *result;
    
    result = calloc(1, sizeof(struct gdIOCtx));
    if (!result) return NULL;
    
    result->getC = gcp;
    result->getBuf = gbp;
    result->putC = pcp;
    result->putBuf = pbp;
    result->seek = sp;
    result->tell = tp;

    result->gd_free = (void (*)(struct gdIOCtx *)) free;
    
    result->data = (void*) (long)tag;

    return result;
}/* newCtx*/


int
getTag(struct gdIOCtx *ctx) {
    return (int)(long)ctx->data;
}/* getTag*/
