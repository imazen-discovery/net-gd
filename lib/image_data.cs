using System;
using System.IO;

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
    private byte[] data;
    public Enc type = Enc.UNKNOWN;

    private bool _valid = false;
    public bool valid { get {return _valid;} }

    public ImageData(Stream instream) {
      try {
        using (var r = new BinaryReader(instream)) {
          data = r.ReadBytes(instream.Length);
          _valid = true;
        }/* using */
      } catch (IOException e) {
        _valid = false;
        data = null;
      }/* try ... catch */
    }

    public ImageData(Image img, Enc type = Enc.GD) {
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