/// <summary>
///   Basic CSharp wrappers around GD data objects.
/// </summary>

using System;

namespace GD {
  using Internal;

  public class Image : IDisposable {
    SWIGTYPE_p_gdImageStruct _img = null;
    internal SWIGTYPE_p_gdImageStruct img { get { return _img; } }

    // Constructor.
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

    public static Image createFromFile(string filename) {
      SWIGTYPE_p_gdImageStruct img = LibGD.gdImageCreateFromFile(filename);
      if (img == null) {
        return null;
      }

      return new Image(img);
    }

    public virtual void Dispose() {
      lock(this) {
        if (_img != null) {
          LibGD.gdImageDestroy(_img);
          _img = null;
        }/* if */
      }
    }
    
    public int sx { get {return LibGD.gdImage_sx_get(_img);} }
    public int sy { get {return LibGD.gdImage_sy_get(_img);} }
    public bool trueColor {
      get {return LibGD.gdImage_trueColor_get(_img) != 0; } }
    public IMode interpolation_method {
      get { return (IMode)LibGD.gdImageGetInterpolationMethod(_img); }
      set {
        LibGD.gdImageSetInterpolationMethod(_img,
                                            (gdInterpolationMethod) value);
      }
    }



    public static int majorVersion { get {return LibGD.gdMajorVersion();} }
    public static int minorVersion { get {return LibGD.gdMinorVersion();} }
    public static int releaseVersion { get {return LibGD.gdReleaseVersion();} }
    public static string extraVersion { get {return LibGD.gdExtraVersion();} }
    public static string versionString {get {return LibGD.gdVersionString();} }

    /* Fontconfig:
       These do their own locking and so should be thread-safe already.
    */

    /* Enable or disable fontconfig globally. */
    public static int useFontConfig(bool useIt) {
      return LibGD.gdFTUseFontConfig(useIt ? 1 : 0);
    }
    public static void fontCacheSetup() { LibGD.gdFontCacheSetup(); }
    public static void freeFontCache()  { LibGD.gdFreeFontCache();  }



    /*
      Sophisticated(ish) wrappers:
     */
    public bool stringFT(int color, string fontlist,
                         double ptsize, double angle, int x, int y,
                         string text) {
      string msg;
      Rect bounds;

      return stringFT(color, fontlist, ptsize, angle, x, y, text, out bounds,
                      out msg);
    }/* stringFT*/

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

    // Make the palette of this image closer to the colors in 'other',
    // which must be truecolor.
    public bool colorMatchFrom(Image other) {
      return LibGD.gdImageColorMatch(img, other.img) == 0;
    }

    // Create an exact copy of this image
    public Image clone () {
      return new Image(LibGD.gdImageClone(_img));
    }/* clone */

    public void copyFrom(Image src, int dstX, int dstY, int srcX, int srcY,
                         int w, int h) {
      LibGD.gdImageCopy(_img, src.img, dstX, dstY, srcX, srcY, w, h);
    }

    // Copy a block from src to this image.
    public void copyResizedFrom(Image src,
                                int dstX, int dstY, int srcX, int srcY,
                                int dstW, int dstH, int srcW, int srcH) {
      LibGD.gdImageCopyResized(_img, src.img, dstX, dstY, srcX, srcY, dstW,
                               dstH, srcW, srcH);
    }/* copyResizedFrom*/

    public void copyResampledFrom(Image src,
                                  int dstX, int dstY, int srcX, int srcY,
                                  int dstW, int dstH, int srcW, int srcH) {
      LibGD.gdImageCopyResampled(_img, src.img, dstX, dstY, srcX, srcY,
                                 dstW, dstH, srcW, srcH);
    }/* copyResampledFrom*/

    public Image copyGaussianBlurred(int radius, double sigma = -1.0) {
      SWIGTYPE_p_gdImageStruct newimg;

      newimg = LibGD.gdImageCopyGaussianBlurred(_img, radius, sigma);
      if (newimg == null) {
        return null;
      }/* if */

      return new Image(newimg);
    }/* copyGaussianBlurred*/

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


    /* Trivial bindings to LibGD. */
    public void setPixel(int x, int y, int color) {
      LibGD.gdImageSetPixel(_img, x, y, color); }

    public int getPixel(int x, int y) {
      return LibGD.gdImageGetPixel(_img, x, y); }

    public int getTrueColorPixel(int x, int y) {
      return LibGD.gdImageGetTrueColorPixel(_img, x, y); }

    public void line(int x1, int y1, int x2, int y2, int color) {
      LibGD.gdImageLine(_img, x1, y1, x2, y2, color); }

    public void dashedLine(int x1, int y1, int x2, int y2, int color) {
      LibGD.gdImageDashedLine(_img, x1, y1, x2, y2, color); }

    public void rectangle(int x1, int y1, int x2, int y2, int color) {
      LibGD.gdImageRectangle(_img, x1, y1, x2, y2, color); }

    public void filledRectangle(int x1, int y1, int x2, int y2,
                                       int color) {
      LibGD.gdImageFilledRectangle(_img, x1, y1, x2, y2, color); }

    public void setClip(int x1, int y1, int x2, int y2) {
      LibGD.gdImageSetClip(_img, x1, y1, x2, y2); }

    public void setResolution(uint res_x, uint res_y) {
      LibGD.gdImageSetResolution(_img, res_x, res_y); }

    public bool boundsSafe(int x, int y) {
      return LibGD.gdImageBoundsSafe(_img, x, y) != 0; }

    public int colorAllocate(int r, int g, int b) {
      return LibGD.gdImageColorAllocate(_img, r, g, b); }

    public int colorAllocateAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorAllocateAlpha(_img, r, g, b, a); }

    public int colorClosest(int r, int g, int b) {
      return LibGD.gdImageColorClosest(_img, r, g, b); }

    public int colorClosestAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorClosestAlpha(_img, r, g, b, a); }

    public int colorClosestHWB(int r, int g, int b) {
      return LibGD.gdImageColorClosestHWB(_img, r, g, b); }

    public int colorExact(int r, int g, int b) {
      return LibGD.gdImageColorExact(_img, r, g, b); }

    public int colorExactAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorExactAlpha(_img, r, g, b, a); }

    public int colorResolve(int r, int g, int b) {
      return LibGD.gdImageColorResolve(_img, r, g, b); }

    public int colorResolveAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorResolveAlpha(_img, r, g, b, a); }

    public void colorDeallocate(int color) {
      LibGD.gdImageColorDeallocate(_img, color); }

    public int trueColorToPalette(int ditherFlag, int colorsWanted) {
      return LibGD.gdImageTrueColorToPalette(_img, ditherFlag, colorsWanted); }

    public int paletteToTrueColor() {
      return LibGD.gdImagePaletteToTrueColor(_img); }

    public int trueColorToPaletteSetMethod(int method, int speed) {
      return LibGD.gdImageTrueColorToPaletteSetMethod(_img, method, speed); }

    public void trueColorToPaletteSetQuality(int min_quality,
                                                    int max_quality) {
      LibGD.gdImageTrueColorToPaletteSetQuality(_img, min_quality,max_quality);}

    public void colorTransparent(int color) {
      LibGD.gdImageColorTransparent(_img, color); }

    public int colorReplace(int src, int dst) {
      return LibGD.gdImageColorReplace(_img, src, dst); }

    public int colorReplaceThreshold(int src, int dst, float threshold){
      return LibGD.gdImageColorReplaceThreshold(_img, src, dst, threshold); }

    public bool file(string filename) {
      return LibGD.gdImageFile(_img, filename) != 0; }

    public void filledArc(int cx, int cy, int w, int h, int s, int e,
                                 int color, int style) {
      LibGD.gdImageFilledArc(_img, cx, cy, w, h, s, e, color, style); }

    public void arc(int cx, int cy, int w, int h, int s, int e,
                           int color) {
      LibGD.gdImageArc(_img, cx, cy, w, h, s, e, color); }

    public void ellipse(int cx, int cy, int w, int h, int color) {
      LibGD.gdImageEllipse(_img, cx, cy, w, h, color); }

    public void filledEllipse(int cx, int cy, int w, int h, int color) {
      LibGD.gdImageFilledEllipse(_img, cx, cy, w, h, color); }

    public void fillToBorder(int x, int y, int border, int color) {
      LibGD.gdImageFillToBorder(_img, x, y, border, color); }

    public void fill(int x, int y, int color) {
      LibGD.gdImageFill(_img, x, y, color); }

    public void setAntiAliased(int c) {
      LibGD.gdImageSetAntiAliased(_img, c); }

    public void setAntiAliasedDontBlend(int c, int dont_blend) {
      LibGD.gdImageSetAntiAliasedDontBlend(_img, c, dont_blend); }

    public void setThickness(int thickness) {
      LibGD.gdImageSetThickness(_img, thickness); }

    public void interlace(int interlaceArg) {
      LibGD.gdImageInterlace(_img, interlaceArg); }

    public void alphaBlending(int alphaBlendingArg) {
      LibGD.gdImageAlphaBlending(_img, alphaBlendingArg); }

    public void saveAlpha(int saveAlphaArg) {
      LibGD.gdImageSaveAlpha(_img, saveAlphaArg); }

    public bool pixelate(int block_size, uint mode) {
      return LibGD.gdImagePixelate(_img, block_size, mode) != 0; }

    public bool scatter(int sub, int plus) {
      return LibGD.gdImageScatter(_img, sub, plus) != 0; }

    public bool smooth(float weight) {
      return LibGD.gdImageSmooth(_img, weight) != 0; }

    public bool meanRemoval() {
      return LibGD.gdImageMeanRemoval(_img) != 0; }

    public bool emboss() {
      return LibGD.gdImageEmboss(_img) != 0; }

    public bool gaussianBlur() {
      return LibGD.gdImageGaussianBlur(_img) != 0; }

    public bool edgeDetectQuick() {
      return LibGD.gdImageEdgeDetectQuick(_img) != 0; }

    public bool selectiveBlur() {
      return LibGD.gdImageSelectiveBlur(_img) != 0; }

    public int color(int red, int green, int blue, int alpha) {
      return LibGD.gdImageColor(_img, red, green, blue, alpha); }

    public bool contrast(double contrast) {
      return LibGD.gdImageContrast(_img, contrast) != 0; }

    public bool brightness(int brightness) {
      return LibGD.gdImageBrightness(_img, brightness) != 0; }

    public bool grayScale() {
      return LibGD.gdImageGrayScale(_img) != 0; }

    public bool negate() {
      return LibGD.gdImageNegate(_img) != 0; }

    public void flipHorizontal() {
      LibGD.gdImageFlipHorizontal(_img); }

    public void flipVertical() {
      LibGD.gdImageFlipVertical(_img); }

    public void flipBoth() {
      LibGD.gdImageFlipBoth(_img); }

    public void putChar(Font f, int x, int y, char c, int color) {
      LibGD.gdImageChar(_img, f.fdata, x, y, (int)c, color); }

    public void putCharUp(Font f, int x, int y, char c, int color) {
      LibGD.gdImageCharUp(_img, f.fdata, x, y, (int) c, color); }

    public void putString(Font f, int x, int y, string s, int color) {
      LibGD.gdImageStringCharStar(_img, f.fdata, x, y, s, color); }

    public void putStringUp(Font f, int x, int y, string s, int color) {
      LibGD.gdImageStringUpCharStar(_img, f.fdata, x, y, s, color); }
  }
}


