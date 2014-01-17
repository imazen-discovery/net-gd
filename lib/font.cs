
namespace GD {
  using Internal;

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
}