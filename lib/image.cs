using System;

namespace GD {
  using Internal;

  /// <summary>
  ///   This class contains and manages GD image data.
  /// </summary>
  /// <remarks>
  ///   This wraps the gdImage structure and deletes it when the object 
  ///   is reclaimed.
  /// </remarks>
  public class Image : IDisposable {
    SWIGTYPE_p_gdImageStruct _img = null;
    internal SWIGTYPE_p_gdImageStruct img { get { return _img; } }

    
    /// <summary>
    ///   Creates a new image.
    /// </summary>
    public Image(int sx, int sy, bool truecolor = true) {
      if (truecolor) {
        _img = LibGD.gdImageCreateTrueColor(sx, sy);
      } else {
        _img = LibGD.gdImageCreate(sx, sy);
      }/* if .. else*/
    }/* Image*/

    // Internal constructor for wrapping an existing gdImage struct.
    internal Image(SWIGTYPE_p_gdImageStruct i) {
      _img = i;
    }/* Image*/

    /// <summary>
    ///   Standard C# finalizer.  Just calls Dispose().
    /// </summary>
    ~Image() {
      Dispose(false);
    }

    /// <summary>
    ///   IDispose pattern Dispose.  Deletes the underlying GD data.
    /// </summary>
    public virtual void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }/* Dispose*/

    /// <summary>
    ///   IDispose pattern Dispose(bool).  Does the actual freeing.  
    /// </summary>
    protected virtual void Dispose(bool calledFromUserCode) {
      lock(this) {
        if (_img != null) {
          LibGD.gdImageDestroy(_img);
          _img = null;
        }/* if */
      }
      // No managed resources to dispose
    }/* Dispose*/
    
    
    /// <summary>
    ///   Loads an image from the named file and returns an Image or
    ///   null on failure.  Wraps gdImageCreateFromFile().
    /// </summary>
    public static Image createFromFile(string filename) {
      SWIGTYPE_p_gdImageStruct img = LibGD.gdImageCreateFromFile(filename);
      if (img == null) {
        return null;
      }

      return new Image(img);
    }

    /// <summary> Image width. </summary>
    public int sx { get {return LibGD.gdImage_sx_get(_img);} }

    /// <summary> Image height. </summary>
    public int sy { get {return LibGD.gdImage_sy_get(_img);} }
    
    /// <summary> True if image is truecolor, false if paletted. </summary>
    public bool trueColor {
      get {return LibGD.gdImage_trueColor_get(_img) != 0; } }

    /// <summary>
    ///   Get or set interpolation method used by gdImageScale et. al.
    /// </summary>
    public IMode interpolation_method {
      get { return (IMode)LibGD.gdImageGetInterpolationMethod(_img); }
      set {
        LibGD.gdImageSetInterpolationMethod(_img,
                                            (gdInterpolationMethod) value);
      }
    }


    /// <summary> GD major version number. </summary>    
    public static int majorVersion { get {return LibGD.gdMajorVersion();} }

    /// <summary>  GD minor version number. </summary>    
    public static int minorVersion { get {return LibGD.gdMinorVersion();} }

    /// <summary> GD release number.  </summary>    
    public static int releaseVersion { get {return LibGD.gdReleaseVersion();} }

    /// <summary> GD version description (e.g. "dev")  </summary>
    public static string extraVersion { get {return LibGD.gdExtraVersion();} }

    /// <summary> Full GD version as a string  </summary>
    public static string versionString {get {return LibGD.gdVersionString();} }



    /* Fontconfig:
       These do their own locking and so should be thread-safe already.
    */

    /// <summary> Enable or disable fontconfig (gdFTUserFontConfig())</summary>
    public static int useFontConfig(bool useIt) {
      return LibGD.gdFTUseFontConfig(useIt ? 1 : 0);
    }

    /// <summary> Set up the font cache.  See gdFontCacheSetup(). </summary>
    public static void fontCacheSetup() { LibGD.gdFontCacheSetup(); }

    /// <summary> Frees the font cache.  See gdFreeFontCache(). </summary>
    public static void freeFontCache()  { LibGD.gdFreeFontCache();  }

    /// <summary> Encode the image as a PNG and return it.  </summary>
    public ImageData png(int level = -1) {
      return
        new ImageData(this,
                      (Image i, out int sz) =>
                          LibGD.gdImagePngPtrEx(i.img, out sz, level),
                      Enc.PNG);
    }/* png*/

    /// <summary> Encode the image as a GIF and return it.  </summary>
    public ImageData gif() {
      return
        new ImageData(this,
                      (Image i, out int sz) =>
                        LibGD.gdImageGifPtr(i.img, out sz),
                      Enc.GIF);
    }/* gif*/

    /// <summary> Encode the image as a GD and return it.  </summary>
    public ImageData gd() {
      return
        new ImageData(this,
                      (Image i, out int sz) => LibGD.gdImageGdPtr(i.img,out sz),
                      Enc.GD);
    }/* gd*/

    /// <summary> Encode the image as a GD2 and return it.  </summary>
    public ImageData gd2(int chunkSize = 0, bool compress = true) {
      return
        new ImageData(this,
                      (Image i, out int sz) =>
                        LibGD.gdImageGd2PtrWRAP(i.img, chunkSize,
                                                compress ? 1 : 0,
                                                out sz),
                      Enc.GD2);
    }/* gd2*/

    /// <summary> Encode the image as a JPEG and return it.  </summary>
    public ImageData jpeg(int quality = -1) {
      return
        new ImageData(this,
                      (Image i, out int sz) =>
                        LibGD.gdImageJpegPtr(i.img, out sz, quality),
                      Enc.JPEG);
    }/* jpeg*/

    /// <summary> Encode the image as a WBMP and return it.  </summary>
    public ImageData wbmp(int fg = 0) {
      return
        new ImageData(this,
                      (Image i, out int sz)
                        => LibGD.gdImageWBMPPtr(i.img, out sz, fg),
                      Enc.WBMP);
    }/* wbmp*/

    /// <summary> Encode the image as a BMP and return it.  </summary>
    public ImageData bmp(bool compression = true) {
      return
        new ImageData(this,
                      (Image i, out int sz)
                        => LibGD.gdImageBmpPtr(i.img, out sz,
                                               compression ? 1 : 0),
                      Enc.BMP);
    }/* bmp*/

    /// <summary> Encode the image as a TIFF and return it.  </summary>
    public ImageData tiff() {
      return
        new ImageData(this,
                      (Image i, out int sz)
                        => LibGD.gdImageTiffPtr(i.img, out sz),
                      Enc.TIFF);
    }/* tiff*/

    /// <summary>
    ///   Encode the image in the given format and return it.  Throws
    ///   GDinvalidFormat if the given format is not supported.
    ///   (Currently, that's only Enc.UNKNOWN).
    /// </summary>
    public ImageData encode(Enc format) {
      switch(format) {
      case Enc.GIF:  return gif();
      case Enc.GD:   return gd();
      case Enc.GD2:  return gd2();
      case Enc.WBMP: return wbmp();
      case Enc.BMP:  return bmp();
      case Enc.PNG:  return png();
      case Enc.JPEG: return jpeg();
      case Enc.TIFF: return tiff();
      default:
        throw new GDinvalidFormat();
      }/* switch*/
    }/* encode*/



    /*
      Sophisticated(ish) wrappers:
     */

    /// <summary> Simplfied wrapper for stringFT. </summary>
    public bool stringFT(int color, string fontlist,
                         double ptsize, double angle, int x, int y,
                         string text) {
      string msg;
      Rect bounds;

      return stringFT(color, fontlist, ptsize, angle, x, y, text, out bounds,
                      out msg);
    }/* stringFT*/

    /// <summary>
    ///   Draw text on the image using FreeType.  Returns true on
    ///   success; false on failure.  Wraps gdImageStringFt().
    /// </summary>
    public bool stringFT(int color, string fontlist,
                         double ptsize, double angle, int x, int y,
                         string text, out Rect bounds, out string msg) {
      var br = new int[8];
      string status;

      status = LibGD.gdImageStringFT(_img, br, color, fontlist, ptsize, angle,
                                     x, y, text);
      bounds = new Rect(br);
      msg = (status == null) ? "" : status;

      return status == null;
    }


    /// <summary>
    ///   Make the palette of this image closer to the colors in
    ///   'other', which must be truecolor.  See gdImageColorMatch().
    /// </summary>
    public bool colorMatchFrom(Image other) {
      return LibGD.gdImageColorMatch(img, other.img) == 0;
    }

    /// <summary> Create an exact copy of this image </summary>
    public Image clone () {
      return new Image(LibGD.gdImageClone(_img));
    }/* clone */

    /// <summary> Copy a rectangle in another image into this.
    /// Wraps gdImageCopy().  </summary>
    public void copyFrom(Image src, int dstX, int dstY, int srcX, int srcY,
                         int w, int h) {
      LibGD.gdImageCopy(_img, src.img, dstX, dstY, srcX, srcY, w, h);
    }

    /// <summary> Copy a block from src to this image. </summary>
    public void copyResizedFrom(Image src,
                                int dstX, int dstY, int srcX, int srcY,
                                int dstW, int dstH, int srcW, int srcH) {
      LibGD.gdImageCopyResized(_img, src.img, dstX, dstY, srcX, srcY, dstW,
                               dstH, srcW, srcH);
    }/* copyResizedFrom*/

    /// <summary> Wraps gdImageCopyResampled(). </summary>
    public void copyResampledFrom(Image src,
                                  int dstX, int dstY, int srcX, int srcY,
                                  int dstW, int dstH, int srcW, int srcH) {
      LibGD.gdImageCopyResampled(_img, src.img, dstX, dstY, srcX, srcY,
                                 dstW, dstH, srcW, srcH);
    }/* copyResampledFrom*/

    /// <summary> Wraps gdImageCopyGaussianBlurred(). <param>sigma</param> 
    /// defaults to -1.  Returns null on error. </summary>
    public Image copyGaussianBlurred(int radius, double sigma = -1.0) {
      SWIGTYPE_p_gdImageStruct newimg;

      newimg = LibGD.gdImageCopyGaussianBlurred(_img, radius, sigma);
      if (newimg == null) {
        return null;
      }/* if */

      return new Image(newimg);
    }/* copyGaussianBlurred*/

    /// <summary>
    ///   Returns a copy scaled to the new dimensions.  Wraps
    ///   gdImageScale().  The second argument defaults to 0, which
    ///   causes the operation to preserve the aspect ratio.  Returns
    ///   null on failure.
    /// </summary>
    public Image scale(uint width, uint height = 0) {
      SWIGTYPE_p_gdImageStruct newimg;

      if (height <= 0) {
        height = (uint)( (double)(width * this.sy) / (double)this.sx );
      }/* if */

      newimg = LibGD.gdImageScale(_img, width, height);
      if (newimg == null) {
        return null;
      }/* if */

      return new Image(newimg);
    }/* copyScaled*/

    /// <summary> Wraps gdImageCompare(). </summary>
    public int compare(Image other) {
      return LibGD.gdImageCompare(_img, other.img);
    }/* compare*/


    /// <summary>
    ///   Select the rectangle defined by 'region' in 'src', perform
    ///   the affine transformation described by 'affine' on it and
    ///   then copy it to this image at dst_x, dst_y.
    /// </summary>
    public bool copyAffineTransformedFrom(Image src, int dst_x, int dst_y,
                                          TrueRect region, Affine affine) {
      int status =
        LibGD.gdTransformAffineCopy_WRAP(_img, dst_x, dst_y, src.img,
                                         region.topLeft.x, region.topLeft.y,
                                         region.width, region.height,
                                         affine.matrix);
      return status != 0;
    }/* copyAffineTransformedFrom*/



    /* Trivial bindings to LibGD. */

    /// <summary> Wraps gdImageSetPixel(). </summary>
    public void setPixel(int x, int y, int color) {
      LibGD.gdImageSetPixel(_img, x, y, color); }

    /// <summary> Wraps gdImageGetPixel(). </summary>
    public int getPixel(int x, int y) {
      return LibGD.gdImageGetPixel(_img, x, y); }

    /// <summary> Wraps gdImageGetTrueColorPixel(). </summary>
    public int getTrueColorPixel(int x, int y) {
      return LibGD.gdImageGetTrueColorPixel(_img, x, y); }

    /// <summary> Wraps gdImageLine(). </summary>
    public void line(int x1, int y1, int x2, int y2, int color) {
      LibGD.gdImageLine(_img, x1, y1, x2, y2, color); }

    /// <summary> Wraps gdImageDashedLine(). </summary>
    public void dashedLine(int x1, int y1, int x2, int y2, int color) {
      LibGD.gdImageDashedLine(_img, x1, y1, x2, y2, color); }

    /// <summary> Wraps gdImageRectangle(). </summary>
    public void rectangle(int x1, int y1, int x2, int y2, int color) {
      LibGD.gdImageRectangle(_img, x1, y1, x2, y2, color); }

    /// <summary> Wraps gdImageFilledRectangle(). </summary>
    public void filledRectangle(int x1, int y1, int x2, int y2, int color) {
      LibGD.gdImageFilledRectangle(_img, x1, y1, x2, y2, color); }

    /// <summary> Wraps gdImageSetClip(). </summary>
    public void setClip(int x1, int y1, int x2, int y2) {
      LibGD.gdImageSetClip(_img, x1, y1, x2, y2); }

    /// <summary> Wraps gdImageSetResolution(). </summary>
    public void setResolution(uint res_x, uint res_y) {
      LibGD.gdImageSetResolution(_img, res_x, res_y); }

    /// <summary> Wraps gdImageBoundsSafe(). </summary>
    public bool boundsSafe(int x, int y) {
      return LibGD.gdImageBoundsSafe(_img, x, y) != 0; }

    /// <summary> Wraps gdImageColorAllocate(). </summary>
    public int colorAllocate(int r, int g, int b) {
      return LibGD.gdImageColorAllocate(_img, r, g, b); }

    /// <summary> Wraps gdImageColorAllocateAlpha(). </summary>
    public int colorAllocateAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorAllocateAlpha(_img, r, g, b, a); }

    /// <summary> Wraps gdImageColorClosest(). </summary>
    public int colorClosest(int r, int g, int b) {
      return LibGD.gdImageColorClosest(_img, r, g, b); }

    /// <summary> Wraps gdImageColorClosestAlpha(). </summary>
    public int colorClosestAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorClosestAlpha(_img, r, g, b, a); }

    /// <summary> Wraps gdImageColorClosestHWB(). </summary>
    public int colorClosestHWB(int r, int g, int b) {
      return LibGD.gdImageColorClosestHWB(_img, r, g, b); }

    /// <summary> Wraps gdImageColorExact(). </summary>
    public int colorExact(int r, int g, int b) {
      return LibGD.gdImageColorExact(_img, r, g, b); }

    /// <summary> Wraps gdImageColorExactAlpha(). </summary>
    public int colorExactAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorExactAlpha(_img, r, g, b, a); }

    /// <summary> Wraps gdImageColorResolve(). </summary>
    public int colorResolve(int r, int g, int b) {
      return LibGD.gdImageColorResolve(_img, r, g, b); }

    /// <summary> Wraps gdImageColorResolveAlpha(). </summary>
    public int colorResolveAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorResolveAlpha(_img, r, g, b, a); }

    /// <summary> Wraps gdImageColorDeallocate(). </summary>
    public void colorDeallocate(int color) {
      LibGD.gdImageColorDeallocate(_img, color); }

    /// <summary> Wraps gdImageTrueColorToPalette(). </summary>
    public int trueColorToPalette(int ditherFlag, int colorsWanted) {
      return LibGD.gdImageTrueColorToPalette(_img, ditherFlag, colorsWanted); }

    /// <summary> Wraps gdImagePaletteToTrueColor(). </summary>
    public int paletteToTrueColor() {
      return LibGD.gdImagePaletteToTrueColor(_img); }

    /// <summary> Wraps gdImageTrueColorToPaletteSetMethod(). </summary>
    public int trueColorToPaletteSetMethod(int method, int speed) {
      return LibGD.gdImageTrueColorToPaletteSetMethod(_img, method, speed); }

    /// <summary> Wraps gdImageTrueColorToPaletteSetQuality(). </summary>
    public void trueColorToPaletteSetQuality(int min_quality, int max_quality){
      LibGD.gdImageTrueColorToPaletteSetQuality(_img, min_quality,max_quality);}

    /// <summary> Wraps gdImageColorTransparent(). </summary>
    public void colorTransparent(int color) {
      LibGD.gdImageColorTransparent(_img, color); }

    /// <summary> Wraps gdImageColorReplace(). </summary>
    public int colorReplace(int src, int dst) {
      return LibGD.gdImageColorReplace(_img, src, dst); }

    /// <summary> Wraps gdImageColorReplaceThreshold(). </summary>
    public int colorReplaceThreshold(int src, int dst, float threshold){
      return LibGD.gdImageColorReplaceThreshold(_img, src, dst, threshold); }

    /// <summary> Wraps gdImageFile(). </summary>
    public bool file(string filename) {
      return LibGD.gdImageFile(_img, filename) != 0; }

    /// <summary> Wraps gdImageFilledArc(). </summary>
    public void filledArc(int cx, int cy, int w, int h, int s, int e,
                          int color, int style) {
      LibGD.gdImageFilledArc(_img, cx, cy, w, h, s, e, color, style); }

    /// <summary> Wraps gdImageArc(). </summary>
    public void arc(int cx, int cy, int w, int h, int s, int e, int color) {
      LibGD.gdImageArc(_img, cx, cy, w, h, s, e, color); }

    /// <summary> Wraps gdImageEllipse(). </summary>
    public void ellipse(int cx, int cy, int w, int h, int color) {
      LibGD.gdImageEllipse(_img, cx, cy, w, h, color); }

    /// <summary> Wraps gdImageFilledEllipse(). </summary>
    public void filledEllipse(int cx, int cy, int w, int h, int color) {
      LibGD.gdImageFilledEllipse(_img, cx, cy, w, h, color); }

    /// <summary> Wraps gdImageFillToBorder(). </summary>
    public void fillToBorder(int x, int y, int border, int color) {
      LibGD.gdImageFillToBorder(_img, x, y, border, color); }

    /// <summary> Wraps gdImageFill(). </summary>
    public void fill(int x, int y, int color) {
      LibGD.gdImageFill(_img, x, y, color); }

    /// <summary> Wraps gdImageSetAntiAliased(). </summary>
    public void setAntiAliased(int c) {
      LibGD.gdImageSetAntiAliased(_img, c); }

    /// <summary> Wraps gdImageSetAntiAliasedDontBlend(). </summary>
    public void setAntiAliasedDontBlend(int c, int dont_blend) {
      LibGD.gdImageSetAntiAliasedDontBlend(_img, c, dont_blend); }

    /// <summary> Wraps gdImageSetThickness(). </summary>
    public void setThickness(int thickness) {
      LibGD.gdImageSetThickness(_img, thickness); }

    /// <summary> Wraps gdImageInterlace(). </summary>
    public void interlace(int interlaceArg) {
      LibGD.gdImageInterlace(_img, interlaceArg); }

    /// <summary> Wraps gdImageAlphaBlending(). </summary>
    public void alphaBlending(int alphaBlendingArg) {
      LibGD.gdImageAlphaBlending(_img, alphaBlendingArg); }

    /// <summary> Wraps gdImageSaveAlpha(). </summary>
    public void saveAlpha(int saveAlphaArg) {
      LibGD.gdImageSaveAlpha(_img, saveAlphaArg); }

    /// <summary> Wraps gdImagePixelate(). </summary>
    public bool pixelate(int block_size, uint mode) {
      return LibGD.gdImagePixelate(_img, block_size, mode) != 0; }

    /// <summary> Wraps gdImageScatter(). </summary>
    public bool scatter(int sub, int plus) {
      return LibGD.gdImageScatter(_img, sub, plus) != 0; }

    /// <summary> Wraps gdImageSmooth(). </summary>
    public bool smooth(float weight) {
      return LibGD.gdImageSmooth(_img, weight) != 0; }

    /// <summary> Wraps gdImageMeanRemoval(). </summary>
    public bool meanRemoval() {
      return LibGD.gdImageMeanRemoval(_img) != 0; }

    /// <summary> Wraps gdImageEmboss(). </summary>
    public bool emboss() {
      return LibGD.gdImageEmboss(_img) != 0; }

    /// <summary> Wraps gdImageGaussianBlur(). </summary>
    public bool gaussianBlur() {
      return LibGD.gdImageGaussianBlur(_img) != 0; }

    /// <summary> Wraps gdImageEdgeDetectQuick(). </summary>
    public bool edgeDetectQuick() {
      return LibGD.gdImageEdgeDetectQuick(_img) != 0; }

    /// <summary> Wraps gdImageSelectiveBlur(). </summary>
    public bool selectiveBlur() {
      return LibGD.gdImageSelectiveBlur(_img) != 0; }

    /// <summary> Wraps gdImageColor(). </summary>
    public int color(int red, int green, int blue, int alpha) {
      return LibGD.gdImageColor(_img, red, green, blue, alpha); }

    /// <summary> Wraps gdImageContrast(). </summary>
    public bool contrast(double contrast) {
      return LibGD.gdImageContrast(_img, contrast) != 0; }

    /// <summary> Wraps gdImageBrightness(). </summary>
    public bool brightness(int brightness) {
      return LibGD.gdImageBrightness(_img, brightness) != 0; }

    /// <summary> Wraps gdImageGrayScale(). </summary>
    public bool grayScale() {
      return LibGD.gdImageGrayScale(_img) != 0; }

    /// <summary> Wraps gdImageNegate(). </summary>
    public bool negate() {
      return LibGD.gdImageNegate(_img) != 0; }

    /// <summary> Wraps gdImageFlipHorizontal(). </summary>
    public void flipHorizontal() {
      LibGD.gdImageFlipHorizontal(_img); }

    /// <summary> Wraps gdImageFlipVertical(). </summary>
    public void flipVertical() {
      LibGD.gdImageFlipVertical(_img); }

    /// <summary> Wraps gdImageFlipBoth(). </summary>
    public void flipBoth() {
      LibGD.gdImageFlipBoth(_img); }

    /// <summary> Wraps gdImageChar(). </summary>
    public void putChar(Font f, int x, int y, char c, int color) {
      LibGD.gdImageChar(_img, f.fdata, x, y, (int)c, color); }

    /// <summary> Wraps gdImageCharUp(). </summary>
    public void putCharUp(Font f, int x, int y, char c, int color) {
      LibGD.gdImageCharUp(_img, f.fdata, x, y, (int) c, color); }

    /// <summary> Wraps gdImageStringCharStar(). </summary>
    public void putString(Font f, int x, int y, string s, int color) {
      LibGD.gdImageStringCharStar(_img, f.fdata, x, y, s, color); }

    /// <summary> Wraps gdImageStringUpCharStar(). </summary>
    public void putStringUp(Font f, int x, int y, string s, int color) {
      LibGD.gdImageStringUpCharStar(_img, f.fdata, x, y, s, color); }
  }
}


