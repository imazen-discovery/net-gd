
#define SAVE

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

      /* There's not an easy way to check this programmatically so I'm
       * punting for now.  Just eyeball the resulting image.*/

#if SAVE
      im.file("BasicCall3.png");
#endif

      Assert.IsTrue(im.grayScale());

#if SAVE
      im.file("BasicCall2.png");
#endif

      im.Dispose();
    }



  }

}