
// If set, write some of the resulting images to disk.
//#define SAVE

using System;
using System.IO;

namespace GD {
  using NUnit.Framework;

  [TestFixture]
  public class AffineTest {

    private Image mkTestImg() {
      Image im = new Image(100, 100);
    
      int red = im.colorClosest(255, 0, 0);
      int white = im.colorClosest(255, 255, 255);

      im.filledRectangle(10, 10, 90, 90, red);

      Font sm = Font.small;
      im.putChar(sm, 10, 10, 'a', white);
      im.putChar(sm, 10 + sm.w , 10 + sm.h, 'b', white);
      im.putCharUp(sm, 10, 40, 'c', white);
      im.putCharUp(sm, 10 + sm.h, 40, 'd', white);
      im.putString(sm, 10, 60, "horizontal", white);
      im.putStringUp(sm, 80, 80, "vertical", white);

      return im;
    }


    [Test]
    public void AffineCreation () {
      Affine id = Affine.identity();
      Assert.AreNotEqual(null, id);
      Assert.IsTrue(id.rectilinear());
      Assert.AreEqual(1.0, id.expansion());
      Assert.IsTrue(id.equalish(id));
      Assert.IsFalse(id.equalish(Affine.scale(0.5, 0.5)));

      TrueRect srcbox = new TrueRect(10, 10, 20, 20);
      TrueRect bb = id.boundingBoxFor(srcbox);
      Assert.AreNotEqual(null, bb);
      
      Affine rot = Affine.rotate(0.5);
      Assert.IsTrue(rot.equalish(Affine.rotate(0.5)));
      Assert.IsFalse(rot.equalish(Affine.rotate(1.5)));

      Assert.IsTrue(Affine.shearVertical(0.5).
                    equalish(Affine.shearVertical(0.5)));
      Assert.IsFalse(Affine.shearVertical(0.5).
                    equalish(Affine.shearVertical(5.5)));

      Assert.IsTrue(Affine.shearHorizontal(0.5).
                    equalish(Affine.shearHorizontal(0.5)));
      Assert.IsFalse(Affine.shearHorizontal(0.5).
                    equalish(Affine.shearHorizontal(5.5)));

      Assert.IsTrue(Affine.translate(20,30).
                    equalish(Affine.translate(20,30)));
      Assert.IsFalse(Affine.translate(20,30).
                     equalish(Affine.translate(5.4,11.9)));



    }/* AffineCreation */

  }/* class */
}/* namespace */

