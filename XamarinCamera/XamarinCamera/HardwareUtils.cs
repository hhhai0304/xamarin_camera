using Android.Content;
using Android.Net.Wifi;
using Android.Net;
using Java.Lang;
using Java.Lang.Reflect;
using Xamarin.Forms;
using Java.IO;

namespace XamarinCamera
{
    class HardwareUtils
    {
        public static bool IsWifiEnable()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    var wm = Android.App.Application.Context.GetSystemService(Context.WifiService) as WifiManager;
                    return wm.IsWifiEnabled;
                default:
                    return false;
            }
        }

        public static bool IsMobileDataEnabled()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                    {
                        return Android.Provider.Settings.Global.GetInt(Android.App.Application.Context.ContentResolver, "mobile_data", 0) == 1;
                    }
                    else
                    {
                        try
                        {
                            ConnectivityManager cm = Android.App.Application.Context.GetSystemService(Context.ConnectivityService) as ConnectivityManager;
                            Method method = cm.Class.GetMethod("getMobileDataEnabled");
                            return (bool)method.Invoke(cm);
                        }
                        catch (Java.Lang.Exception e)
                        {
                            e.PrintStackTrace();
                        }
                        return false;
                    }
                case Device.iOS:
                    return false;
                default:
                    return false;
            }
        }

        public static bool IsDeviceRooted()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    return CheckRootMethod1() || CheckRootMethod2() || CheckRootMethod3();
                default:
                    return false;
            }

        }

        private static bool CheckRootMethod1()
        {
            string buildTags = Android.OS.Build.Tags;
            return buildTags != null && buildTags.Contains("test-keys");
        }

        private static bool CheckRootMethod2()
        {
            string[] paths = { "/system/app/Superuser.apk", "/sbin/su", "/system/bin/su", "/system/xbin/su", "/data/local/xbin/su", "/data/local/bin/su", "/system/sd/xbin/su",
                "/system/bin/failsafe/su", "/data/local/su", "/su/bin/su"};
            foreach (string path in paths)
            {
                if (new File(path).Exists()) return true;
            }
            return false;
        }

        private static bool CheckRootMethod3()
        {
            Process process = null;
            try
            {
                process = Runtime.GetRuntime().Exec(new string[] { "/system/xbin/which", "su" });
                BufferedReader buf = new BufferedReader(new InputStreamReader(process.InputStream));
                if (buf.ReadLine() != null) return true;
                return false;
            }
            catch (Throwable t)
            {
                return false;
            }
            finally
            {
                if (process != null) process.Destroy();
            }
        }
    }
}