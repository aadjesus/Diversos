namespace Globus5.WPF.Sistemas.GestaoDeFrotaOnline.Resultados
{

#pragma warning disable 108,168,414,420,429,465,467,618,649,675,1058,1060,1591,1598,1607,1610,1616,1658,1683,1685,1690,1691,1699,1700,1701,1762,1956,3003,3007,3009

    partial class HistoricoDeDeteccoes
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
            this.button1 = new System.Windows.Forms.Button();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.smplBtnImprimir = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 47);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(728, 574);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // smplBtnImprimir
            // 
            this.smplBtnImprimir.Enabled = false;
            this.smplBtnImprimir.Location = new System.Drawing.Point(174, 12);
            this.smplBtnImprimir.Name = "smplBtnImprimir";
            this.smplBtnImprimir.Size = new System.Drawing.Size(75, 23);
            this.smplBtnImprimir.TabIndex = 2;
            this.smplBtnImprimir.Text = "Imprimir";
            this.smplBtnImprimir.UseVisualStyleBackColor = true;
            this.smplBtnImprimir.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(93, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Limpar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.smplBtnImprimir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 47);
            this.panel1.TabIndex = 4;
            // 
            // HistoricoDeDeteccoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 621);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.panel1);
            this.Name = "HistoricoDeDeteccoes";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        public System.Windows.Forms.Button smplBtnImprimir;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
    }

#pragma warning restore 108,168,414,420,429,465,467,618,649,675,1058,1060,1591,1598,1607,1610,1616,1658,1683,1685,1690,1691,1699,1700,1701,1762,1956,3003,3007,3009

}

