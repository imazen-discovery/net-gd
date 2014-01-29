using System;

namespace GD {
  using Internal;

  /// <summary>
  ///   Represents a point on a plain.  Supports integer or FP
  ///   coordinates.
  /// </summary>
  public class Point {
    private double _x, _y;

    /// <summary> X coordinate rounded to nearest int </summary>
    public int x { get {return Convert.ToInt32(_x);} }

    /// <summary> Y coordinate rounded to nearest int </summary>
    public int y { get {return Convert.ToInt32(_y);} }

    /// <summary> X coordinate </summary>
    public double xd { get {return _x; } }

    /// <summary> Y coordinate </summary>
    public double yd { get {return _y; } }

    /// <summary>
    ///   Translate according to an affine. Wraps
    ///   gdAffineApplyToPointF().  Returns null on failure.
    /// </summary>
    public Point applyAffine(Affine a) {
      double[] points = new[] {_x, _y};

      int status = LibGD.gdAffineApplyToPointF_WRAP(points, a.matrix);
      if (status == 0) return null;

      return new Point(points[0], points[1]);
    }/* applyAffine*/

    /// <summary> Integer constructor. </summary>
    public Point(int x, int y) {
      _x = (double)x;
      _y = (double)y;
    }

    /// <summary> FP constructor. </summary>
    public Point(double x, double y) {
      _x = x;
      _y = y;
    }

  }


  /// <summary>
  ///   Represents a quadrilateral, typically the bounding rectangle
  ///   of a region of text.  Since this rectangle may be rotated,
  ///   there is no checking to ensure that this is actually a
  ///   rectangle.  This class is rarely used.
  /// </summary>
  public class Rect {
    private Point _bottomLeft, _bottomRight,_topRight, _topLeft;

    /// <summary> bottom-left corner </summary>
    public Point bottomLeft  { get {return _bottomLeft; } }

    /// <summary> bottom-right corner </summary>
    public Point bottomRight { get {return _bottomRight; } }

    /// <summary> top-left corner </summary>
    public Point topLeft     { get {return _topLeft; } }

    /// <summary> top-right corner </summary>
    public Point topRight    { get {return _topRight; } }

    /// <summary> Constructor; creates from list of integer points. </summary>
    public Rect(int[] pts) {
      _bottomLeft  = new Point(pts[0], pts[1]);
      _bottomRight = new Point(pts[2], pts[3]);
      _topRight    = new Point(pts[4], pts[5]);
      _topLeft     = new Point(pts[6], pts[7]);
    }

    /// <summary> Constructor; creates Point coordinates. </summary>
    public Rect(Point topLeft, Point topRight, Point bottomLeft,
                     Point bottomRight) {
      _topLeft = topLeft;
      _topRight = topRight;
      _bottomLeft = bottomLeft;
      _bottomRight = bottomRight;
    }
  }

  /// <summary>
  ///  A rectangle guaranteed to have all of its edges be horizontal
  ///  or vertical.  This is not currently used by the supported API.
  /// </summary>
  public class TrueRect : Rect {

    /// <summary> Constructor; takes top-left and bottom right corners as
    /// Points. </summary>
    public TrueRect(Point topLeft, Point bottomRight) 
    :  base (topLeft,
             new Point(bottomRight.x, topLeft.y),
             new Point(topLeft.x, bottomRight.y),
             bottomRight) { }

    /// <summary> Constructor: takes corners as integers. </summary>
    public TrueRect(int tlx, int tly, int brx, int bry)
    : base (new Point (tlx, tly),
            new Point (brx, tly),
            new Point (tlx, bry),
            new Point (brx, bry)) {}

    /// <summary> Rectangle width. </summary>
    public int width  { get {return bottomRight.x - topLeft.x;} }

    /// <summary> Rectangle height. </summary>
    public int height { get {return bottomLeft.y  - topLeft.y;} }
  }

}