using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts.Wpf;
using LiveCharts;
using System.Windows.Markup;
using OS_Pomoreva.Classes;
using OS_Pomoreva.Properties;
using LiveCharts.Wpf.Charts.Base;
using System.Runtime.CompilerServices;
using System.Windows.Shapes;

namespace OS_Pomoreva
{
    public partial class LR7 : Form
    {
        List<OSProgram> openApplications = new List<OSProgram>();
        List<OSProgram> CPUClockCyclesDataGridView = new List<OSProgram>();
        private int startClockCyclesValue = 0;
        private int countActiveApps = 0;

        private List<int> time = new List<int>();
        private int counterTime = 0;
        private ChartValues<int> workload = new ChartValues<int>();

        private int RAM = 8192;
        private int usingRam = 1500; // system's using 1500 MB ram
        private SeriesCollection piechartData = new SeriesCollection();
        private PieSeries freeRam = new PieSeries();

        private bool clickPictureBox = true;

        private void addListBoxInfo(OSProgram program)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox1.Items.Add("Наименование: " + program.name + ", Кол-во потоков: " + program.threads + ", Приоритет: " + program.priority);
            listBox1.Items.Add("Занимаемая память: " + program.memory + "(MB), " + program.clockCycle + " такт(ов)");
            listBox2.Items.Add(program.modules);
        }
        private void bubbleSortCPUClockCyclesDGV()
        {
            int index = 0;
            if (dataGridView1.RowCount > 0 && dataGridView1.Rows[0].Cells[0].Value != null)
            {
                index = 1;
                startClockCyclesValue = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value);
            }
            for (int i = index; i < CPUClockCyclesDataGridView.Count; i++)
            {
                for (int j = index; j < CPUClockCyclesDataGridView.Count - 1; j++)
                {
                    if (CPUClockCyclesDataGridView[j].priority > CPUClockCyclesDataGridView[j+1].priority)
                    {
                        OSProgram buff = CPUClockCyclesDataGridView[j];
                        CPUClockCyclesDataGridView[j] = CPUClockCyclesDataGridView[j + 1];
                        CPUClockCyclesDataGridView[j + 1] = buff;
                    }
                }
            }
            paintClockCyclesDGV();
        }

        private void paintClockCyclesDGV()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView3.Refresh();
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            dataGridView4.Refresh();
            Random rand = new Random();
            for (int i = 0; i < CPUClockCyclesDataGridView.Count; i++)
            {
                // dataGridView1
                DataGridViewProgressColumn progressCol = new DataGridViewProgressColumn(CPUClockCyclesDataGridView[i].clockCycle);
                progressCol.CellTemplate.Value = CPUClockCyclesDataGridView[i].clockCycle;
                progressCol.ProgressBarColor = Color.Yellow;
                progressCol.Width = CPUClockCyclesDataGridView[i].clockCycle * 15;
                dataGridView1.Columns.Add(progressCol);
                dataGridView1.Rows[0].Cells[i] = progressCol.CellTemplate;

                // dataGridView3
                DataGridViewImageColumn imageCol2 = new DataGridViewImageColumn();
                imageCol2.Width = progressCol.Width;
                dataGridView3.Columns.Add(imageCol2);
                imageCol2.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dataGridView3.Rows[0].Cells[i].Value = CPUClockCyclesDataGridView[i].icon;

                // dataGridView4
                dataGridView4.Columns.Add("appText", "HeaderAppText");
                dataGridView4.Rows.Add();
                dataGridView4.Columns[i].Width = progressCol.Width;
                dataGridView4.Columns[i].ReadOnly = true;
                dataGridView4.Rows[0].Cells[i].Value = CPUClockCyclesDataGridView[i].name;
                dataGridView4.Rows[1].Cells[i].Value = CPUClockCyclesDataGridView[i].priority;
            }
            if (startClockCyclesValue != 0)
            {
                if (CPUClockCyclesDataGridView[0].name == "INTERRUPT")
                {
                    int i = 0;
                    while (CPUClockCyclesDataGridView[i].name == "INTERRUPT")
                        i += 1;
                    dataGridView1.Rows[0].Cells[i].Value = startClockCyclesValue;
                }
                else
                    dataGridView1.Rows[0].Cells[0].Value = startClockCyclesValue;
            }
        }

        private void addDGVColumnRow(Bitmap image)
        {
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            dataGridView2.Columns.Add(imageCol);
            imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView2.Rows[0].Cells[countActiveApps].Value = image;
            foreach (DataGridViewColumn column in dataGridView2.Columns)
                column.Width = 40;
            dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[countActiveApps];
        }

        private void addNewActiveApp(OSProgram program)
        {
            if (RAM-usingRam-program.memory <= 0)
            {
                MessageBox.Show(
                    "Невозможно отрыть приложение, не хватает ОЗУ",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                openApplications.Add(program);
                CPUClockCyclesDataGridView.Add(program);
                piechartData.Add(new PieSeries
                {
                    Title = program.name,
                    Values = new ChartValues<int> { program.memory },
                    DataLabels = true
                });
                usingRam += program.memory;
                freeRam.Values = new ChartValues<int> { RAM - usingRam };
                freeRam.Title = "Свободно";

                addListBoxInfo(program);
                addDGVColumnRow(program.icon);
                bubbleSortCPUClockCyclesDGV();

                dataGridView3.BringToFront();
                dataGridView1.BringToFront();
                countActiveApps += 1;
            }
        }

        private void updatePieChart()
        {
            int usingRam = 1500;
            piechartData.Clear();
            piechartData.Add(
                new PieSeries
                {
                    Title = "Система",
                    Values = new ChartValues<int> { 1500 },
                    DataLabels = true
                });
            int tempOpenAppsRam = 0;
            foreach (var program in openApplications)
            {
                piechartData.Add(
                new PieSeries
                {
                    Title = program.name,
                    Values = new ChartValues<int> { program.memory },
                    DataLabels = true
                });
                tempOpenAppsRam += program.memory;
            }
            usingRam = usingRam + tempOpenAppsRam;
            freeRam.Title = "Свободно";
            freeRam.Values = new ChartValues<int> { RAM - usingRam };
            freeRam.DataLabels = true;
            piechartData.Add(freeRam);
            pieChart1.Series = piechartData;
            pieChart1.LegendLocation = LegendLocation.Right;
        }

        private void setDefaultPieChart()
        {
            piechartData.Clear();
            piechartData.Add(
                new PieSeries
                {
                    Title = "Система",
                    Values = new ChartValues<int> { 1500 },
                    DataLabels = true
                });
            freeRam.Title = "Свободно";
            freeRam.Values = new ChartValues<int> { RAM - usingRam };
            freeRam.DataLabels = true;
            piechartData.Add(freeRam);
            pieChart1.Series = piechartData;
            pieChart1.LegendLocation = LegendLocation.Right;
        }

        public LR7()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (clickPictureBox == true)
            {
                OSProgram chrome = new OSProgram("Google Chrome", 11, 145, 4, 4, new Bitmap(Resources.google_icon), "agentpsh.dll, advapi32.dll, apphelp.dll, appwiz.cpl, atl.dll, browseui.dll, btmmhook.dll, btncopy.dll, btneighborhood.dll, clbcatq.dll, comdlg32.dll, comres.dll, credui.dll, crypt32.dll, cryptdll.dll, cryptext.dll, cryptnet.dll");
                addNewActiveApp(chrome);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (clickPictureBox == true)
            {
                OSProgram discord = new OSProgram("Discord", 16, 188, 8, 5, new Bitmap(Resources.discord_icon), "hkvolkey.dll, grooveex.dll, mlshext.dll, msohevi.dll, olkfstub.dll, onfilter.dll, visshe.dll, grooveintlresource.dll, shellext.dllwabfind.dll, syntpcpl.dll, btkeyind.dll, wzshlstb.dll");
                addNewActiveApp(discord);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (clickPictureBox == true)
            {
                OSProgram excel = new OSProgram("Microsoft Excel", 3, 111, 5, 8, new Bitmap(Resources.excel_icon), "hnetcfg.dll, hticons.dll, icmui.dll, ieframe.dll, iertutil.dll, imagehlp.dll, imm32.dll, iphlpapi.dll, kernel32.dll, linkinfo.dll, midimap.dll, mmcshext.dll, msacm32.dll, sacm32.drv, msasn1.dll");
                addNewActiveApp(excel);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (clickPictureBox == true)
            {
                OSProgram photoshop = new OSProgram("Photoshop", 22, 567, 14, 2, new Bitmap(Resources.photoshop_icon), "rsaenh.dll, rsvpsp.dll, rtutils.dll, samlib.dll, secur32.dll, sendmail.dll, sensapi.dll, setupapi.dll");
                addNewActiveApp(photoshop);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (clickPictureBox == true)
            {
                OSProgram plSql = new OSProgram("Oracle SQL Developer", 4, 24, 3, 9, new Bitmap(Resources.plsqldev_icon), "rsaenh.dll, rsvpsp.dll, rtutils.dll, samlib.dll, secur32.dll, sendmail.dll, sensapi.dll, setupapi.dll");
                addNewActiveApp(plSql);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (clickPictureBox == true)
            {
                OSProgram visualStudio = new OSProgram("Visual Studio", 30, 1275, 17, 1, new Bitmap(Resources.visualstudio_icon), "cryptui.dll, cscdll.dll, cscui.dll, devenum.dll, dfshim.dll, dfsshlex.dll, dnsapi.dll, docprop2.dll, dot3api.dll, dot3dlg.dll, dsquery.dll, dsuiext.dll, eappcfg.dll, eappprxy.dll, extmgr.dll, gdi32.dll");
                addNewActiveApp(visualStudio);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (clickPictureBox == true)
            {
                OSProgram word = new OSProgram("Microsoft Word", 3, 102, 5, 8, new Bitmap(Resources.word_icon), "hnetcfg.dll, hticons.dll, icmui.dll, ieframe.dll, iertutil.dll, imagehlp.dll, imm32.dll, iphlpapi.dll, kernel32.dll, linkinfo.dll, midimap.dll, mmcshext.dll, msacm32.dll, sacm32.drv, msasn1.dll");
                addNewActiveApp(word);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (clickPictureBox == true)
            {
                OSProgram yandex = new OSProgram("Yandex Browser", 11, 145, 4, 3, new Bitmap(Resources.yandex_icon), "agentpsh.dll, advapi32.dll, apphelp.dll, appwiz.cpl, atl.dll, browseui.dll, btmmhook.dll, btncopy.dll, btneighborhood.dll, clbcatq.dll, comdlg32.dll, comres.dll, credui.dll, crypt32.dll, cryptdll.dll, cryptext.dll, cryptnet.dll");
                addNewActiveApp(yandex);
            }
        }

        private void LR7_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += new System.EventHandler(timer1_Tick);
            timer1.Start();
            System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 1000;
            timer2.Tick += new System.EventHandler(timer1_ChangeAppClockCount);
            timer2.Start();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Время(сек.)",
                MinValue = 0,
                MaxValue = 30
            });

            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Загруженность(%)",
                MinValue = 0,
                MaxValue = 100
            });
            LineSeries line = new LineSeries()
            {
                Values = workload
            };
            SeriesCollection series = new SeriesCollection();
            series.Add(line);
            cartesianChart1.Series = series;

            setDefaultPieChart();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            counterTime += 1;
            if (counterTime > 30)
            {
               time.Clear();
               workload.Clear();
               counterTime = 0;
            }
            else
            {
                time.Add(counterTime);
                Random rand = new Random();
                if (openApplications.Count == 0)
                    workload.Add(rand.Next(0, 5));
                else if (openApplications.Count <= 5)
                    workload.Add(rand.Next(5, 40));
                else if (openApplications.Count > 5 && openApplications.Count <=10)
                    workload.Add(rand.Next(40, 80));
                else if (openApplications.Count > 10)
                    workload.Add(rand.Next(95, 100));
            }
        }

        private void timer1_ChangeAppClockCount(object sender, EventArgs e)
        {
            if (CPUClockCyclesDataGridView.Count != 0)
            {
                if (Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value) > 1)
                {
                    dataGridView1.Rows[0].Cells[0].Value = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value) - 1;
                }
                else
                {
                    string colName = dataGridView1.Columns[0].Name;
                    dataGridView1.Columns.Remove(colName);
                    colName = dataGridView3.Columns[0].Name;
                    dataGridView3.Columns.Remove(colName);
                    colName = dataGridView4.Columns[0].Name;
                    dataGridView4.Columns.Remove(colName);
                    if (CPUClockCyclesDataGridView[0].name != "INTERRUPT")
                    {
                        CPUClockCyclesDataGridView.Add(CPUClockCyclesDataGridView[0]);
                        startClockCyclesValue = 0;
                    }
                    CPUClockCyclesDataGridView.RemoveAt(0);
                    if (CPUClockCyclesDataGridView[0].name != "INTERRUPT")
                    {
                        clickPictureBox = true;
                    }
                    paintClockCyclesDGV();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SeriesCollection piechartData = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Система",
                        Values = new ChartValues<int> {23},
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Файл подкачки",
                        Values = new ChartValues<int> {16},
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Приложения",
                        Values = new ChartValues<int> {265},
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Свободно",
                        Values = new ChartValues<int> {696},
                        DataLabels = true,
                    },
                };
            pieChart1.Series = piechartData;
            pieChart1.LegendLocation = LegendLocation.Right;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            updatePieChart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OSProgram interrupt = new OSProgram("INTERRUPT", 2, new Bitmap(Resources.interrupt_icon));
            CPUClockCyclesDataGridView.Add(interrupt);
            for (int i = CPUClockCyclesDataGridView.Count - 1; i >= 1; i--)
            {
                CPUClockCyclesDataGridView[i] = CPUClockCyclesDataGridView[i - 1];
            }
            CPUClockCyclesDataGridView[0] = interrupt;
            for (int i = 0; i < CPUClockCyclesDataGridView.Count; i++)
            {
                if (CPUClockCyclesDataGridView[i].name != "INTERRUPT")
                {
                    startClockCyclesValue = Convert.ToInt32(dataGridView1.Rows[0].Cells[i-1].Value);
                    i = CPUClockCyclesDataGridView.Count;
                }
            }
            clickPictureBox = false;
            paintClockCyclesDGV();
        }

        private void dataGridView4_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridView1.HorizontalScrollingOffset = dataGridView4.HorizontalScrollingOffset;
            dataGridView3.HorizontalScrollingOffset = dataGridView4.HorizontalScrollingOffset;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView3.ClearSelection();
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView4.ClearSelection();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Thread.Sleep(100);
            OSProgram program = openApplications[dataGridView2.CurrentCell.ColumnIndex];
            addListBoxInfo(program);
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < CPUClockCyclesDataGridView.Count; i++)
            {
                if (CPUClockCyclesDataGridView[i].name == openApplications[dataGridView2.CurrentCell.ColumnIndex].name)
                {
                    CPUClockCyclesDataGridView.RemoveAt(i);
                    dataGridView1.Columns.RemoveAt(i);
                    dataGridView3.Columns.RemoveAt(i);
                    dataGridView4.Columns.RemoveAt(i);
                    i = CPUClockCyclesDataGridView.Count;
                }
            }
            openApplications.RemoveAt(dataGridView2.CurrentCell.ColumnIndex);
            dataGridView2.Columns.RemoveAt(dataGridView2.CurrentCell.ColumnIndex);

            if (dataGridView2.CurrentCell != null) 
            {
                OSProgram program = openApplications[dataGridView2.CurrentCell.ColumnIndex];
                addListBoxInfo(program);
                usingRam -= program.memory;
                updatePieChart();
            }
            else
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Refresh();
                dataGridView3.Rows.Clear();
                dataGridView3.Columns.Clear();
                dataGridView3.Refresh();
                dataGridView4.Rows.Clear();
                dataGridView4.Columns.Clear();
                dataGridView4.Refresh();
                usingRam = 1500;
                startClockCyclesValue = 0;
                setDefaultPieChart();
            }
            countActiveApps -= 1;
        }
    }
}
