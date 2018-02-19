namespace Traffic_v1
{
    partial class TrafficConsumer
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
            this.EdtUf = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EdtCidade = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EdtIdCorredor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EdtPag = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.EdtRegPag = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.LbCount = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "getInfo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EdtUf
            // 
            this.EdtUf.Location = new System.Drawing.Point(209, 14);
            this.EdtUf.Name = "EdtUf";
            this.EdtUf.Size = new System.Drawing.Size(100, 20);
            this.EdtUf.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "UF";
            // 
            // EdtCidade
            // 
            this.EdtCidade.Location = new System.Drawing.Point(209, 40);
            this.EdtCidade.Name = "EdtCidade";
            this.EdtCidade.Size = new System.Drawing.Size(100, 20);
            this.EdtCidade.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Cidade";
            // 
            // EdtIdCorredor
            // 
            this.EdtIdCorredor.Location = new System.Drawing.Point(209, 66);
            this.EdtIdCorredor.Name = "EdtIdCorredor";
            this.EdtIdCorredor.Size = new System.Drawing.Size(100, 20);
            this.EdtIdCorredor.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "IdCorredor";
            // 
            // EdtPag
            // 
            this.EdtPag.Location = new System.Drawing.Point(449, 17);
            this.EdtPag.Name = "EdtPag";
            this.EdtPag.Size = new System.Drawing.Size(100, 20);
            this.EdtPag.TabIndex = 7;
            this.EdtPag.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(382, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "pag";
            // 
            // EdtRegPag
            // 
            this.EdtRegPag.Location = new System.Drawing.Point(449, 43);
            this.EdtRegPag.Name = "EdtRegPag";
            this.EdtRegPag.Size = new System.Drawing.Size(100, 20);
            this.EdtRegPag.TabIndex = 9;
            this.EdtRegPag.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(382, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "rec por pag";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 229);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(591, 264);
            this.listBox1.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Número de registros";
            // 
            // LbCount
            // 
            this.LbCount.AutoSize = true;
            this.LbCount.Location = new System.Drawing.Point(133, 204);
            this.LbCount.Name = "LbCount";
            this.LbCount.Size = new System.Drawing.Size(13, 13);
            this.LbCount.TabIndex = 13;
            this.LbCount.Text = "0";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "getCorridor";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "getExcerpt";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(186, 108);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(134, 23);
            this.button4.TabIndex = 16;
            this.button4.Text = "getAllCongested";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 137);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(134, 23);
            this.button5.TabIndex = 17;
            this.button5.Text = "getExcerptsCongested";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 108);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(134, 23);
            this.button6.TabIndex = 18;
            this.button6.Text = "getCorridorCongested";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // TrafficConsumer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 507);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.LbCount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.EdtRegPag);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EdtPag);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.EdtIdCorredor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EdtCidade);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EdtUf);
            this.Controls.Add(this.button1);
            this.Name = "TrafficConsumer";
            this.Text = "TrafficConsumer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox EdtUf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EdtCidade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EdtIdCorredor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox EdtPag;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox EdtRegPag;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LbCount;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

