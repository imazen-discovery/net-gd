
//#define SAVE

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
      Image im = new Image(10, 10);
    
      int red = im.colorClosest(255, 0, 0);
      Assert.Greater(red, 0);

      im.filledRectangle(0, 0, 9, 9, red);
      Assert.AreEqual(im.getPixel(5, 5), red);

#if SAVE
      im.file("BasicCall.png");
#endif

      Assert.IsTrue(im.grayScale());

#if SAVE
      im.file("BasicCall2.png");
#endif

      im.Dispose();
    }



  }

}