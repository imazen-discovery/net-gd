/// <summary>
///   Basic CSharp wrappers around GD data objects.
/// </summary>

using GD.Internal;

namespace GD {

    class Image : IDisposable {
        private gdImage img = null;
        
        /// <summary>
        ///   Dispose method.  gdImage's default dispose does the
        ///   wrong thing.
        /// </summary>
        public virtual void Dispose() {
            lock(this) {
                if (img != null) {
                    gdImageDestroy(img);
                    img = null;
                }/* if */
            }
        }


        public int sx { get {return img.sx} };
        public int sy { get {return img.sy} };


    }

}