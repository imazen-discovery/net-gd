
namespace GD {
  using Internal;

  /// <summary>
  ///   Enumeration of all interpolation methods.
  /// </summary>
  public enum IMode {
    /// <summary> An alias for another mode. May change.  </summary>
    Default             = gdInterpolationMethod.GD_DEFAULT,

    /// <summary> Bell </summary>
    Bell                = gdInterpolationMethod.GD_BELL,

    /// <summary> Bessel </summary>
    Bessel              = gdInterpolationMethod.GD_BESSEL,

    /// <summary> BilinearFixed </summary>
    BilinearFixed       = gdInterpolationMethod.GD_BILINEAR_FIXED,

    /// <summary> Bicubic </summary>
    Bicubic             = gdInterpolationMethod.GD_BICUBIC,

    /// <summary> BicubicFixed </summary>
    BicubicFixed        = gdInterpolationMethod.GD_BICUBIC_FIXED,

    /// <summary> Blackman </summary>
    Blackman            = gdInterpolationMethod.GD_BLACKMAN,

    /// <summary> Box </summary>
    Box                 = gdInterpolationMethod.GD_BOX,

    /// <summary> Bspline </summary>
    Bspline             = gdInterpolationMethod.GD_BSPLINE,

    /// <summary> Catmullrom </summary>
    Catmullrom          = gdInterpolationMethod.GD_CATMULLROM,

    /// <summary> Gaussian </summary>
    Gaussian            = gdInterpolationMethod.GD_GAUSSIAN,

    /// <summary> GeneralizedCubic </summary>
    GeneralizedCubic    = gdInterpolationMethod.GD_GENERALIZED_CUBIC,

    /// <summary> Hermite </summary>
    Hermite             = gdInterpolationMethod.GD_HERMITE,

    /// <summary> Hamming </summary>
    Hamming             = gdInterpolationMethod.GD_HAMMING,

    /// <summary> Hanning </summary>
    Hanning             = gdInterpolationMethod.GD_HANNING,

    /// <summary> Mitchell </summary>
    Mitchell            = gdInterpolationMethod.GD_MITCHELL,

    /// <summary> NearestNeighbour </summary>
    NearestNeighbour    = gdInterpolationMethod.GD_NEAREST_NEIGHBOUR,

    /// <summary> Power </summary>
    Power               = gdInterpolationMethod.GD_POWER,

    /// <summary> Quadratic </summary>
    Quadratic           = gdInterpolationMethod.GD_QUADRATIC,

    /// <summary> Sinc </summary>
    Sinc                = gdInterpolationMethod.GD_SINC,

    /// <summary> Triangle </summary>
    Triangle            = gdInterpolationMethod.GD_TRIANGLE,
  
    //Weighted4         = gdInterpolationMethod.GD_WEIGHTED4,   // broken
  }
}