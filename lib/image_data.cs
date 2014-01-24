using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace GD {
  using Internal;

  public enum Enc {
    UNKNOWN,
    GIF, GD, GD2, WBMP, BMP, PNG, JPG, TIFF, 
#if BROKEN_FORMATS  // These formats are currently broken in the GD trunk
    WEBP, XPM, TGA,
#endif 
  }

  internal class FileType {
    public delegate void ReaderFn(int size, byte[] data);

    public readonly string extension;
    public readonly Enc type;
    public readonly ReaderFn reader;
    
    public FileType(string ext, Enc t, ReaderFn fn) {
      extension = ext;
      type = t;
      reader = fn;
    }
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


    public Image decode() {
      if (!valid || type == Enc.UNKNOWN) throw new GDinvalidImageData();
                                                
      SWIGTYPE_p_gdImageStruct img;
      unsafe {
        fixed(byte *p = data) {
          var datap = new IntPtr(p);
          img = LibGD.gdImageCreateFromPngPtr(data.Length, datap);
        }
      }

      if (img == null) return null;
      return new Image(img);
    }

    
#if NOPE    
    static private FileType[] types = null;
    static void init_types() {
      if (types != null) return;
      types = new FileType[] {
        new FileType("png", Enc.PNG, 
                     (sz, data) => {LibGD.gdImageCreateFromPngPtr(sz, data);}),
      };

    }
#endif

  }

}