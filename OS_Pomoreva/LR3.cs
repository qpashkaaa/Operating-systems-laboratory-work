using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_Pomoreva
{
    public partial class LR3 : Form
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MemoryStatus
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool GlobalMemoryStatusEx([In, Out] MemoryStatus lpBuffer);

            private uint dwLength;
            public uint MemoryLoad;
            public ulong TotalPhys;
            public ulong AvailPhys;
            public ulong TotalPageFile;
            public ulong AvailPageFile;
            public ulong TotalVirtual;
            public ulong AvailVirtual;
            public ulong AvailExtendedVirtual;

            private static volatile MemoryStatus singleton;
            private static readonly object syncroot = new object();

            public static MemoryStatus CreateInstance()
            {
                if (singleton == null)
                    lock (syncroot)
                        if (singleton == null)
                            singleton = new MemoryStatus();
                return singleton;
            }

            [SecurityCritical]
            private MemoryStatus()
            {
                dwLength = (uint)Marshal.SizeOf(typeof(MemoryStatus));
                GlobalMemoryStatusEx(this);
            }
        }

        public LR3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("LRConsole/LR3Console.exe", "");
        }

        private void LR3_Load(object sender, EventArgs e)
        {
            //List<double> yValues = new List<double>() { 55.62, 45.54, 73.45, 9.73, 88.42, 45.9, 63.6, 85.1, 67.2, 23.6 };
            MemoryStatus status = MemoryStatus.CreateInstance();
            uint MemoryLoad = status.MemoryLoad;
            ulong TotalPhys = status.TotalPhys;
            ulong AvailPhys = status.AvailPhys;
            ulong TotalPageFile = status.TotalPageFile;
            ulong AvailPageFile = status.AvailPageFile;
            ulong TotalVirtual = status.TotalVirtual;
            ulong AvailVirtual = status.AvailVirtual;
            ulong AvailExtendedVirtual = status.AvailExtendedVirtual;
            listBox1.Items.Add("Всего физической памяти= " + TotalPhys / 1024 / 1024 + " MB");
            listBox1.Items.Add("Объем физической памяти, доступный в данный момент " + AvailPhys / 1024 / 1024 + " MB");
            listBox1.Items.Add("Всего файл подкачки= " + TotalPageFile / 1024 / 1024 + " MB");
            listBox1.Items.Add("Объем файла подкачки, доступный в данный момент " + AvailPageFile / 1024 / 1024 + " MB");
            listBox1.Items.Add("Всего виртуальной памяти= " + TotalVirtual / 1024 / 1024 + " MB");
            listBox1.Items.Add("Объем виртуальной памяти, доступный в данный момент " + AvailVirtual / 1024 / 1024 + " MB");
            listBox1.Items.Add("Используется памяти данным процессом: " + MemoryLoad + "байт");
            SeriesCollection piechartData = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Занято – Мб",
                        Values = new ChartValues<double> {(TotalPhys / 1024 / 1024) - AvailPhys / 1024 / 1024},
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Свободно – Мб",
                        Values = new ChartValues<double> {AvailPhys / 1024 / 1024},
                        DataLabels = true,
                    },
                };
            pieChart1.Series = piechartData;
            pieChart1.LegendLocation = LegendLocation.Right;

            SeriesCollection piechartData2 = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Занято – Мб",
                        Values = new ChartValues<double> {(TotalPageFile / 1024 / 1024) - AvailPageFile / 1024 / 1024},
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Свободно – Мб",
                        Values = new ChartValues<double> {AvailPageFile / 1024 / 1024},
                        DataLabels = true,
                    },
                };
            pieChart2.Series = piechartData2;
            pieChart2.LegendLocation = LegendLocation.Right;

            SeriesCollection piechartData3 = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Занято – Мб",
                        Values = new ChartValues<double> {(TotalVirtual / 1024 / 1024) - AvailVirtual / 1024 / 1024},
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Свободно – Мб",
                        Values = new ChartValues<double> {AvailVirtual / 1024 / 1024},
                        DataLabels = true,
                    },
                };
            pieChart3.Series = piechartData3;
            pieChart3.LegendLocation = LegendLocation.Right;

        }
    }
}
