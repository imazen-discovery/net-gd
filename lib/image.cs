/// <summary>
///   Basic CSharp wrappers around GD data objects.
/// </summary>

using System;

namespace GD {
  using Internal;

  public enum IMode {
    Default             = gdInterpolationMethod.GD_DEFAULT,
    Bell                = gdInterpolationMethod.GD_BELL,
    Bessel              = gdInterpolationMethod.GD_BESSEL,
    BilinearFixed       = gdInterpolationMethod.GD_BILINEAR_FIXED,
    Bicubic             = gdInterpolationMethod.GD_BICUBIC,
    BicubicFixed        = gdInterpolationMethod.GD_BICUBIC_FIXED,
    Blackman            = gdInterpolationMethod.GD_BLACKMAN,
    Box                 = gdInterpolationMethod.GD_BOX,
    Bspline             = gdInterpolationMethod.GD_BSPLINE,
    Catmullrom          = gdInterpolationMethod.GD_CATMULLROM,
    Gaussian            = gdInterpolationMethod.GD_GAUSSIAN,
    GeneralizedCubic    = gdInterpolationMethod.GD_GENERALIZED_CUBIC,
    Hermite             = gdInterpolationMethod.GD_HERMITE,
    Hamming             = gdInterpolationMethod.GD_HAMMING,
    Hanning             = gdInterpolationMethod.GD_HANNING,
    Mitchell            = gdInterpolationMethod.GD_MITCHELL,
    NearestNeighbour    = gdInterpolationMethod.GD_NEAREST_NEIGHBOUR,
    Power               = gdInterpolationMethod.GD_POWER,
    Quadratic           = gdInterpolationMethod.GD_QUADRATIC,
    Sinc                = gdInterpolationMethod.GD_SINC,
    Triangle            = gdInterpolationMethod.GD_TRIANGLE,
    //Weighted4         = gdInterpolationMethod.GD_WEIGHTED4,   // broken
  }

  class GDFailure : Exception {
    public GDFailure(string message) : base("Null value returned by " + message) {}
  }


  public class Font {
    private SWIGTYPE_p_gdFont _fdata;
    internal SWIGTYPE_p_gdFont fdata { get {return _fdata; } }

    private Font(SWIGTYPE_p_gdFont fontData) {
      _fdata = fontData;
    }/* Font*/

    public int w { get {return LibGD.gdFont_w_get(_fdata); } }
    public int h { get {return LibGD.gdFont_h_get(_fdata); } }


    // TODO: add support for custom fonts.

    static private bool fontsInit = false;
    static private Font _tiny, _small, _mediumBold, _large, _giant;

    private static void _initFonts() {
      if (fontsInit) return;

      _tiny       = new Font(LibGD.gdFontGetTiny());
      _small      = new Font(LibGD.gdFontGetSmall());
      _mediumBold = new Font(LibGD.gdFontGetMediumBold());
      _large      = new Font(LibGD.gdFontGetLarge());
      _giant      = new Font(LibGD.gdFontGetGiant());

      fontsInit = true;
    }

    public static Font tiny       { get {_initFonts(); return _tiny;} }
    public static Font small      { get {_initFonts(); return _small;} }
    public static Font mediumBold { get {_initFonts(); return _mediumBold;} }
    public static Font large      { get {_initFonts(); return _large;} }
    public static Font giant      { get {_initFonts(); return _giant;} }
  }

  public class Point {
    private int _x, _y;
    public int x { get {return _x;} }
    public int y { get {return _y;} }

    public Point(int x, int y) {
      _x = x;
      _y = y;
    }
  }

  // Not actually guaranteed to be a rectangle.
  public class Rect {
    protected Point _bottomLeft, _bottomRight,_topRight, _topLeft;
    public Point bottomLeft  { get {return _bottomLeft; } }
    public Point bottomRight { get {return _bottomRight; } }
    public Point topLeft     { get {return _topLeft; } }
    public Point topRight    { get {return _topRight; } }

    public Rect(int[] pts) {
      _bottomLeft  = new Point(pts[0], pts[1]);
      _bottomRight = new Point(pts[2], pts[3]);
      _topRight    = new Point(pts[4], pts[5]);
      _topLeft     = new Point(pts[6], pts[7]);
    }

    public Rect(Point topLeft, Point topRight, Point bottomLeft,
                     Point bottomRight) {
      _topLeft = topLeft;
      _topRight = topRight;
      _bottomLeft = bottomLeft;
      _bottomRight = bottomRight;
    }
  }

  // A rectangle guaranteed to have all of its edges be horizontal or
  // vertical.
  public class TrueRect : Rect {
    public TrueRect(Point topLeft, Point bottomRight) 
    :  base (topLeft,
             new Point(bottomRight.x, topLeft.y),
             new Point(topLeft.x, bottomRight.y),
             bottomRight) { }

    public TrueRect(int tlx, int tly, int brx, int bry)
    : base (new Point (tlx, tly),
            new Point (brx, tly),
            new Point (tlx, bry),
            new Point (brx, bry)) {}

    public int width  { get {return _bottomRight.x - _topLeft.x;} }
    public int height { get {return _bottomLeft.y  - _topLeft.y;} }
  }



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

    // Private constructor for wrapping an existing gdImage struct.
    private Image(SWIGTYPE_p_gdImageStruct i) {
      _img = i;
    }/* Image*/

    private static SWIGTYPE_p_gdImageStruct chkptr(SWIGTYPE_p_gdImageStruct ptr,
                                                   string apiFunc) {
      if (ptr != null) return ptr;
      throw new GDFailure(apiFunc);
    }/* chkptr*/

    public static Image createFromFile(string filename) {
      return new Image(chkptr(LibGD.gdImageCreateFromFile(filename),
                              "gdImageCreateFromFile"));
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

    public static int majorVersion { get {return LibGD.gdMajorVersion();} }
    public static int minorVersion { get {return LibGD.gdMinorVersion();} }
    public static int releaseVersion { get {return LibGD.gdReleaseVersion();} }
    public static string extraVersion { get {return LibGD.gdExtraVersion();} }
    public static string versionString {get {return LibGD.gdVersionString();} }

    /* Enable or disable fontconfig globally. */
    private static object _lock = new object(); 
    public static int useFontConfig(bool useIt) {
      lock (_lock) {
        return LibGD.gdFTUseFontConfig(useIt ? 1 : 0);
      }
    }


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


    public void copyFrom(Image src, int dstX, int dstY, int srcX, int srcY,
                         int w, int h) {
      LibGD.gdImageCopy(_img, src.img, dstX, dstY, srcX, srcY, w, h);
    }

    // Make the palette of this image closer to the colors in 'other',
    // which must be truecolor.
    public bool colorMatchFrom(Image other) {
      return LibGD.gdImageColorMatch(img, other.img) == 0;
    }

    // Create an exact copy of this image
    public Image clone () {
      return new Image(LibGD.gdImageClone(img));
    }/* clone */


    /* Bindings to LibGD. */
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

    public bool setInterpolationMethod(IMode id) {
      return LibGD.gdImageSetInterpolationMethod
        (_img, (gdInterpolationMethod)id) != 0;
    }

    public IMode getInterpolationMethod() {
      return (IMode)LibGD.gdImageGetInterpolationMethod(_img); }


    public void putChar(Font f, int x, int y, char c, int color) {
      LibGD.gdImageChar(_img, f.fdata, x, y, (int)c, color); }

    public void putCharUp(Font f, int x, int y, char c, int color) {
      LibGD.gdImageCharUp(_img, f.fdata, x, y, (int) c, color); }

    public void putString(Font f, int x, int y, string s, int color) {
      LibGD.gdImageStringCharStar(_img, f.fdata, x, y, s, color); }

    public void putStringUp(Font f, int x, int y, string s, int color) {
      LibGD.gdImageStringUpCharStar(_img, f.fdata, x, y, s, color); }

    

#if NOPE
#endif

  }
}


