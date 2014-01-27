using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace GD {
  using Internal;

  public enum Enc {
    UNKNOWN,
    GIF, GD, GD2, WBMP, BMP, PNG, JPEG, TIFF, 
    // XPM can only be read from a file
#if BROKEN_FORMATS  // These formats are currently broken in the GD trunk
    WEBP, XBM, TGA,
#endif
  }

  internal delegate IntPtr EncFn(Image im, out int sz);

  public class ImageData {
    private byte[] data = null;
    public Enc type = Enc.UNKNOWN;

    public bool valid { get {return data != null;} }

    public ImageData(BinaryReader reader, Enc tp = Enc.PNG) {
      data = load(reader);
      type = tp;
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