
using System;

namespace GD {
  // All GD exceptions are avoidable.  
  
  // Abstract base class for GD exceptions
  public class GDException : Exception {}

  // Attempted to use an ImageData as valid (i.e. containing valid
  // data) when it wasn't.
  public class GDinvalidImageData : GDException {}
}
