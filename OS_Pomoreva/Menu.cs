using System.Diagnostics;

namespace OS_Pomoreva
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void LR1_btn_Click(object sender, EventArgs e)
        {
            LR1 LR1 = new LR1();
            LR1.ShowDialog();
        }

        private void LR2_btn_Click(object sender, EventArgs e)
        {
            using Process process = new Process();
            {
                process.StartInfo.FileName = "LR2Console.exe";
                process.StartInfo.WorkingDirectory = "LRConsole/LR2Console/";
                process.StartInfo.UseShellExecute = true;
                process.Start();
            };
        }

        private void LR3_btn_Click(object sender, EventArgs e)
        {
            LR3 LR3 = new LR3();
            LR3.ShowDialog();
        }

        private void LR4_btn_Click(object sender, EventArgs e)
        {
            Process.Start("LRConsole/LR4Console.exe", "");
        }

        private void LR5_btn_Click(object sender, EventArgs e)
        {
            Process.Start("LRConsole/LR5ConsoleWithSynchro/LR5Console.exe", "");
        }

        private void LR6_btn_Click(object sender, EventArgs e)
        {
            using Process process = new Process();
            {
                process.StartInfo.FileName = "LR6Console.exe";
                process.StartInfo.WorkingDirectory = "LRConsole/LR6Console/SendMessage(MainProcess)/";
                process.StartInfo.UseShellExecute = true;
                process.Start();
            };
        }

        private void LR7_btn_Click(object sender, EventArgs e)
        {
            LR7 LR7 = new LR7();
            LR7.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("LRConsole/LR5ConsoleWithoutSynchro/LR5Console.exe", "");
        }
    }
}