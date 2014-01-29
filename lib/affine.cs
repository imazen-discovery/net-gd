using System;

namespace GD {
  using Internal;

  class Affine {
    private double[] _matrix = newMatrix();
    internal double[] matrix { get {return _matrix;} }

    public Affine(double[] coeffs) {
      if (coeffs.Length != 6) {
        throw new ArgumentException("Affine coefficient list length not 6."); 
      }
      _matrix = (double[])coeffs.Clone();
    }

    static private double[] newMatrix() {
      return new [] {0.0, 0.0, 0.0, 0.0, 0.0, 0.0};
    }

    public double expansion() {
      return LibGD.gdAffineExpansion(_matrix);
    }/* expansion*/

    public double rectilinear() {
      return LibGD.gdAffineRectilinear(_matrix);
    }/* rectilinear*/

    /// <summary>
    ///   Test for equality via gdAffineEqual(), which gives us
    ///   equality within a range defined in GD.  This is different
    ///   enough from what C# needs to make it ineligible for standard
    ///   object equality.
    /// </summary>
    public bool equalish(Affine a) {
      return LibGD.gdAffineEqual(_matrix, a.matrix) != 0;
    }


    public static Affine identity() {
      var m = newMatrix();
      LibGD.gdAffineIdentity(m);
      return new Affine(m);
    }

    public static Affine scale(double scale_x, double scale_y) {
      var m = newMatrix();
      LibGD.gdAffineScale(m, scale_x, scale_y);
      return new Affine(m);
    }

    public static Affine rotate(double angle) {
      var m = newMatrix();
      LibGD.gdAffineRotate(m, angle);
      return new Affine(m);
    }
    
    public static Affine shearHorizontal(double angle) {
      var m = newMatrix();
      LibGD.gdAffineShearHorizontal(m, angle);
      return new Affine(m);
    }/* shearHorizontal*/
      
    public static Affine shearVertical(double angle) {
      var m = newMatrix();
      LibGD.gdAffineShearVertical(m, angle);
      return new Affine(m);
    }/* shearVertical*/
      
    public static Affine translate(double offset_x, double offset_y) {
      var m = newMatrix();
      LibGD.gdAffineTranslate(m, offset_x, offset_y);
      return new Affine(m);
    }/* translate*/
      
    

  }



}