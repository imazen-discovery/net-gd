using System.Runtime.InteropServices;

namespace GD {
  using Internal;

  private class FakeStream {
    int getNext() {
      return 0;
    }

    void putNext(int n) {
    }
  }


  public class IOCtx {
    public delegate int getCdelegate(SWIGTYPE_p_gdIOCtx ctx);

//    private static extern int getC(

  }

}