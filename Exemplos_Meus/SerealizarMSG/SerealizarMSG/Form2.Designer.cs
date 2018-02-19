namespace SerealizarMSG
{
    partial class Form2
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
            this.simpleButtonBGM1 = new FGlobus.Componentes.WinForms.SimpleButtonBGM(this.components);
            this.SuspendLayout();
            // 
            // simpleButtonBGM1
            // 
            this.simpleButtonBGM1.AchouRegistro = true;
            this.simpleButtonBGM1.AssociaPanel = null;
            this.simpleButtonBGM1.Habilita = false;
            this.simpleButtonBGM1.Location = new System.Drawing.Point(75, 45);
            this.simpleButtonBGM1.Name = "simpleButtonBGM1";
            this.simpleButtonBGM1.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonBGM1.TabIndex = 0;
            this.simpleButtonBGM1.Text = "simpleButtonBGM1";
            this.simpleButtonBGM1.Click += new System.EventHandler(this.simpleButtonBGM1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.simpleButtonBGM1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private FGlobus.Componentes.WinForms.SimpleButtonBGM simpleButtonBGM1;

    }
}