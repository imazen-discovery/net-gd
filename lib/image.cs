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
    internal SWIGTYPE_p_gdFont font;

    private Font(SWIGTYPE_p_gdFont fontData) {
      font = fontData;
    }/* Font*/

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


  public class Image : IDisposable {
    SWIGTYPE_p_gdImageStruct img = null;

    // Constructor.
    public Image(int sx, int sy, bool truecolor = true) {
      if (truecolor) {
        img = LibGD.gdImageCreateTrueColor(sx, sy);
      } else {
        img = LibGD.gdImageCreate(sx, sy);
      }/* if .. else*/
    }/* Image*/

    // Private constructor for wrapping an existing gdImage struct.
    private Image(SWIGTYPE_p_gdImageStruct i) {
      img = i;
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
        if (img != null) {
          LibGD.gdImageDestroy(img);
          img = null;
        }/* if */
      }
    }
    
    public int sx { get {return LibGD.gdImage_sx_get(img);} }
    public int sy { get {return LibGD.gdImage_sy_get(img);} }

    public static int majorVersion { get {return LibGD.gdMajorVersion();} }
    public static int minorVersion { get {return LibGD.gdMinorVersion();} }
    public static int releaseVersion { get {return LibGD.gdReleaseVersion();} }
    public static string extraVersion { get {return LibGD.gdExtraVersion();} }
    public static string versionString {get {return LibGD.gdVersionString();} }


    /* Bindings to LibGD. */
    public void setPixel(int x, int y, int color) {
      LibGD.gdImageSetPixel(img, x, y, color); }

    public int getPixel(int x, int y) {
      return LibGD.gdImageGetPixel(img, x, y); }

    public int getTrueColorPixel(int x, int y) {
      return LibGD.gdImageGetTrueColorPixel(img, x, y); }

    public void line(int x1, int y1, int x2, int y2, int color) {
      LibGD.gdImageLine(img, x1, y1, x2, y2, color); }

    public void dashedLine(int x1, int y1, int x2, int y2, int color) {
      LibGD.gdImageDashedLine(img, x1, y1, x2, y2, color); }

    public void rectangle(int x1, int y1, int x2, int y2, int color) {
      LibGD.gdImageRectangle(img, x1, y1, x2, y2, color); }

    public void filledRectangle(int x1, int y1, int x2, int y2,
                                       int color) {
      LibGD.gdImageFilledRectangle(img, x1, y1, x2, y2, color); }

    public void setClip(int x1, int y1, int x2, int y2) {
      LibGD.gdImageSetClip(img, x1, y1, x2, y2); }

    public void setResolution(uint res_x, uint res_y) {
      LibGD.gdImageSetResolution(img, res_x, res_y); }

    public bool boundsSafe(int x, int y) {
      return LibGD.gdImageBoundsSafe(img, x, y) != 0; }

    public int colorAllocate(int r, int g, int b) {
      return LibGD.gdImageColorAllocate(img, r, g, b); }

    public int colorAllocateAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorAllocateAlpha(img, r, g, b, a); }

    public int colorClosest(int r, int g, int b) {
      return LibGD.gdImageColorClosest(img, r, g, b); }

    public int colorClosestAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorClosestAlpha(img, r, g, b, a); }

    public int colorClosestHWB(int r, int g, int b) {
      return LibGD.gdImageColorClosestHWB(img, r, g, b); }

    public int colorExact(int r, int g, int b) {
      return LibGD.gdImageColorExact(img, r, g, b); }

    public int colorExactAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorExactAlpha(img, r, g, b, a); }

    public int colorResolve(int r, int g, int b) {
      return LibGD.gdImageColorResolve(img, r, g, b); }

    public int colorResolveAlpha(int r, int g, int b, int a) {
      return LibGD.gdImageColorResolveAlpha(img, r, g, b, a); }

    public void colorDeallocate(int color) {
      LibGD.gdImageColorDeallocate(img, color); }

    public int trueColorToPalette(int ditherFlag, int colorsWanted) {
      return LibGD.gdImageTrueColorToPalette(img, ditherFlag, colorsWanted); }

    public int paletteToTrueColor() {
      return LibGD.gdImagePaletteToTrueColor(img); }

    public int trueColorToPaletteSetMethod(int method, int speed) {
      return LibGD.gdImageTrueColorToPaletteSetMethod(img, method, speed); }

    public void trueColorToPaletteSetQuality(int min_quality,
                                                    int max_quality) {
      LibGD.gdImageTrueColorToPaletteSetQuality(img, min_quality,max_quality);}

    public void colorTransparent(int color) {
      LibGD.gdImageColorTransparent(img, color); }

    public int colorReplace(int src, int dst) {
      return LibGD.gdImageColorReplace(img, src, dst); }

    public int colorReplaceThreshold(int src, int dst, float threshold){
      return LibGD.gdImageColorReplaceThreshold(img, src, dst, threshold); }

    public bool file(string filename) {
      return LibGD.gdImageFile(img, filename) != 0; }

    public void filledArc(int cx, int cy, int w, int h, int s, int e,
                                 int color, int style) {
      LibGD.gdImageFilledArc(img, cx, cy, w, h, s, e, color, style); }

    public void arc(int cx, int cy, int w, int h, int s, int e,
                           int color) {
      LibGD.gdImageArc(img, cx, cy, w, h, s, e, color); }

    public void ellipse(int cx, int cy, int w, int h, int color) {
      LibGD.gdImageEllipse(img, cx, cy, w, h, color); }

    public void filledEllipse(int cx, int cy, int w, int h, int color) {
      LibGD.gdImageFilledEllipse(img, cx, cy, w, h, color); }

    public void fillToBorder(int x, int y, int border, int color) {
      LibGD.gdImageFillToBorder(img, x, y, border, color); }

    public void fill(int x, int y, int color) {
      LibGD.gdImageFill(img, x, y, color); }

    public void setAntiAliased(int c) {
      LibGD.gdImageSetAntiAliased(img, c); }

    public void setAntiAliasedDontBlend(int c, int dont_blend) {
      LibGD.gdImageSetAntiAliasedDontBlend(img, c, dont_blend); }

    public void setThickness(int thickness) {
      LibGD.gdImageSetThickness(img, thickness); }

    public void interlace(int interlaceArg) {
      LibGD.gdImageInterlace(img, interlaceArg); }

    public void alphaBlending(int alphaBlendingArg) {
      LibGD.gdImageAlphaBlending(img, alphaBlendingArg); }

    public void saveAlpha(int saveAlphaArg) {
      LibGD.gdImageSaveAlpha(img, saveAlphaArg); }

    public bool pixelate(int block_size, uint mode) {
      return LibGD.gdImagePixelate(img, block_size, mode) != 0; }

    public bool scatter(int sub, int plus) {
      return LibGD.gdImageScatter(img, sub, plus) != 0; }

    public bool smooth(float weight) {
      return LibGD.gdImageSmooth(img, weight) != 0; }

    public bool meanRemoval() {
      return LibGD.gdImageMeanRemoval(img) != 0; }

    public bool emboss() {
      return LibGD.gdImageEmboss(img) != 0; }

    public bool gaussianBlur() {
      return LibGD.gdImageGaussianBlur(img) != 0; }

    public bool edgeDetectQuick() {
      return LibGD.gdImageEdgeDetectQuick(img) != 0; }

    public bool selectiveBlur() {
      return LibGD.gdImageSelectiveBlur(img) != 0; }

    public int color(int red, int green, int blue, int alpha) {
      return LibGD.gdImageColor(img, red, green, blue, alpha); }

    public bool contrast(double contrast) {
      return LibGD.gdImageContrast(img, contrast) != 0; }

    public bool brightness(int brightness) {
      return LibGD.gdImageBrightness(img, brightness) != 0; }

    public bool grayScale() {
      return LibGD.gdImageGrayScale(img) != 0; }

    public bool negate() {
      return LibGD.gdImageNegate(img) != 0; }

    public void flipHorizontal() {
      LibGD.gdImageFlipHorizontal(img); }

    public void flipVertical() {
      LibGD.gdImageFlipVertical(img); }

    public void flipBoth() {
      LibGD.gdImageFlipBoth(img); }

    public bool setInterpolationMethod(IMode id) {
      return LibGD.gdImageSetInterpolationMethod
        (img, (gdInterpolationMethod)id) != 0;
    }

    public IMode getInterpolationMethod() {
      return (IMode)LibGD.gdImageGetInterpolationMethod(img); }
  }
}


