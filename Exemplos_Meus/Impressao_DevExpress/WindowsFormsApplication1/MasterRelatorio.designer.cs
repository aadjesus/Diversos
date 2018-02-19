namespace FGlobus.Relatorios
{
    partial class MasterRelatorio
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterRelatorio));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.mstRelPageHeaderOperacional = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.mstRelParam = new DevExpress.XtraReports.UI.XRRichText();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.mstRelNomeOperacional = new DevExpress.XtraReports.UI.XRLabel();
            this.mstRelPageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.mstRelPagina = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.mstRelTopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.mstRelParamGerencial = new DevExpress.XtraReports.UI.XRRichText();
            this.mstRelLogoCliente = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLine5 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
            this.mstRelNomeGerencial = new DevExpress.XtraReports.UI.XRLabel();
            this.mstRelBottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.mstRelLogoBgm = new DevExpress.XtraReports.UI.XRPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mstRelParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mstRelParamGerencial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // mstRelPageHeaderOperacional
            // 
            this.mstRelPageHeaderOperacional.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.mstRelParam,
            this.xrLine1,
            this.mstRelNomeOperacional});
            this.mstRelPageHeaderOperacional.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mstRelPageHeaderOperacional.Height = 92;
            this.mstRelPageHeaderOperacional.Name = "mstRelPageHeaderOperacional";
            this.mstRelPageHeaderOperacional.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.mstRelPageHeaderOperacional.StylePriority.UseFont = false;
            this.mstRelPageHeaderOperacional.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // mstRelParam
            // 
            this.mstRelParam.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mstRelParam.Location = new System.Drawing.Point(0, 38);
            this.mstRelParam.Name = "mstRelParam";
            this.mstRelParam.SerializableRtfString = resources.GetString("mstRelParam.SerializableRtfString");
            this.mstRelParam.Size = new System.Drawing.Size(750, 51);
            this.mstRelParam.StylePriority.UseFont = false;
            // 
            // xrLine1
            // 
            this.xrLine1.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.xrLine1.LineWidth = 2;
            this.xrLine1.Location = new System.Drawing.Point(0, 25);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.Size = new System.Drawing.Size(750, 8);
            this.xrLine1.StylePriority.UseForeColor = false;
            // 
            // mstRelNomeOperacional
            // 
            this.mstRelNomeOperacional.BorderColor = System.Drawing.Color.DimGray;
            this.mstRelNomeOperacional.Font = new System.Drawing.Font("Nina", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mstRelNomeOperacional.ForeColor = System.Drawing.Color.DimGray;
            this.mstRelNomeOperacional.Location = new System.Drawing.Point(0, 0);
            this.mstRelNomeOperacional.Name = "mstRelNomeOperacional";
            this.mstRelNomeOperacional.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.mstRelNomeOperacional.Size = new System.Drawing.Size(750, 25);
            this.mstRelNomeOperacional.StylePriority.UseBorderColor = false;
            this.mstRelNomeOperacional.StylePriority.UseFont = false;
            this.mstRelNomeOperacional.StylePriority.UseForeColor = false;
            this.mstRelNomeOperacional.StylePriority.UseTextAlignment = false;
            this.mstRelNomeOperacional.Text = "Nome do Relatório";
            this.mstRelNomeOperacional.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // mstRelPageFooter
            // 
            this.mstRelPageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.mstRelPagina,
            this.xrLine2});
            this.mstRelPageFooter.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mstRelPageFooter.Height = 30;
            this.mstRelPageFooter.Name = "mstRelPageFooter";
            this.mstRelPageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.mstRelPageFooter.StylePriority.UseFont = false;
            this.mstRelPageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // mstRelPagina
            // 
            this.mstRelPagina.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mstRelPagina.ForeColor = System.Drawing.Color.DimGray;
            this.mstRelPagina.Format = "Página {0}/{1}";
            this.mstRelPagina.Location = new System.Drawing.Point(650, 0);
            this.mstRelPagina.Name = "mstRelPagina";
            this.mstRelPagina.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.mstRelPagina.Size = new System.Drawing.Size(100, 25);
            this.mstRelPagina.StylePriority.UseFont = false;
            this.mstRelPagina.StylePriority.UseForeColor = false;
            this.mstRelPagina.StylePriority.UseTextAlignment = false;
            this.mstRelPagina.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLine2
            // 
            this.xrLine2.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.xrLine2.LineWidth = 2;
            this.xrLine2.Location = new System.Drawing.Point(0, 0);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.Size = new System.Drawing.Size(750, 8);
            this.xrLine2.StylePriority.UseForeColor = false;
            // 
            // mstRelTopMargin
            // 
            this.mstRelTopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.mstRelParamGerencial,
            this.mstRelLogoCliente,
            this.xrLine5,
            this.xrLine4,
            this.mstRelNomeGerencial});
            this.mstRelTopMargin.Height = 125;
            this.mstRelTopMargin.Name = "mstRelTopMargin";
            // 
            // mstRelParamGerencial
            // 
            this.mstRelParamGerencial.Location = new System.Drawing.Point(0, 92);
            this.mstRelParamGerencial.Name = "mstRelParamGerencial";
            this.mstRelParamGerencial.SerializableRtfString = resources.GetString("mstRelParamGerencial.SerializableRtfString");
            this.mstRelParamGerencial.Size = new System.Drawing.Size(750, 33);
            // 
            // mstRelLogoCliente
            // 
            this.mstRelLogoCliente.Location = new System.Drawing.Point(0, 0);
            this.mstRelLogoCliente.Name = "mstRelLogoCliente";
            this.mstRelLogoCliente.Size = new System.Drawing.Size(150, 83);
            this.mstRelLogoCliente.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrLine5
            // 
            this.xrLine5.ForeColor = System.Drawing.Color.DimGray;
            this.xrLine5.LineWidth = 7;
            this.xrLine5.Location = new System.Drawing.Point(0, 81);
            this.xrLine5.Name = "xrLine5";
            this.xrLine5.Size = new System.Drawing.Size(750, 8);
            this.xrLine5.StylePriority.UseForeColor = false;
            // 
            // xrLine4
            // 
            this.xrLine4.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.xrLine4.LineWidth = 7;
            this.xrLine4.Location = new System.Drawing.Point(150, 67);
            this.xrLine4.Name = "xrLine4";
            this.xrLine4.Size = new System.Drawing.Size(598, 8);
            this.xrLine4.StylePriority.UseForeColor = false;
            // 
            // mstRelNomeGerencial
            // 
            this.mstRelNomeGerencial.Font = new System.Drawing.Font("Nina", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mstRelNomeGerencial.ForeColor = System.Drawing.Color.DimGray;
            this.mstRelNomeGerencial.Location = new System.Drawing.Point(150, 33);
            this.mstRelNomeGerencial.Name = "mstRelNomeGerencial";
            this.mstRelNomeGerencial.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.mstRelNomeGerencial.Size = new System.Drawing.Size(600, 33);
            this.mstRelNomeGerencial.StylePriority.UseFont = false;
            this.mstRelNomeGerencial.StylePriority.UseForeColor = false;
            this.mstRelNomeGerencial.StylePriority.UseTextAlignment = false;
            this.mstRelNomeGerencial.Text = "Nome do Relatório";
            this.mstRelNomeGerencial.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // mstRelBottomMargin
            // 
            this.mstRelBottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.mstRelLogoBgm});
            this.mstRelBottomMargin.Name = "mstRelBottomMargin";
            // 
            // mstRelLogoBgm
            // 
            this.mstRelLogoBgm.Image = ((System.Drawing.Image)(resources.GetObject("mstRelLogoBgm.Image")));
            this.mstRelLogoBgm.Location = new System.Drawing.Point(8, 8);
            this.mstRelLogoBgm.Name = "mstRelLogoBgm";
            this.mstRelLogoBgm.NavigateUrl = "www.bgmrodotec.com.br";
            this.mstRelLogoBgm.Size = new System.Drawing.Size(117, 42);
            this.mstRelLogoBgm.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // MasterRelatorio
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.mstRelPageHeaderOperacional,
            this.mstRelPageFooter,
            this.mstRelTopMargin,
            this.mstRelBottomMargin});
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 125, 100);
            this.Version = "8.1";
            ((System.ComponentModel.ISupportInitialize)(this.mstRelParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mstRelParamGerencial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.PageHeaderBand mstRelPageHeaderOperacional;
        private DevExpress.XtraReports.UI.PageFooterBand mstRelPageFooter;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel mstRelNomeOperacional;
        private DevExpress.XtraReports.UI.TopMarginBand mstRelTopMargin;
        private DevExpress.XtraReports.UI.XRLine xrLine5;
        private DevExpress.XtraReports.UI.XRLine xrLine4;
        private DevExpress.XtraReports.UI.XRLabel mstRelNomeGerencial;
        private DevExpress.XtraReports.UI.XRPageInfo mstRelPagina;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.BottomMarginBand mstRelBottomMargin;
        private DevExpress.XtraReports.UI.XRPictureBox mstRelLogoBgm;
        private DevExpress.XtraReports.UI.XRPictureBox mstRelLogoCliente;
        public DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRRichText mstRelParam;
        private DevExpress.XtraReports.UI.XRRichText mstRelParamGerencial;
    }
}
