
/* Delegates used for callbacks into gdIOCtx structs.

   This gets linked into net-gd-glue.dll (in wrapper/) but is kept here
   because wrapper/ is filled with generated C# code and it's nice to be
   able to blow it all away without losing anything valuable. */


namespace GD.Internal {
  public delegate int getCdelegate(SWIGTYPE_p_gdIOCtx ptr);
//  public delegate int getBufDelegate(SWIGTYPE_p_gdIOCtx ptr, );
}