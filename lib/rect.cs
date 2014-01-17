
namespace GD {
  using Internal;

  public class Point {
    private int _x, _y;
    public int x { get {return _x;} }
    public int y { get {return _y;} }

    public Point(int x, int y) {
      _x = x;
      _y = y;
    }
  }

  // Not actually guaranteed to be a rectangle.
  public class Rect {
    protected Point _bottomLeft, _bottomRight,_topRight, _topLeft;
    public Point bottomLeft  { get {return _bottomLeft; } }
    public Point bottomRight { get {return _bottomRight; } }
    public Point topLeft     { get {return _topLeft; } }
    public Point topRight    { get {return _topRight; } }

    public Rect(int[] pts) {
      _bottomLeft  = new Point(pts[0], pts[1]);
      _bottomRight = new Point(pts[2], pts[3]);
      _topRight    = new Point(pts[4], pts[5]);
      _topLeft     = new Point(pts[6], pts[7]);
    }

    public Rect(Point topLeft, Point topRight, Point bottomLeft,
                     Point bottomRight) {
      _topLeft = topLeft;
      _topRight = topRight;
      _bottomLeft = bottomLeft;
      _bottomRight = bottomRight;
    }
  }

  // This is currently tested but not used.

  // A rectangle guaranteed to have all of its edges be horizontal or
  // vertical.
  public class TrueRect : Rect {
    public TrueRect(Point topLeft, Point bottomRight) 
    :  base (topLeft,
             new Point(bottomRight.x, topLeft.y),
             new Point(topLeft.x, bottomRight.y),
             bottomRight) { }

    public TrueRect(int tlx, int tly, int brx, int bry)
    : base (new Point (tlx, tly),
            new Point (brx, tly),
            new Point (tlx, bry),
            new Point (brx, bry)) {}

    public int width  { get {return _bottomRight.x - _topLeft.x;} }
    public int height { get {return _bottomLeft.y  - _topLeft.y;} }
  }

}