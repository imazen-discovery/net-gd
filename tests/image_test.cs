
namespace GD {
  using NUnit.Framework;

  [TestFixture]
  public class SmokeTest {

    [Test]
    public void GetVersion() {
      int mj = Image.majorVersion;
//      int mn = Image.minorVersion;
//      int rv = Image.releaseVersion;

      Assert.Greater(mj, 0);
    }
  }
}