using System;
using System.IO;
using System.Runtime.InteropServices;

namespace GD {
  using Internal;

  public enum Enc {
    UNKNOWN,
    GIF, GD, GD2, WBMP, BMP, TGA, PNG, JPG, TIFF, WEBP, XPM, 
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


  class ImageData {
    private byte[] data = null;
    public Enc type = Enc.UNKNOWN;

    private bool _valid = false;
    public bool valid { get {return _valid;} }

    public ImageData(Stream instream) {

      /* Files bigger than 4GB don't work due to this stupid word size
       * mismatch.  Theoretically, we could work around this. */
      if (instream.Length > Int32.MaxValue) {
        _valid = false;
        return;
      }

      try {

        using (var r = new BinaryReader(instream)) {
          data = r.ReadBytes(Convert.ToInt32(instream.Length));
          _valid = true;
        }/* using */

      } catch (IOException) {
        _valid = false;
        data = null;
      }/* try ... catch */
    }

    public ImageData(Image im) {
      int sz = 0;
      IntPtr ptr = LibGD.gdImagePngPtr(im.img, out sz);
      if (ptr == IntPtr.Zero) return;

      data = new byte[sz];
      Marshal.Copy(ptr, data, 0, sz);

      LibGD.gdFree(ptr);

      _valid = true;
    }

    public void save(Stream outstream) {
    }


    public Image decode() {
      
      return null;
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