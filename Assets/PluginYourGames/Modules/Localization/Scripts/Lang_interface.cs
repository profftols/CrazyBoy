namespace YG
{
#if UNITY_WEBGL
    using System;
    using System.Runtime.InteropServices;
    using Insides;
#endif

    public partial interface IPlatformsYG2
    {
#if PLATFORM_WEBGL
        [DllImport("__Internal")]
        private static extern IntPtr GeneralLanguage_js();
#endif
        string GetLanguage()
        {
#if PLATFORM_WEBGL
            var ptr = GeneralLanguage_js();
            var str = Marshal.PtrToStringUTF8(ptr);
            YGInsides.FreeBuffer(ptr);
            return str;
#else
            return "en";
#endif
        }
    }
}