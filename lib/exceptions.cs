
using System;

namespace GD {

  /// <summary>
  ///   Abstract base class for GD exceptions.  These exceptions are
  ///   intended to signal programmer errors and so ideally should be
  ///   prevented instead of caught.
  /// </summary>
  public class GDException : Exception {}

  /// <summary>
  ///  Attempted to use an ImageData as valid (i.e. containing valid
  ///  data) when it wasn't.  You should ensure ImageData.valid is
  ///  true and that ImageData.type is not UNKNOWN before decoding an
  ///  ImageData.
  /// </summary>
  public class GDinvalidImageData : GDException {}

  /// <summary>
  ///   Attempted to encode or decode an image to format
  ///   'Enc.UNKNOWN'.
  /// </summary>
  public class GDinvalidFormat : GDException {}
}
