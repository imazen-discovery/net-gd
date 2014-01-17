
namespace GD {
  using Internal;

  public enum IMode {
    Default             = gdInterpolationMethod.GD_DEFAULT,
    Bell                = gdInterpolationMethod.GD_BELL,
    Bessel              = gdInterpolationMethod.GD_BESSEL,
    BilinearFixed       = gdInterpolationMethod.GD_BILINEAR_FIXED,
    Bicubic             = gdInterpolationMethod.GD_BICUBIC,
    BicubicFixed        = gdInterpolationMethod.GD_BICUBIC_FIXED,
    Blackman            = gdInterpolationMethod.GD_BLACKMAN,
    Box                 = gdInterpolationMethod.GD_BOX,
    Bspline             = gdInterpolationMethod.GD_BSPLINE,
    Catmullrom          = gdInterpolationMethod.GD_CATMULLROM,
    Gaussian            = gdInterpolationMethod.GD_GAUSSIAN,
    GeneralizedCubic    = gdInterpolationMethod.GD_GENERALIZED_CUBIC,
    Hermite             = gdInterpolationMethod.GD_HERMITE,
    Hamming             = gdInterpolationMethod.GD_HAMMING,
    Hanning             = gdInterpolationMethod.GD_HANNING,
    Mitchell            = gdInterpolationMethod.GD_MITCHELL,
    NearestNeighbour    = gdInterpolationMethod.GD_NEAREST_NEIGHBOUR,
    Power               = gdInterpolationMethod.GD_POWER,
    Quadratic           = gdInterpolationMethod.GD_QUADRATIC,
    Sinc                = gdInterpolationMethod.GD_SINC,
    Triangle            = gdInterpolationMethod.GD_TRIANGLE,
    //Weighted4         = gdInterpolationMethod.GD_WEIGHTED4,   // broken
  }
}