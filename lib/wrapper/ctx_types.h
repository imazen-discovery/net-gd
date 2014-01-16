
#ifndef __CTXTYPES_H
#define __CTXTYPES_H

/* Callback function pointer types. */

typedef int (*getCptr)(struct gdIOCtx *);
typedef int (*getBufPtr)(struct gdIOCtx *, void *, int);
typedef void (*putCptr)(struct gdIOCtx *, int);

typedef int (*putBufPtr)(struct gdIOCtx *, const void *, int);

typedef int (*seekPtr)(struct gdIOCtx *, const int);
typedef long (*tellPtr)(struct gdIOCtx *);

#endif
