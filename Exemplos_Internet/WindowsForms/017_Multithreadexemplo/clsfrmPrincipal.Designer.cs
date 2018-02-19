namespace MultiThreadExemplo
{
    partial class clsfrmPrincipal
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
            this.textBoxEvento = new System.Windows.Forms.TextBox();
            this.butExecutarThread = new System.Windows.Forms.Button();
            this.butExecutarSemThread = new System.Windows.Forms.Button();
            this.butInteragirThreads = new System.Windows.Forms.Button();
            this.butNaoInteragirThreads = new System.Windows.Forms.Button();
            this.butSincronizarThread = new System.Windows.Forms.Button();
            this.butNaoSincronizarThread = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxEvento
            // 
            this.textBoxEvento.Location = new System.Drawing.Point(12, 28);
            this.textBoxEvento.Multiline = true;
            this.textBoxEvento.Name = "textBoxEvento";
            this.textBoxEvento.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxEvento.Size = new System.Drawing.Size(308, 380);
            this.textBoxEvento.TabIndex = 2;
            // 
            // butExecutarThread
            // 
            this.butExecutarThread.Location = new System.Drawing.Point(360, 57);
            this.butExecutarThread.Name = "butExecutarThread";
            this.butExecutarThread.Size = new System.Drawing.Size(135, 23);
            this.butExecutarThread.TabIndex = 10;
            this.butExecutarThread.Text = "Executar Thread";
            this.butExecutarThread.UseVisualStyleBackColor = true;
            this.butExecutarThread.Click += new System.EventHandler(this.butExecutarThread_Click);
            // 
            // butExecutarSemThread
            // 
            this.butExecutarSemThread.Location = new System.Drawing.Point(360, 28);
            this.butExecutarSemThread.Name = "butExecutarSemThread";
            this.butExecutarSemThread.Size = new System.Drawing.Size(135, 23);
            this.butExecutarSemThread.TabIndex = 9;
            this.butExecutarSemThread.Text = "Executar sem Thread";
            this.butExecutarSemThread.UseVisualStyleBackColor = true;
            this.butExecutarSemThread.Click += new System.EventHandler(this.butExecutarSemThread_Click);
            // 
            // butInteragirThreads
            // 
            this.butInteragirThreads.Location = new System.Drawing.Point(360, 134);
            this.butInteragirThreads.Name = "butInteragirThreads";
            this.butInteragirThreads.Size = new System.Drawing.Size(135, 23);
            this.butInteragirThreads.TabIndex = 12;
            this.butInteragirThreads.Text = "Interagir Threads";
            this.butInteragirThreads.UseVisualStyleBackColor = true;
            this.butInteragirThreads.Click += new System.EventHandler(this.butInteragirThreads_Click);
            // 
            // butNaoInteragirThreads
            // 
            this.butNaoInteragirThreads.Location = new System.Drawing.Point(360, 105);
            this.butNaoInteragirThreads.Name = "butNaoInteragirThreads";
            this.butNaoInteragirThreads.Size = new System.Drawing.Size(135, 23);
            this.butNaoInteragirThreads.TabIndex = 11;
            this.butNaoInteragirThreads.Text = "Não Interagir Threads";
            this.butNaoInteragirThreads.UseVisualStyleBackColor = true;
            this.butNaoInteragirThreads.Click += new System.EventHandler(this.butNaoInteragirThreads_Click);
            // 
            // butSincronizarThread
            // 
            this.butSincronizarThread.Location = new System.Drawing.Point(360, 207);
            this.butSincronizarThread.Name = "butSincronizarThread";
            this.butSincronizarThread.Size = new System.Drawing.Size(135, 23);
            this.butSincronizarThread.TabIndex = 14;
            this.butSincronizarThread.Text = "Sincronizar Thread";
            this.butSincronizarThread.UseVisualStyleBackColor = true;
            this.butSincronizarThread.Click += new System.EventHandler(this.butSincronizarThread_Click);
            // 
            // butNaoSincronizarThread
            // 
            this.butNaoSincronizarThread.Location = new System.Drawing.Point(360, 178);
            this.butNaoSincronizarThread.Name = "butNaoSincronizarThread";
            this.butNaoSincronizarThread.Size = new System.Drawing.Size(135, 23);
            this.butNaoSincronizarThread.TabIndex = 13;
            this.butNaoSincronizarThread.Text = "Não Sincronizar Thread";
            this.butNaoSincronizarThread.UseVisualStyleBackColor = true;
            this.butNaoSincronizarThread.Click += new System.EventHandler(this.butNaoSincronizarThread_Click);
            // 
            // clsfrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 432);
            this.Controls.Add(this.butNaoSincronizarThread);
            this.Controls.Add(this.textBoxEvento);
            this.Controls.Add(this.butInteragirThreads);
            this.Controls.Add(this.butSincronizarThread);
            this.Controls.Add(this.butNaoInteragirThreads);
            this.Controls.Add(this.butExecutarThread);
            this.Controls.Add(this.butExecutarSemThread);
            this.Name = "clsfrmPrincipal";
            this.Text = "MultiThreadExemplo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.clsfrmPrincipal_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxEvento;
        private System.Windows.Forms.Button butExecutarThread;
        private System.Windows.Forms.Button butExecutarSemThread;
        private System.Windows.Forms.Button butInteragirThreads;
        private System.Windows.Forms.Button butNaoInteragirThreads;
        private System.Windows.Forms.Button butSincronizarThread;
        private System.Windows.Forms.Button butNaoSincronizarThread;
    }
}

