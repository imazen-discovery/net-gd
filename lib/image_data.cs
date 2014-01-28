using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace GD {
  using Internal;

  /// <summary> All know and supported image types. </summary>  
  public enum Enc {
    /// <summary> No known type. </summary>
    UNKNOWN,

    /// <summary> GIF format </summary>
    GIF, 

    /// <summary> GD format </summary>
    GD,

    /// <summary> GD format </summary>
    GD2,

    /// <summary> WBMP format </summary>
    WBMP,

    /// <summary> BMP format </summary>
    BMP,

    /// <summary> PNG format </summary>
    PNG,

    /// <summary> JPEG format </summary>
    JPEG,

    /// <summary> TIFF format </summary>
    TIFF, 

    // XPM can only be read from a file
#if BROKEN_FORMATS  // These formats are currently broken in the GD trunk
    WEBP, XBM, TGA,
#endif
  }

  internal delegate IntPtr EncFn(Image im, out int sz);

  /// <summary>
  ///   Represents the contents of an image file encoded in one of the
  ///   preferred formats.  Can be read from or written to a stream
  ///   and/or converted to an Image.
  /// </summary>
  public class ImageData {
    private byte[] data = null;

    /// <summary> Type (i.e. file format) of the image data. </summary>
    public Enc type = Enc.UNKNOWN;

    /// <summary>
    ///   True if this contains an apparently valid image; false
    ///   otherwise.  Note that this is true if no error was detected
    ///   when reading or encoding the content; it does not say
    ///   anything about the actual correctness of the image.  To find
    ///   that out, you will need to actully decode() it.
    /// </summary>
    public bool valid { get {return data != null;} }

    /// <summary>
    ///   Constructor; create new file from an image reader.  Type
    ///   must also be set.
    /// </summary>
    public ImageData(BinaryReader reader, Enc imageType) {
      data = load(reader);
      type = imageType;
    }

    internal ImageData(Image im, EncFn fn, Enc enctype) {
      data = null;
      type = Enc.UNKNOWN;

      int sz = 0;
      IntPtr ptr = fn(im, out sz);
      if (ptr == IntPtr.Zero) return;

      data = new byte[sz];
      Marshal.Copy(ptr, data, 0, sz);

      LibGD.gdFree(ptr);

      type = enctype;
    }

    private byte[] load(BinaryReader reader) {
      /* Files bigger than 4GB don't work due to this stupid word size
       * mismatch.  Theoretically, we could work around this. */
      if (reader.BaseStream.Length > Int32.MaxValue) {
        return null;
      }

      byte[] contents = null;
      try {
        contents = reader.ReadBytes(Convert.ToInt32(reader.BaseStream.Length));
      } catch (IOException) {
        contents = null;
      }/* try ... catch */

      return contents;
    }

    /// <summary>
    ///   Write the contents to the given BinaryWriter.  Throws
    ///   GDinvalidImageData if this contains no valid data of a known
    ///   type.
    /// </summary>
    public void save(BinaryWriter writer) {
      if (!valid || type == Enc.UNKNOWN) throw new GDinvalidImageData();
      writer.Write(data);
    }


    private SWIGTYPE_p_gdImageStruct doDecode(IntPtr pdata, int len) {
      switch (type) {
      case Enc.GIF:  return LibGD.gdImageCreateFromGifPtr(len, pdata);
      case Enc.GD:   return LibGD.gdImageCreateFromGdPtr(len, pdata);
      case Enc.GD2:  return LibGD.gdImageCreateFromGd2Ptr(len, pdata);
      case Enc.WBMP: return LibGD.gdImageCreateFromWBMPPtr(len, pdata);
      case Enc.BMP:  return LibGD.gdImageCreateFromBmpPtr(len, pdata);
      case Enc.PNG:  return LibGD.gdImageCreateFromPngPtr(len, pdata);
      case Enc.JPEG: return LibGD.gdImageCreateFromJpegPtr(len, pdata);
      case Enc.TIFF: return LibGD.gdImageCreateFromTiffPtr(len, pdata);
      default:
        throw new GDinvalidFormat();
      }
    }

    /// <summary>
    ///   Decode the contents and return the resulting image.  Returns
    ///   null if decoding fails.  Throws GDinvalidImageData if this
    ///   contains no valid data or has an unknown encoding type.
    /// </summary>
    public Image decode() {
      if (!valid || type == Enc.UNKNOWN) throw new GDinvalidImageData();
                                                
      SWIGTYPE_p_gdImageStruct img;
      unsafe {
        fixed(byte *p = data) {
          img = doDecode(new IntPtr(p), data.Length);
        }
      }

      if (img == null) return null;
      return new Image(img);
    }
  }
}

