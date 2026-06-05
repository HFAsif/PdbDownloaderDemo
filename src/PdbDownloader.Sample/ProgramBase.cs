
using System.Runtime.InteropServices;

internal class ProgramBase
{
#if NET35 || NET20
    protected const string _clrLib = "mscorwks.dll";
    protected const string _clrjitLib = "mscorjit.dll";
#elif NET40_OR_GREATER
    protected const string _clrLib = "clr.dll";
    protected const string _clrjitLib = "clrjit.dll";
#elif NET5_0_OR_GREATER
    protected const string _clrLib = "coreclr.dll";
    protected const string _clrjitLib = "clrjit.dll";
#endif
    protected static string _clrSystemLocation => Io.Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), _clrLib);
    protected static string _clrjitSystemLocation => Io.Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), _clrjitLib);
}
