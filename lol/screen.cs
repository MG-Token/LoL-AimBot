using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace screen
{
    public static class all
    {
        public static Rectangle searchBitmap(Bitmap smallBmp, Bitmap bigBmp, double tolerance)
        {
            BitmapData smallData =
              smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData bigData =
              bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int smallStride = smallData.Stride;
            int bigStride = bigData.Stride;

            int bigWidth = bigBmp.Width;
            int bigHeight = bigBmp.Height - smallBmp.Height + 1;
            int smallWidth = smallBmp.Width * 3;
            int smallHeight = smallBmp.Height;

            Rectangle location = Rectangle.Empty;
            int margin = Convert.ToInt32(255.0 * tolerance);

            unsafe
            {
                byte* pSmall = (byte*)(void*)smallData.Scan0;
                byte* pBig = (byte*)(void*)bigData.Scan0;

                int smallOffset = smallStride - smallBmp.Width * 3;
                int bigOffset = bigStride - bigBmp.Width * 3;

                bool matchFound = true;

                for (int y = 0; y < bigHeight; y++)
                {
                    for (int x = 0; x < bigWidth; x++)
                    {
                        byte* pBigBackup = pBig;
                        byte* pSmallBackup = pSmall;

                        //Look for the small picture.
                        for (int i = 0; i < smallHeight; i++)
                        {
                            int j = 0;
                            matchFound = true;
                            for (j = 0; j < smallWidth; j++)
                            {
                                //With tolerance: pSmall value should be between margins.
                                int inf = pBig[0] - margin;
                                int sup = pBig[0] + margin;
                                if (sup < pSmall[0] || inf > pSmall[0])
                                {
                                    matchFound = false;
                                    break;
                                }

                                pBig++;
                                pSmall++;
                            }

                            if (!matchFound) break;

                            //We restore the pointers.
                            pSmall = pSmallBackup;
                            pBig = pBigBackup;

                            //Next rows of the small and big pictures.
                            pSmall += smallStride * (1 + i);
                            pBig += bigStride * (1 + i);
                        }

                        //If match found, we return.
                        if (matchFound)
                        {
                            location.X = x;
                            location.Y = y;
                            location.Width = smallBmp.Width;
                            location.Height = smallBmp.Height;
                            break;
                        }
                        //If no match found, we restore the pointers and continue.
                        else
                        {
                            pBig = pBigBackup;
                            pSmall = pSmallBackup;
                            pBig += 3;
                        }
                    }

                    if (matchFound) break;

                    pBig += bigOffset;
                }
            }

            bigBmp.UnlockBits(bigData);
            smallBmp.UnlockBits(smallData);

            return location;
        }
        public static double per(double num, double pers)
        {
            return (num * pers) / 100;
        }
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);
        static Dictionary<string, IntPtr> GetRootWindowsOfProcess(int pid)
        {
            List<IntPtr> rootWindows = GetChildWindows(IntPtr.Zero);
            Dictionary<string, IntPtr> ds = new Dictionary<string, IntPtr>();
            int i = 0;
            foreach (IntPtr hWnd in rootWindows)
            {
                uint lpdwProcessId;
                GetWindowThreadProcessId(hWnd, out lpdwProcessId);
                if (lpdwProcessId == pid)
                {
                    StringBuilder o = new StringBuilder(10);
                    GetClassName(hWnd, o, 10);
                    try
                    {



                        ds.Add(o.ToString(), hWnd);
                    }
                    catch
                    {
                        i++;
                        ds.Add(o.ToString() + i.ToString(), hWnd);

                    }
                }
            }
            return ds;
        }

        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                Win32Callback childProc = new Win32Callback(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }
        public static Bitmap Capture()
        {
            IntPtr screen = (IntPtr)0x0000;
            var y = GetRootWindowsOfProcess(Process.GetProcessesByName("explorer")[0].Id);
            foreach (var item in y)
            {
                if (item.Key.Contains("WorkerW"))
                {
                    var r = NativeMethods.GetAbsoluteClientRect(item.Value);
                    if (r.X == 0)
                    {
                        screen = item.Value;
                        try
                        {
                            Direct3DCapture.CaptureWindow(screen);
                            break;

                        }
                        catch
                        {


                        }
                    }
                }
            }
            return Direct3DCapture.CaptureWindow(screen);

        }
        public static Rectangle rect()
        {
            IntPtr screen = (IntPtr)0x0000;
            var y = GetRootWindowsOfProcess(Process.GetProcessesByName("explorer")[0].Id);
            foreach (var item in y)
            {
                if (item.Key.Contains("WorkerW"))
                {
                    var r = NativeMethods.GetAbsoluteClientRect(item.Value);
                    if (r.X == 0)
                    {
                        screen = item.Value;
                        try
                        {
                            Direct3DCapture.CaptureWindow(screen);
                            break;

                        }
                        catch
                        {


                        }
                    }
                }
            }
            return NativeMethods.GetAbsoluteClientRect(screen);
        }
    }
    public static class Direct3DCapture
    {
        private static SlimDX.Direct3D9.Direct3D _direct3D9 = new SlimDX.Direct3D9.Direct3D();
        private static Dictionary<IntPtr, SlimDX.Direct3D9.Device> _direct3DDeviceCache = new Dictionary<IntPtr, SlimDX.Direct3D9.Device>();

        public static Bitmap CaptureWindow(IntPtr hWnd)
        {
            return CaptureRegionDirect3D(hWnd, NativeMethods.GetAbsoluteClientRect(hWnd));
        }

        public static Bitmap CaptureRegionDirect3D(IntPtr handle, Rectangle region)
        {
            IntPtr hWnd = handle;
            Bitmap bitmap = null;

            // We are only supporting the primary display adapter for Direct3D mode
            SlimDX.Direct3D9.AdapterInformation adapterInfo = _direct3D9.Adapters.DefaultAdapter;
            SlimDX.Direct3D9.Device device;

            #region Get Direct3D Device
            // Retrieve the existing Direct3D device if we already created one for the given handle
            if (_direct3DDeviceCache.ContainsKey(hWnd))
            {
                device = _direct3DDeviceCache[hWnd];
            }
            // We need to create a new device
            else
            {
                // Setup the device creation parameters
                SlimDX.Direct3D9.PresentParameters parameters = new SlimDX.Direct3D9.PresentParameters();
                parameters.BackBufferFormat = adapterInfo.CurrentDisplayMode.Format;
                Rectangle clientRect = NativeMethods.GetAbsoluteClientRect(hWnd);
                parameters.BackBufferHeight = clientRect.Height;
                parameters.BackBufferWidth = clientRect.Width;
                parameters.Multisample = SlimDX.Direct3D9.MultisampleType.None;
                parameters.SwapEffect = SlimDX.Direct3D9.SwapEffect.Discard;
                parameters.DeviceWindowHandle = hWnd;
                parameters.PresentationInterval = SlimDX.Direct3D9.PresentInterval.Default;
                parameters.FullScreenRefreshRateInHertz = 0;

                // Create the Direct3D device
                device = new SlimDX.Direct3D9.Device(_direct3D9, adapterInfo.Adapter, SlimDX.Direct3D9.DeviceType.Hardware, hWnd, SlimDX.Direct3D9.CreateFlags.SoftwareVertexProcessing, parameters);
                _direct3DDeviceCache.Add(hWnd, device);
            }
            #endregion

            // Capture the screen and copy the region into a Bitmap
            using (SlimDX.Direct3D9.Surface surface = SlimDX.Direct3D9.Surface.CreateOffscreenPlain(device, adapterInfo.CurrentDisplayMode.Width, adapterInfo.CurrentDisplayMode.Height, SlimDX.Direct3D9.Format.A8R8G8B8, SlimDX.Direct3D9.Pool.SystemMemory))
            {
                device.GetFrontBufferData(0, surface);

                // Update: thanks digitalutopia1 for pointing out that SlimDX have fixed a bug
                // where they previously expected a RECT type structure for their Rectangle
                bitmap = new Bitmap(SlimDX.Direct3D9.Surface.ToStream(surface, SlimDX.Direct3D9.ImageFileFormat.Jpg, new Rectangle(region.Left, region.Top, region.Width, region.Height)));
                // Previous SlimDX bug workaround: new Rectangle(region.Left, region.Top, region.Right, region.Bottom)));

            }

            return bitmap;
        }
    }

    #region Native Win32 Interop
    [Serializable, StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public Rectangle AsRectangle
        {
            get
            {
                return new Rectangle(this.Left, this.Top, this.Right - this.Left, this.Bottom - this.Top);
            }
        }

        public static RECT FromXYWH(int x, int y, int width, int height)
        {
            return new RECT(x, y, x + width, y + height);
        }

        public static RECT FromRectangle(Rectangle rect)
        {
            return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
    }

    [System.Security.SuppressUnmanagedCodeSecurity()]
    internal sealed class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        internal static Rectangle GetClientRect(IntPtr hwnd)
        {
            RECT rect = new RECT();
            GetClientRect(hwnd, out rect);
            return rect.AsRectangle;
        }

        internal static Rectangle GetWindowRect(IntPtr hwnd)
        {
            RECT rect = new RECT();
            GetWindowRect(hwnd, out rect);
            return rect.AsRectangle;
        }

        internal static Rectangle GetAbsoluteClientRect(IntPtr hWnd)
        {
            Rectangle windowRect = NativeMethods.GetWindowRect(hWnd);
            Rectangle clientRect = NativeMethods.GetClientRect(hWnd);

            // This gives us the width of the left, right and bottom chrome - we can then determine the top height
            int chromeWidth = (int)((windowRect.Width - clientRect.Width) / 2);

            return new Rectangle(new Point(windowRect.X + chromeWidth, windowRect.Y + (windowRect.Height - clientRect.Height - chromeWidth)), clientRect.Size);
        }
    }
    #endregion
}