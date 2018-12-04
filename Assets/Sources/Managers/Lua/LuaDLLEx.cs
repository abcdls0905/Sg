namespace XLua.LuaDLL
{

    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using XLua;

    public partial class Lua
    {
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)] //[-0, +0, m]
        public static extern int luaopen_pb(IntPtr L);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)] //[-0, +0, m]
        public static extern int luaopen_cjson(IntPtr L);
        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)] //[-0, +0, m]
        public static extern int luaopen_protobuf_c(IntPtr L);
    }
}