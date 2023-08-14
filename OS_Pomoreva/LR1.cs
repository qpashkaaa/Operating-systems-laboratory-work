using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static OS_Pomoreva.LR1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OS_Pomoreva
{
    public partial class LR1 : Form
    {
        // Определение имени ПК
        [DllImport("Kernel32")]
        static extern unsafe bool GetComputerName(byte* lpBuffer, long* nSize);

        // Определение UserName
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool GetUserName(System.Text.StringBuilder sb, ref Int32 length);

        // Пути к системным каталогам Windows
        [DllImport("kernel32.dll")]
        static extern uint GetSystemDirectory([Out] StringBuilder lpBuffer, uint uSize);

        // Версия ОС
        struct OSVERSIONINFO
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public UInt16 wServicePackMajor;
            public UInt16 wServicePackMinor;
            public UInt16 wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }
        [DllImport("kernel32")]
        static extern bool GetVersionEx(ref OSVERSIONINFO osvi);

        // 3 системные метрики
        public enum SystemMetric
        {
            SM_CXSCREEN = 0,  // 0x00
            SM_CYSCREEN = 1,  // 0x01
            SM_CXVSCROLL = 2,  // 0x02
            SM_CYHSCROLL = 3,  // 0x03
            SM_CYCAPTION = 4,  // 0x04
            SM_CXBORDER = 5,  // 0x05
            SM_CYBORDER = 6,  // 0x06
            SM_CXDLGFRAME = 7,  // 0x07
            SM_CXFIXEDFRAME = 7,  // 0x07
            SM_CYDLGFRAME = 8,  // 0x08
            SM_CYFIXEDFRAME = 8,  // 0x08
            SM_CYVTHUMB = 9,  // 0x09
            SM_CXHTHUMB = 10, // 0x0A
            SM_CXICON = 11, // 0x0B
            SM_CYICON = 12, // 0x0C
            SM_CXCURSOR = 13, // 0x0D
            SM_CYCURSOR = 14, // 0x0E
            SM_CYMENU = 15, // 0x0F
            SM_CXFULLSCREEN = 16, // 0x10
            SM_CYFULLSCREEN = 17, // 0x11
            SM_CYKANJIWINDOW = 18, // 0x12
            SM_MOUSEPRESENT = 19, // 0x13
            SM_CYVSCROLL = 20, // 0x14
            SM_CXHSCROLL = 21, // 0x15
            SM_DEBUG = 22, // 0x16
            SM_SWAPBUTTON = 23, // 0x17
            SM_CXMIN = 28, // 0x1C
            SM_CYMIN = 29, // 0x1D
            SM_CXSIZE = 30, // 0x1E
            SM_CYSIZE = 31, // 0x1F
            SM_CXSIZEFRAME = 32, // 0x20
            SM_CXFRAME = 32, // 0x20
            SM_CYSIZEFRAME = 33, // 0x21
            SM_CYFRAME = 33, // 0x21
            SM_CXMINTRACK = 34, // 0x22
            SM_CYMINTRACK = 35, // 0x23
            SM_CXDOUBLECLK = 36, // 0x24
            SM_CYDOUBLECLK = 37, // 0x25
            SM_CXICONSPACING = 38, // 0x26
            SM_CYICONSPACING = 39, // 0x27
            SM_MENUDROPALIGNMENT = 40, // 0x28
            SM_PENWINDOWS = 41, // 0x29
            SM_DBCSENABLED = 42, // 0x2A
            SM_CMOUSEBUTTONS = 43, // 0x2B
            SM_SECURE = 44, // 0x2C
            SM_CXEDGE = 45, // 0x2D
            SM_CYEDGE = 46, // 0x2E
            SM_CXMINSPACING = 47, // 0x2F
            SM_CYMINSPACING = 48, // 0x30
            SM_CXSMICON = 49, // 0x31
            SM_CYSMICON = 50, // 0x32
            SM_CYSMCAPTION = 51, // 0x33
            SM_CXSMSIZE = 52, // 0x34
            SM_CYSMSIZE = 53, // 0x35
            SM_CXMENUSIZE = 54, // 0x36
            SM_CYMENUSIZE = 55, // 0x37
            SM_ARRANGE = 56, // 0x38
            SM_CXMINIMIZED = 57, // 0x39
            SM_CYMINIMIZED = 58, // 0x3A
            SM_CXMAXTRACK = 59, // 0x3B
            SM_CYMAXTRACK = 60, // 0x3C
            SM_CXMAXIMIZED = 61, // 0x3D
            SM_CYMAXIMIZED = 62, // 0x3E
            SM_NETWORK = 63, // 0x3F
            SM_CLEANBOOT = 67, // 0x43
            SM_CXDRAG = 68, // 0x44
            SM_CYDRAG = 69, // 0x45
            SM_SHOWSOUNDS = 70, // 0x46
            SM_CXMENUCHECK = 71, // 0x47
            SM_CYMENUCHECK = 72, // 0x48
            SM_SLOWMACHINE = 73, // 0x49
            SM_MIDEASTENABLED = 74, // 0x4A
            SM_MOUSEWHEELPRESENT = 75, // 0x4B
            SM_XVIRTUALSCREEN = 76, // 0x4C
            SM_YVIRTUALSCREEN = 77, // 0x4D
            SM_CXVIRTUALSCREEN = 78, // 0x4E
            SM_CYVIRTUALSCREEN = 79, // 0x4F
            SM_CMONITORS = 80, // 0x50
            SM_SAMEDISPLAYFORMAT = 81, // 0x51
            SM_IMMENABLED = 82, // 0x52
            SM_CXFOCUSBORDER = 83, // 0x53
            SM_CYFOCUSBORDER = 84, // 0x54
            SM_TABLETPC = 86, // 0x56
            SM_MEDIACENTER = 87, // 0x57
            SM_STARTER = 88, // 0x58
            SM_SERVERR2 = 89, // 0x59
            SM_MOUSEHORIZONTALWHEELPRESENT = 91, // 0x5B
            SM_CXPADDEDBORDER = 92, // 0x5C
            SM_DIGITIZER = 94, // 0x5E
            SM_MAXIMUMTOUCHES = 95, // 0x5F

            SM_REMOTESESSION = 0x1000, // 0x1000
            SM_SHUTTINGDOWN = 0x2000, // 0x2000
            SM_REMOTECONTROL = 0x2001, // 0x2001


            SM_CONVERTIBLESLATEMODE = 0x2003,
            SM_SYSTEMDOCKED = 0x2004,
        }
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);

        // 3 системных параметра
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref bool pvParam, uint fWinIni);

        // Системные цвета
            // Определить  
        [DllImport("user32.dll")]
        static extern uint GetSysColor(int nIndex);

        private void GetColor3DLight()
        {
            const int COLOR_3DLIGHT = 22;
            int color3dLight = Convert.ToInt32(GetSysColor(COLOR_3DLIGHT));
            Color color = Color.FromArgb(color3dLight & 0xFF, (color3dLight & 0xFF00) >> 8, (color3dLight & 0xFF0000) >> 16);
            label14.Text += "[R = " + color.R + ", G = " + color.G + ", B = " + color.B + "]";
        }

        private void GetColorInActiveCaptionText()
        {
            const int COLOR_INACTIVECAPTIONTEXT = 19;
            int colorInActiveCaptionText = Convert.ToInt32(GetSysColor(COLOR_INACTIVECAPTIONTEXT));
            Color color = Color.FromArgb(colorInActiveCaptionText & 0xFF, (colorInActiveCaptionText & 0xFF00) >> 8, (colorInActiveCaptionText & 0xFF0000) >> 16);
            label21.Text += "[R = " + color.R + ", G = " + color.G + ", B = " + color.B + "]";
        }

        private void GetColorMenuText()
        {
            const int COLOR_MENUTEXT = 7;
            int colorMenuText = Convert.ToInt32(GetSysColor(COLOR_MENUTEXT));
            Color color = Color.FromArgb(colorMenuText & 0xFF, (colorMenuText & 0xFF00) >> 8, (colorMenuText & 0xFF0000) >> 16);
            label25.Text += "[R = " + color.R + ", G = " + color.G + ", B = " + color.B + "]";
        }
            // Задать
        [DllImport("user32.dll")]
        static extern bool SetSysColors(int cElements, int[] lpaElements, int[] lpaRgbValues);

        // Работа со временем
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            [MarshalAs(UnmanagedType.U2)] public short Year;
            [MarshalAs(UnmanagedType.U2)] public short Month;
            [MarshalAs(UnmanagedType.U2)] public short DayOfWeek;
            [MarshalAs(UnmanagedType.U2)] public short Day;
            [MarshalAs(UnmanagedType.U2)] public short Hour;
            [MarshalAs(UnmanagedType.U2)] public short Minute;
            [MarshalAs(UnmanagedType.U2)] public short Second;
            [MarshalAs(UnmanagedType.U2)] public short Milliseconds;

            public SYSTEMTIME(DateTime dt)
            {
                dt = dt.ToUniversalTime();  // SetSystemTime expects the SYSTEMTIME in UTC
                Year = (short)dt.Year;
                Month = (short)dt.Month;
                DayOfWeek = (short)dt.DayOfWeek;
                Day = (short)dt.Day;
                Hour = (short)dt.Hour;
                Minute = (short)dt.Minute;
                Second = (short)dt.Second;
                Milliseconds = (short)dt.Millisecond;
            }
        }
        [DllImport("kernel32.dll")]
        static extern void GetLocalTime(out SYSTEMTIME lpSystemTime);
        private void GetSystemLocalTime()
        {
            SYSTEMTIME systemtime;
            GetLocalTime(out systemtime);
            label29.Text += systemtime.Day + "/" + systemtime.Month + "/" + systemtime.Year + " "
                + systemtime.Hour + ":" + systemtime.Minute;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TIME_ZONE_INFORMATION
        {
            [MarshalAs(UnmanagedType.I4)]
            public Int32 Bias;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string StandardName;
            public SYSTEMTIME StandardDate;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 StandardBias;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DaylightName;
            public SYSTEMTIME DaylightDate;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 DaylightBias;
        }
        [DllImport("kernel32.dll")]
        static extern uint GetTimeZoneInformation(out TIME_ZONE_INFORMATION lpTimeZoneInformation);

        [DllImport("kernel32.dll")]
        static extern bool SetSystemTime(ref SYSTEMTIME time);

        // Дополнительные API функции
        public enum ExitWindows : uint
        {
            // ONE of the following five:
            LogOff = 0x00,
            ShutDown = 0x01,
            Reboot = 0x02,
            PowerOff = 0x08,
            RestartApps = 0x40,
            // plus AT MOST ONE of the following two:
            Force = 0x04,
            ForceIfHung = 0x10,
        }
        enum ShutdownReason : uint
        {
            MajorApplication = 0x00040000,
            MajorHardware = 0x00010000,
            MajorLegacyApi = 0x00070000,
            MajorOperatingSystem = 0x00020000,
            MajorOther = 0x00000000,
            MajorPower = 0x00060000,
            MajorSoftware = 0x00030000,
            MajorSystem = 0x00050000,

            MinorBlueScreen = 0x0000000F,
            MinorCordUnplugged = 0x0000000b,
            MinorDisk = 0x00000007,
            MinorEnvironment = 0x0000000c,
            MinorHardwareDriver = 0x0000000d,
            MinorHotfix = 0x00000011,
            MinorHung = 0x00000005,
            MinorInstallation = 0x00000002,
            MinorMaintenance = 0x00000001,
            MinorMMC = 0x00000019,
            MinorNetworkConnectivity = 0x00000014,
            MinorNetworkCard = 0x00000009,
            MinorOther = 0x00000000,
            MinorOtherDriver = 0x0000000e,
            MinorPowerSupply = 0x0000000a,
            MinorProcessor = 0x00000008,
            MinorReconfig = 0x00000004,
            MinorSecurity = 0x00000013,
            MinorSecurityFix = 0x00000012,
            MinorSecurityFixUninstall = 0x00000018,
            MinorServicePack = 0x00000010,
            MinorServicePackUninstall = 0x00000016,
            MinorTermSrv = 0x00000020,
            MinorUnstable = 0x00000006,
            MinorUpgrade = 0x00000003,
            MinorWMI = 0x00000015,

            FlagUserDefined = 0x40000000,
            FlagPlanned = 0x80000000
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ExitWindowsEx(ExitWindows uFlags, ShutdownReason dwReason);

        [DllImport("user32.dll")]
        static extern uint GetKBCodePage();

        [DllImport("kernel32.dll")]
        static extern uint GetSystemDefaultLCID();

        [DllImport("user32.dll")]
        static extern bool SetCaretBlinkTime(uint time);


        public LR1()
        {
            InitializeComponent();
        }

        private unsafe void LR1_Load(object sender, EventArgs e)
        {
            // Определение имени ПК
            byte[] buffer = new byte[512];
            long sizeNameComputer = buffer.Length;
            long* pSize = &sizeNameComputer;
            fixed (byte* pBuffer = buffer)
            {
                GetComputerName(pBuffer, pSize);
            }
            System.Text.Encoding textEnc = new System.Text.ASCIIEncoding();
            label1.Text = label1.Text + textEnc.GetString(buffer);

            // Определение UserName
            StringBuilder bufferUserName = new StringBuilder();
            int sizeUserName = 512;
            GetUserName(bufferUserName, ref sizeUserName);
            label2.Text = label2.Text + bufferUserName;

            // Пути к системным каталогам Windows
            StringBuilder sbSystemDir = new StringBuilder(256);
            GetSystemDirectory(sbSystemDir, 256);
            label3.Text = label3.Text + sbSystemDir;

            // Версия ОС
            OSVERSIONINFO osvi = new OSVERSIONINFO();
            osvi.dwOSVersionInfoSize = Marshal.SizeOf(osvi);
            GetVersionEx(ref osvi);
            label4.Text = label4.Text + osvi.dwBuildNumber;

            // 3 системные метрики
            label6.Text += GetSystemMetrics(SystemMetric.SM_CXCURSOR);
            label7.Text += GetSystemMetrics(SystemMetric.SM_CXSCREEN) + "x" + GetSystemMetrics(SystemMetric.SM_CYSCREEN);
            label8.Text += GetSystemMetrics(SystemMetric.SM_CMONITORS);

            // 3 системных параметра
            bool fontsAntialasing = false;
            const uint SPI_GETFONTSMOOTHING = 0x004A;
            SystemParametersInfo(SPI_GETFONTSMOOTHING, 0, ref fontsAntialasing, 0);
            if (fontsAntialasing == false)
                label10.Text += "Нет";
            else
                label10.Text += "Да";
            bool animations = false;
            const uint SPI_GETCLIENTAREAANIMATION = 0x1042;
            SystemParametersInfo(SPI_GETCLIENTAREAANIMATION, 0, ref animations, 0);
            if (animations == false)
                label11.Text += "Нет";
            else
                label11.Text += "Да";
            bool keyboardMouse = false;
            const uint SPI_GETKEYBOARDPREF = 0x0044;
            SystemParametersInfo(SPI_GETKEYBOARDPREF, 0, ref keyboardMouse, 0);
            if (keyboardMouse == false)
                label12.Text += "Нет";
            else
                label12.Text += "Да";

            // Системные цвета
            GetColor3DLight();
            GetColorInActiveCaptionText();
            GetColorMenuText();

            // Работа со временем
            GetSystemLocalTime();

            TIME_ZONE_INFORMATION tzi;
            GetTimeZoneInformation(out tzi);
            label28.Text += tzi.StandardName + "/" + tzi.Bias;

            // Дополнительные API функции
            label30.Text += GetKBCodePage();
            label27.Text += GetSystemDefaultLCID();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            const int COLOR_3DLIGHT = 22;
            Color newColor = Color.FromArgb(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value));
            int[] elements = { COLOR_3DLIGHT };
            int[] colors = { System.Drawing.ColorTranslator.ToWin32(newColor) };
            SetSysColors(elements.Length, elements, colors);
            label14.Text = "Светлый цвет трёхмерных элементов (COLOR_3DLIGHT): ";
            GetColor3DLight();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            const int COLOR_INACTIVECAPTIONTEXT = 19;
            Color newColor = Color.FromArgb(Convert.ToInt32(numericUpDown6.Value), Convert.ToInt32(numericUpDown5.Value), Convert.ToInt32(numericUpDown4.Value));
            int[] elements = { COLOR_INACTIVECAPTIONTEXT };
            int[] colors = { System.Drawing.ColorTranslator.ToWin32(newColor) };
            SetSysColors(elements.Length, elements, colors);
            label21.Text = "Цвет текста в неактивном заголовке (COLOR_INACTIVECAPTIONTEXT): ";
            GetColorInActiveCaptionText();
    }

        private void button3_Click(object sender, EventArgs e)
        {
            const int COLOR_MENUTEXT = 7;
            Color newColor = Color.FromArgb(Convert.ToInt32(numericUpDown9.Value), Convert.ToInt32(numericUpDown8.Value), Convert.ToInt32(numericUpDown7.Value));
            int[] elements = { COLOR_MENUTEXT };
            int[] colors = { System.Drawing.ColorTranslator.ToWin32(newColor) };
            SetSysColors(elements.Length, elements, colors);
            label25.Text = "Цвет текста меню(COLOR_MENUTEXT): ";
            GetColorMenuText();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SYSTEMTIME systime = new SYSTEMTIME();
            systime.Year = (short)dateTimePicker1.Value.Year;
            systime.Month = (short)dateTimePicker1.Value.Month;
            systime.DayOfWeek = (short)dateTimePicker1.Value.DayOfWeek;
            systime.Day = (short)dateTimePicker1.Value.Day;
            systime.Hour = (short)dateTimePicker2.Value.Hour;
            systime.Hour -= 3;
            systime.Minute = (short)dateTimePicker2.Value.Minute;
            systime.Second = (short)dateTimePicker2.Value.Second;
            systime.Milliseconds = (short)dateTimePicker2.Value.Millisecond;
            SetSystemTime(ref systime);
            label29.Text = "Текущая дата и время (GetLocalTime): ";
            GetSystemLocalTime();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExitWindowsEx(ExitWindows.LogOff, ShutdownReason.MajorOther | ShutdownReason.MinorOther);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SetCaretBlinkTime((uint)numericUpDown10.Value);
        }
    }
}
