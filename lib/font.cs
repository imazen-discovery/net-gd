
namespace GD {
  using Internal;


  /// <summary>
  ///   Wrapper class for struct gdFont.  There are only a fixed
  ///   number of instances of these class, one for each built-in font
  ///   in GD.
  /// </summary>
  public class Font {
    private SWIGTYPE_p_gdFont _fdata;
    internal SWIGTYPE_p_gdFont fdata { get {return _fdata; } }

    private Font(SWIGTYPE_p_gdFont fontData) {
      _fdata = fontData;
    }/* Font*/

    /// <summary> Character width </summary>
    public int w { get {return LibGD.gdFont_w_get(_fdata); } }

    /// <summary> Character height </summary>
    public int h { get {return LibGD.gdFont_h_get(_fdata); } }

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

    /// <summary> Return the GD tiny font. </summary>
    public static Font tiny       { get {_initFonts(); return _tiny;} }

    /// <summary> Return the GD small font.  </summary>
    public static Font small      { get {_initFonts(); return _small;} }

    /// <summary> Return the GD medium-bold font.  </summary>
    public static Font mediumBold { get {_initFonts(); return _mediumBold;} }

    /// <summary> Return the GD large font.  </summary>
    public static Font large      { get {_initFonts(); return _large;} }

    /// <summary> Return the GD giant font.  </summary>
    public static Font giant      { get {_initFonts(); return _giant;} }
  }
}