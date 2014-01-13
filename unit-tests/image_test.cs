
#define SAVE

using System;

namespace GD {
  using NUnit.Framework;

  [TestFixture]
  public class SmokeTest {

    [Test]
    public void GetVersion() {
      int mj = Image.majorVersion;
      int mn = Image.minorVersion;
      int rv = Image.releaseVersion;
      string ex = Image.extraVersion;

      Assert.Greater(mj, 0);
      Assert.AreEqual(Image.versionString, mj + "." + mn + "." + rv + ex);

      Image.fontCacheSetup();

      //Assert.AreEqual(1, Image.useFontConfig(false));
    }

    [Test]
    public void BasicCall() {
      Image im = new Image(100, 100);
    
      int red = im.colorClosest(255, 0, 0);
      Assert.Greater(red, 0);

      im.filledRectangle(10, 10, 90, 90, red);
      Assert.AreEqual(im.getPixel(50, 50), red);

#if SAVE
      im.file("BasicCall.png");
#endif
    }

    [Test]
    public void BasicCall2() {
      Image im = new Image(100, 100);
      int white = im.colorClosest(255, 255, 255);

      Font sm = Font.small;
      im.putChar(sm, 10, 10, 'a', white);
      im.putChar(sm, 10 + sm.w , 10 + sm.h, 'b', white);
      im.putCharUp(sm, 10, 40, 'c', white);
      im.putCharUp(sm, 10 + sm.h, 40, 'd', white);
      im.putString(sm, 10, 60, "horizontal", white);
      im.putStringUp(sm, 80, 80, "vertical", white);

      /* There's not an easy way to check this programmatically so I'm
       * punting for now.  Just eyeball the resulting image.*/

      /* Test FT string writing. */
      Rect bounds;
      string errmsg; 
      string fontpath =
        "/usr/share/fonts/truetype/liberation/LiberationMono-Regular.ttf";

      Assert.IsTrue(
        im.stringFT(white, fontpath, 10.0, 125.0, 20, 10, "whoah!", out bounds,
          out errmsg)
        );

      Assert.AreEqual("", errmsg);
      Assert.Less(bounds.topLeft.y, bounds.bottomRight.y);
      Assert.Less(bounds.bottomLeft.x, bounds.topRight.x);

      Assert.IsTrue(
        im.stringFT(white, fontpath, 10.0, 125.0, 20, 10, "whoah!")
        );

#if SAVE
      im.file("BasicCall2.png");
#endif
    }
    
    [Test]
    public void BasicCall3() {
      Image im = new Image(100, 100);
    
      int red = im.colorClosest(255, 0, 0);
      im.filledRectangle(10, 10, 90, 90, red);

      Assert.IsTrue(im.grayScale());

#if SAVE
      im.file("BasicCall3.png");
#endif
    }


    [Test]
    public void Rescale() {
      Image im = new Image(100, 100);
    
      int red = im.colorClosest(255, 0, 0);
      im.filledRectangle(10, 10, 90, 90, red);

      Image dest = new Image(200, 200);

      dest.copyResizedFrom(im, 0, 0, 0, 0, 40, 40, 20, 20);
      dest.copyResampledFrom(im, 50, 50, 0, 0, 40, 40, 20, 20);

#if SAVE
      dest.file("Rescale.png");
#endif
    }

    [Test]
    public void TestRect () {
      TrueRect t = new TrueRect(10, 10, 30, 30);
      Assert.AreEqual(t.topLeft.x, t.bottomLeft.x);
      Assert.AreEqual(t.topRight.x, t.bottomRight.x);
      Assert.AreEqual(t.topLeft.y, t.topRight.y);
      Assert.AreEqual(t.bottomLeft.y, t.bottomRight.y);

      Assert.AreEqual(20, t.width);
      Assert.AreEqual(20, t.height);
    }

    [Test]
    public void TestTrueColor () {
      Image tc = new Image(100, 100, true);
      Image pc = new Image(100, 100, false);
      
      Assert.IsTrue(tc.trueColor);
      Assert.IsFalse(pc.trueColor);
    }


    [Test]
    public void Blur() {
      Image im = new Image(100, 100);
    
      int red = im.colorClosest(255, 0, 0);
      im.filledRectangle(10, 10, 90, 90, red);

      Image dest = im.copyGaussianBlurred(4);

#if SAVE
      dest.file("Blur.png");
#endif
    }


    [Test]
    public void Scale() {
      Image im = new Image(100, 100);
      int white = im.colorClosest(255, 255, 255);

      Font sm = Font.small;
      im.putString(sm, 10, 60, "text!!!", white);

      im.interpolation_method = IMode.Bicubic;
      Assert.AreEqual(IMode.Bicubic, im.interpolation_method);

      Image half = im.scale(50);
      Assert.AreEqual(50, half.sx);
      Assert.AreEqual(50, half.sy);
#if SAVE
      half.file("Scale1.png");
#endif

      Image halfx = im.scale(50, 100);
      Assert.AreEqual(50, halfx.sx);
      Assert.AreEqual(100, halfx.sy);
#if SAVE
      halfx.file("Scale2.png");
#endif

      Image dbl = im.scale(200);
      Assert.AreEqual(200, dbl.sx);
      Assert.AreEqual(200, dbl.sy);
#if SAVE
      dbl.file("Scale3.png");
#endif
    }
  }

}

