using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lol
{
    public partial class Main : Form
    {
        public class MouseOperations
        {
            [Flags]
            public enum MouseEventFlags
            {
                LeftDown = 0x00000002,
                LeftUp = 0x00000004,
                MiddleDown = 0x00000020,
                MiddleUp = 0x00000040,
                Move = 0x00000001,
                Absolute = 0x00008000,
                RightDown = 0x00000008,
                RightUp = 0x00000010
            }

            [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool SetCursorPos(int x, int y);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool GetCursorPos(out MousePoint lpMousePoint);

            [DllImport("user32.dll")]
            private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

            public static void SetCursorPosition(int x, int y)
            {
                SetCursorPos(x, y);
            }

            public static void SetCursorPosition(MousePoint point)
            {
                SetCursorPos(point.X, point.Y);
            }

            public static MousePoint GetCursorPosition()
            {
                MousePoint currentMousePoint;
                var gotPoint = GetCursorPos(out currentMousePoint);
                if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
                return currentMousePoint;
            }

            public static void MouseEvent(MouseEventFlags value)
            {
                MousePoint position = GetCursorPosition();

                mouse_event
                    ((int)value,
                     position.X,
                     position.Y,
                     0,
                     0)
                    ;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MousePoint
            {
                public int X;
                public int Y;

                public MousePoint(int x, int y)
                {
                    X = x;
                    Y = y;
                }
            }
        }
        public Main()
        {
            InitializeComponent();
        }
        Bitmap img = new Bitmap(new MemoryStream(Convert.FromBase64String("Qk32AQAAAAAAADYAAAAoAAAACAAAAA4AAAABACAAAAAAAMABAAAAAAAAAAAAAAAAAAAAAAAAEBIT/zE4Of9KTUr/CAQI/wkGB/8JBwf/DAwI/wwMCP8QEhP/MTg5/0pNSv8IBAj/GCSU/xgklP8YJJT/GCSU/xASE/8xODn/Sk1K/wgECP8YJJT/GCSU/xgklP8YJJT/EBIT/zE4Of9KTUr/CAQI/xgklP8YJJT/GCSU/xgklP8QEhP/MTg5/0pNSv8IBAj/KTSl/yEspf8hLKX/ISyl/xASE/8xODn/Sk1K/wgECP8pNKX/JjGn/yYxp/8mMaf/EBIT/zE4Of9KTUr/CAQI/yk0pf8sN6r/LDeq/yw3qv8QEhP/MTg5/0pNSv8IBAj/KTSl/zE8rf8xPK3/MTyt/xASE/8xODn/Sk1K/wgECP9aZc7/VWDI/1VgyP9VYMj/EBIT/zE4Of9KTUr/CAQI/1plzv9YYsv/WGLL/1hiy/8QEhP/MTg5/0pNSv8IBAj/WmXO/1plzv9aZc7/WmXO/xASE/8xODn/Sk1K/wgECP9aZc7/WmXO/1plzv9aZc7/CAgI/0RIRP9CREL/AAQA/wAEAP8ABAD/AAQA/wAEAP8mJSb/REhE/0JEQv9CREL/QkRC/0JEQv9CR0L/QkdC/wAAAAAAAAAAAAAAAAAA")));
        Bitmap acc = new Bitmap(new MemoryStream(Convert.FromBase64String("Qk32DAAAAAAAADYAAAAoAAAAMAAAABEAAAABACAAAAAAAMAMAAAAAAAAAAAAAAAAAAAAAAAAKiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/QT0x/ywnIP8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/ZWJR/yolHv8qJR7/LCcg/3Z0Xv+1tZT/xcWh/8fHo/++vpv/kpB2/z46L/8qJR7/ZWJP/76+m//Hx6P/x8ej/8fHo//Hx6P/xMSh/3p4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o/+bmn7/LCcg/5ybf//Hx6P/w8Og/317Zf8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/KiUe/yolHv8qJR7/AAAAAAAAAAAAAAAAAAA=")));
        const int ENUM_CURRENT_SETTINGS = -1;
        Size s = new Size();
        private void Form1_Load(object sender, EventArgs e)
        {
            shero.SelectedIndex = 0;
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            EnumDisplaySettings(Screen.PrimaryScreen.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);
            s.Width = dm.dmPelsWidth;
            s.Height = dm.dmPelsHeight;


        }



        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        private Rectangle searchBitmap(Bitmap smallBmp, Bitmap bigBmp, double tolerance)
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

        public static bool IsKeyDown(Keys key)
        {
            return (GetKeyState(Convert.ToInt16(key)) & 0X80) == 0X80;
        }
        /// <summary>
        ///  If the high-order bit is 1, the key is down; otherwise, it is up.
        ///  If the low-order bit is 1, the key is toggled. 
        ///  A key, such as the CAPS LOCK key, is toggled if it is turned on. 
        ///  The key is off and untoggled if the low-order bit is 0. 
        ///  A toggle key's indicator light (if any) on the keyboard will be on when 
        ///  the key is toggled, and off when the key is untoggled.
        /// </summary>
        /// <param name="nVirtKey"></param>
        [DllImport("user32.dll")]
        public extern static Int16 GetKeyState(Int16 nVirtKey);




        private void bts_Click(object sender, EventArgs e)
        {
            if (bts.Text == "Start")
            {

                shero.Enabled = false;
                bts.Text = "Stop";
                new Thread(() => {
                    while (true)
                {
                    switch (shero.SelectedIndex)
                    {
                        case 0:
                            if (IsKeyDown(Keys.Q))
                                 aim();
                            break;
                        case 1:
                            if (IsKeyDown(Keys.Q))
                                 aim();
                            if (IsKeyDown(Keys.E))
                                 aim();
                            if (IsKeyDown(Keys.R))
                                 aim();
                            break;
                        case 2:
                            if (IsKeyDown(Keys.Q))
                                 aim();
                            if (IsKeyDown(Keys.W))
                                 aim();
                            break;
                        case 3:
                            if (IsKeyDown(Keys.W))
                                 aim();
                            if (IsKeyDown(Keys.R))
                                 aim();
                            break;
                        case 4:
                            if (IsKeyDown(Keys.Q))
                                 aim();
                            if (IsKeyDown(Keys.E))
                                 aim();
                            break;
                        case 5:
                            if (IsKeyDown(Keys.Q))
                                 aim();

                            break;
                        case 6:
                            if (IsKeyDown(Keys.Q))
                                 aim();

                            break;
                        case 7:
                            if (IsKeyDown(Keys.Q))
                                 aim();
                            if (IsKeyDown(Keys.E))
                                 aim();
                            if (IsKeyDown(Keys.W))
                                 aim();
                            break;
                        case 8:

                            if (IsKeyDown(Keys.E))
                                 aim();
                            if (IsKeyDown(Keys.W))
                                 aim();
                            break;
                        case 9:
                            if (IsKeyDown(Keys.Q))
                                 aim();
                            if (IsKeyDown(Keys.R))
                                 aim();
                            if (IsKeyDown(Keys.W))
                                 aim();
                            break;
                        case 10:
                            if (IsKeyDown(Keys.Q))
                                 aim();
                            if (IsKeyDown(Keys.W))
                                 aim();
                            if (IsKeyDown(Keys.E))
                                  aim();
                            if (IsKeyDown(Keys.R))
                                  aim();

                            break;
                        case 11:
                            if (IsKeyDown(Keys.Q))
                                aim();

                            break;

                        default:
                            break;
                    }
                        //await Task.Delay(1);
                        Thread.Sleep(1);

                        if (bts.Text == "Start")
                        break;
                }
                })
                { IsBackground = true }.Start();
            }
            else
            {
                shero.Enabled = true;

                bts.Text = "Start";

            }
        }
        void aim()
        {
            Bitmap ScreenBmp;
            Rectangle gs;
            try
            {


                ScreenBmp = screen.Direct3DCapture.CaptureWindow(Process.GetProcessesByName("League of Legends")[0].MainWindowHandle);
                gs = screen.NativeMethods.GetAbsoluteClientRect(Process.GetProcessesByName("League of Legends")[0].MainWindowHandle);
            }
            catch
            {
               // await Task.Delay(100);
                return;

            }
            var r = searchBitmap(img, ScreenBmp, 0.2);
            if (!r.IsEmpty)
            {
                var t = r.Location;
               
                t.X += r.Width*8;
                t.Y += r.Height*6;
                Cursor.Position = t;

            }
            
           // await Task.Delay(100);
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            while (sscaps.Checked)
            {
                if (IsKeyDown(Keys.F4))
                {
                    bts.PerformClick();
                    await Task.Delay(100);

                }

                await Task.Delay(1);

            }
        }

        private void autoacc_CheckedChanged(object sender, EventArgs e)
        {
            new Thread(() => { 
            while (autoacc.Checked)
            {
                Bitmap ScreenBmp = screen.all.Capture();
                var r = searchBitmap(acc, ScreenBmp, 0.2);
                if (!r.IsEmpty)
                {
                    var t = r.Location;
                    t.X += 10;
                    t.Y += 10;
                    Cursor.Position = t;
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                        Thread.Sleep(100);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                        Thread.Sleep(100);
                        t.X -= 10;
                        t.Y -= 10;
                        t.X += r.Width * 8;
                        t.Y += r.Height * 6;
                        Cursor.Position = t;

                    }
                   
                    Thread.Sleep(500);

                }
            })
            { IsBackground = true }.Start();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GC.Collect();
        }
    }
}