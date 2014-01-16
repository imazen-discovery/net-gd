
#ifndef __CTX_H
#define __CTX_H

#include "ctx_types.h"

struct gdIOCtx *newCtx(int tag, getCptr gcp, getBufPtr gbp, putCptr pcp,
                       putBufPtr pbp, seekPtr sp, tellPtr tp);
int getTag(struct gdIOCtx *ctx);

#endif
