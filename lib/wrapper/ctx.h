
#ifndef __CTX_H
#define __CTX_H

/* Callback function pointer types. */

typedef int (*getCptr)(struct gdIOCtx *);
typedef int (*getBufPtr)(struct gdIOCtx *, void *, int);
typedef void (*putCptr)(struct gdIOCtx *, int);

typedef int (*putBufPtr)(struct gdIOCtx *, const void *, int);

typedef int (*seekPtr)(struct gdIOCtx *, const int);
typedef long (*tellPtr)(struct gdIOCtx *);

struct gdIOCtx *newCtx(int tag, getCptr gcp, getBufPtr gbp, putCptr pcp,
                       putBufPtr pbp, seekPtr sp, tellPtr tp);
int getTag(struct gdIOCtx *ctx);

#endif
