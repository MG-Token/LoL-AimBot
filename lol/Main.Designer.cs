
namespace lol
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.bts = new System.Windows.Forms.Button();
            this.shero = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sscaps = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.autoacc = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // bts
            // 
            this.bts.Location = new System.Drawing.Point(12, 43);
            this.bts.Name = "bts";
            this.bts.Size = new System.Drawing.Size(75, 23);
            this.bts.TabIndex = 0;
            this.bts.Text = "Start";
            this.bts.UseVisualStyleBackColor = true;
            this.bts.Click += new System.EventHandler(this.bts_Click);
            // 
            // shero
            // 
            this.shero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shero.FormattingEnabled = true;
            this.shero.Items.AddRange(new object[] {
            "Master YI",
            "LUX",
            "Morgana",
            "Ashe",
            "Caitlyn",
            "Jax",
            "Teemo",
            "Pantheon",
            "Xin zhao",
            "Veigar",
            "Vel\'koz",
            "Sivir"});
            this.shero.Location = new System.Drawing.Point(121, 20);
            this.shero.Name = "shero";
            this.shero.Size = new System.Drawing.Size(121, 21);
            this.shero.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Your Selected Hero:";
            // 
            // sscaps
            // 
            this.sscaps.AutoSize = true;
            this.sscaps.Location = new System.Drawing.Point(12, 74);
            this.sscaps.Name = "sscaps";
            this.sscaps.Size = new System.Drawing.Size(182, 17);
            this.sscaps.TabIndex = 3;
            this.sscaps.Text = "Start/Stop when F4 key pressed!";
            this.sscaps.UseVisualStyleBackColor = true;
            this.sscaps.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "No Ban! Safe play!";
            // 
            // autoacc
            // 
            this.autoacc.AutoSize = true;
            this.autoacc.Location = new System.Drawing.Point(12, 97);
            this.autoacc.Name = "autoacc";
            this.autoacc.Size = new System.Drawing.Size(184, 17);
            this.autoacc.TabIndex = 5;
            this.autoacc.Text = "Auto accept when a mach is find!";
            this.autoacc.UseVisualStyleBackColor = true;
            this.autoacc.CheckedChanged += new System.EventHandler(this.autoacc_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 121);
            this.Controls.Add(this.autoacc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sscaps);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shero);
            this.Controls.Add(this.bts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoL AimBot (By @MGToken)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bts;
        private System.Windows.Forms.ComboBox shero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox sscaps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox autoacc;
        private System.Windows.Forms.Timer timer1;
    }
}

