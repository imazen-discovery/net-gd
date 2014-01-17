using System;
using System.IO;

namespace GD {
  using Internal;

  public enum Enc {
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
    byte[] data;
    
    public ImageData(string filename) {
    }

    public ImageData(Image img, Enc type = Enc.GD) {
    }

    public void save(Stream outstream) {
    }

    public Image decode() {
      return null;
    }

    
    static private FileType[] types = null;
    static void init_types() {
      if (types != null) return;
      types = new FileType[] {
        new FileType("gif", Enc.GIF, (sz, data) => {}),
      };

    }

  }

}