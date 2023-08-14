namespace OS_Pomoreva
{
    partial class LR3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pieChart1 = new LiveCharts.WinForms.PieChart();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pieChart2 = new LiveCharts.WinForms.PieChart();
            this.label3 = new System.Windows.Forms.Label();
            this.pieChart3 = new LiveCharts.WinForms.PieChart();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 247);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация о памяти";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(6, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(418, 214);
            this.listBox1.TabIndex = 0;
            // 
            // pieChart1
            // 
            this.pieChart1.Location = new System.Drawing.Point(464, 34);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(319, 225);
            this.pieChart1.TabIndex = 1;
            this.pieChart1.Text = "Физическая память";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(440, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "Показать карту процессов";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(563, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Физическая память";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(913, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Файл подкачки";
            // 
            // pieChart2
            // 
            this.pieChart2.Location = new System.Drawing.Point(799, 31);
            this.pieChart2.Name = "pieChart2";
            this.pieChart2.Size = new System.Drawing.Size(319, 225);
            this.pieChart2.TabIndex = 4;
            this.pieChart2.Text = "Физическая память";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(739, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Виртуальная память";
            // 
            // pieChart3
            // 
            this.pieChart3.Location = new System.Drawing.Point(640, 298);
            this.pieChart3.Name = "pieChart3";
            this.pieChart3.Size = new System.Drawing.Size(319, 225);
            this.pieChart3.TabIndex = 6;
            this.pieChart3.Text = "Физическая память";
            // 
            // LR3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 543);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pieChart3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pieChart2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pieChart1);
            this.Controls.Add(this.groupBox1);
            this.Name = "LR3";
            this.Text = "Лабораторная работа 3";
            this.Load += new System.EventHandler(this.LR3_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private ListBox listBox1;
        private LiveCharts.WinForms.PieChart pieChart1;
        private Button button1;
        private Label label1;
        private Label label2;
        private LiveCharts.WinForms.PieChart pieChart2;
        private Label label3;
        private LiveCharts.WinForms.PieChart pieChart3;
    }
}