
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
  }


  // [Test]
  // public void BasicCall() {
  //   Image im = new Image(10, 10);

  //   im.colorClosest(, 0, 0);



  // }
}