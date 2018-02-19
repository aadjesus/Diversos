namespace PrintProject
{
    partial class PrintableUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintableUserControl));
            this.printingSystem = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            this.printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
            this.SuspendLayout();
            // 
            // printingSystem
            // 
            this.printingSystem.Links.AddRange(new object[] {
            this.printableComponentLink});
            // 
            // printableComponentLink
            // 
            this.printableComponentLink.CustomPaperSize = new System.Drawing.Size(0, 0);
            this.printableComponentLink.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("printableComponentLink.ImageStream")));
            this.printableComponentLink.PrintingSystem = this.printingSystem;
            this.printableComponentLink.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
            // 
            // PrintableUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "PrintableUserControl";
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPrinting.PrintingSystem printingSystem;
        private DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink;
    }
}
