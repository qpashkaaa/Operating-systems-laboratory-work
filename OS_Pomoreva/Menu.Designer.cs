namespace OS_Pomoreva
{
    partial class Menu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LR1_btn = new System.Windows.Forms.Button();
            this.LR2_btn = new System.Windows.Forms.Button();
            this.LR3_btn = new System.Windows.Forms.Button();
            this.LR4_btn = new System.Windows.Forms.Button();
            this.LR5_btn = new System.Windows.Forms.Button();
            this.LR6_btn = new System.Windows.Forms.Button();
            this.LR7_btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LR1_btn
            // 
            this.LR1_btn.Location = new System.Drawing.Point(12, 12);
            this.LR1_btn.Name = "LR1_btn";
            this.LR1_btn.Size = new System.Drawing.Size(360, 37);
            this.LR1_btn.TabIndex = 0;
            this.LR1_btn.Text = "Лабораторная работа 1";
            this.LR1_btn.UseVisualStyleBackColor = true;
            this.LR1_btn.Click += new System.EventHandler(this.LR1_btn_Click);
            // 
            // LR2_btn
            // 
            this.LR2_btn.Location = new System.Drawing.Point(12, 65);
            this.LR2_btn.Name = "LR2_btn";
            this.LR2_btn.Size = new System.Drawing.Size(360, 37);
            this.LR2_btn.TabIndex = 1;
            this.LR2_btn.Text = "Лабораторная работа 2 (решение в консоли)";
            this.LR2_btn.UseVisualStyleBackColor = true;
            this.LR2_btn.Click += new System.EventHandler(this.LR2_btn_Click);
            // 
            // LR3_btn
            // 
            this.LR3_btn.Location = new System.Drawing.Point(12, 120);
            this.LR3_btn.Name = "LR3_btn";
            this.LR3_btn.Size = new System.Drawing.Size(360, 37);
            this.LR3_btn.TabIndex = 2;
            this.LR3_btn.Text = "Лабораторная работа 3";
            this.LR3_btn.UseVisualStyleBackColor = true;
            this.LR3_btn.Click += new System.EventHandler(this.LR3_btn_Click);
            // 
            // LR4_btn
            // 
            this.LR4_btn.Location = new System.Drawing.Point(12, 175);
            this.LR4_btn.Name = "LR4_btn";
            this.LR4_btn.Size = new System.Drawing.Size(360, 37);
            this.LR4_btn.TabIndex = 3;
            this.LR4_btn.Text = "Лабораторная работа 4 (решение в консоли)";
            this.LR4_btn.UseVisualStyleBackColor = true;
            this.LR4_btn.Click += new System.EventHandler(this.LR4_btn_Click);
            // 
            // LR5_btn
            // 
            this.LR5_btn.Location = new System.Drawing.Point(12, 230);
            this.LR5_btn.Name = "LR5_btn";
            this.LR5_btn.Size = new System.Drawing.Size(170, 37);
            this.LR5_btn.TabIndex = 4;
            this.LR5_btn.Text = "ЛР5 (консоль с синхр.)";
            this.LR5_btn.UseVisualStyleBackColor = true;
            this.LR5_btn.Click += new System.EventHandler(this.LR5_btn_Click);
            // 
            // LR6_btn
            // 
            this.LR6_btn.Location = new System.Drawing.Point(12, 284);
            this.LR6_btn.Name = "LR6_btn";
            this.LR6_btn.Size = new System.Drawing.Size(360, 37);
            this.LR6_btn.TabIndex = 5;
            this.LR6_btn.Text = "Лабораторная работа 6 (решение в консоли)";
            this.LR6_btn.UseVisualStyleBackColor = true;
            this.LR6_btn.Click += new System.EventHandler(this.LR6_btn_Click);
            // 
            // LR7_btn
            // 
            this.LR7_btn.Location = new System.Drawing.Point(12, 338);
            this.LR7_btn.Name = "LR7_btn";
            this.LR7_btn.Size = new System.Drawing.Size(360, 37);
            this.LR7_btn.TabIndex = 6;
            this.LR7_btn.Text = "Лабораторная работа 7";
            this.LR7_btn.UseVisualStyleBackColor = true;
            this.LR7_btn.Click += new System.EventHandler(this.LR7_btn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(207, 230);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 37);
            this.button1.TabIndex = 7;
            this.button1.Text = "ЛР5 (консоль без синхр.)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 384);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LR7_btn);
            this.Controls.Add(this.LR6_btn);
            this.Controls.Add(this.LR5_btn);
            this.Controls.Add(this.LR4_btn);
            this.Controls.Add(this.LR3_btn);
            this.Controls.Add(this.LR2_btn);
            this.Controls.Add(this.LR1_btn);
            this.Name = "Menu";
            this.Text = "Меню";
            this.ResumeLayout(false);

        }

        #endregion

        private Button LR1_btn;
        private Button LR2_btn;
        private Button LR3_btn;
        private Button LR4_btn;
        private Button LR5_btn;
        private Button LR6_btn;
        private Button LR7_btn;
        private Button button1;
    }
}