/// <summary>
///   Basic CSharp wrappers around GD data objects.
/// </summary>

using System;

namespace GD {
  using Internal;

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

  }

}