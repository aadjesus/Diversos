namespace Imagens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.imagensBGM1 = new ClassLibrary1.ImagensBGM(this.components);
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.imagensBGM1)).BeginInit();
            this.SuspendLayout();
            // 
            // imagensBGM1
            // 
            this.imagensBGM1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imagensBGM1.ImageStream")));
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageList = this.imagensBGM1;
            this.simpleButton1.Location = new System.Drawing.Point(52, 46);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "simpleButton1";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(240, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.imagensBGM1;
            this.propertyGrid1.Size = new System.Drawing.Size(291, 376);
            this.propertyGrid1.TabIndex = 2;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 400);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.simpleButton1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.imagensBGM1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ClassLibrary1.ImagensBGM imagensBGM1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;

    }
}