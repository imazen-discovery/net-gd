
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

#if SAVE
      im.file("BasicCall3.png");
#endif

      /* Test FT string writing. */
      string status;
      Rectangle bounds;
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
      im.file("BasicCall4.png");
#endif

      Assert.IsTrue(im.grayScale());

#if SAVE
      im.file("BasicCall2.png");
#endif

      im.Dispose();
    }



  }

}