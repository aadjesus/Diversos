namespace Tela_Generica
{
    partial class Form1
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
            FGlobus.Componentes.WinForms.ClassDateEditBGM classDateEditBGM1 = new FGlobus.Componentes.WinForms.ClassDateEditBGM();
            FGlobus.Componentes.WinForms.ClassDateEditBGM classDateEditBGM2 = new FGlobus.Componentes.WinForms.ClassDateEditBGM();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.itemBarraItensBGM1 = new FGlobus.Componentes.WinForms.ItemBarraItensBGM();
            this.dateEditBGM1 = new FGlobus.Componentes.WinForms.DateEditBGM(this.components);
            this.buttonEditBGM1 = new FGlobus.Componentes.WinForms.ButtonEditBGM(this.components);
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.userControl11 = new Tela_Generica.UserControl1();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBarraItensBGM1.AreaItem)).BeginInit();
            this.itemBarraItensBGM1.AreaItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemBarraItensBGM1.AreaItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBGM1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBGM1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditBGM1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(271, 326);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(177, 147);
            this.panelControl1.TabIndex = 2;
            // 
            // itemBarraItensBGM1
            // 
            // 
            // itemBarraItensBGM1.AreaItem
            // 
            this.itemBarraItensBGM1.AreaItem.Controls.Add(this.dateEditBGM1);
            this.itemBarraItensBGM1.AreaItem.Controls.Add(this.buttonEditBGM1);
            this.itemBarraItensBGM1.AreaItem.Controls.Add(this.buttonEdit1);
            this.itemBarraItensBGM1.AreaItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.itemBarraItensBGM1.AreaItem.Size = new System.Drawing.Size(380, 69);
            // 
            // itemBarraItensBGM1.AreaItens
            // 
            this.itemBarraItensBGM1.AreaItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemBarraItensBGM1.AreaItens.Size = new System.Drawing.Size(380, 136);
            this.itemBarraItensBGM1.Location = new System.Drawing.Point(271, 47);
            this.itemBarraItensBGM1.MinimumSize = new System.Drawing.Size(260, 150);
            this.itemBarraItensBGM1.MostrarItem = false;
            this.itemBarraItensBGM1.Name = "itemBarraItensBGM1";
            this.itemBarraItensBGM1.PrimeiroControle = this.buttonEditBGM1;
            this.itemBarraItensBGM1.PrimeiroControleItem = this.buttonEditBGM1;
            this.itemBarraItensBGM1.SentidoNavegacao = FGlobus.Componentes.WinForms.MasterPanel.eSentidoNavegacao.None;
            this.itemBarraItensBGM1.Size = new System.Drawing.Size(384, 228);
            this.itemBarraItensBGM1.TabIndex = 0;
            // 
            // dateEditBGM1
            // 
            classDateEditBGM1.Ativo = false;
            classDateEditBGM1.MensagemAviso = "";
            classDateEditBGM1.PassarAQuantidadeDeDias = FGlobus.Componentes.WinForms.eDateEditBGM.Permitir;
            classDateEditBGM1.QuantidadeDias = 0;
            this.dateEditBGM1.DataProgressiva = classDateEditBGM1;
            classDateEditBGM2.Ativo = false;
            classDateEditBGM2.MensagemAviso = "";
            classDateEditBGM2.PassarAQuantidadeDeDias = FGlobus.Componentes.WinForms.eDateEditBGM.Permitir;
            classDateEditBGM2.QuantidadeDias = 0;
            this.dateEditBGM1.DataRetroativa = classDateEditBGM2;
            this.dateEditBGM1.EditValue = new System.DateTime(2012, 7, 25, 0, 0, 0, 0);
            this.dateEditBGM1.Location = new System.Drawing.Point(228, 13);
            this.dateEditBGM1.Name = "dateEditBGM1";
            toolTipTitleItem1.Text = "Calendário";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.dateEditBGM1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dateEditBGM1.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4), serializableAppearanceObject1, "", null, superToolTip1, true)});
            this.dateEditBGM1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditBGM1.Size = new System.Drawing.Size(100, 20);
            toolTipTitleItem2.Text = "Pressione...";
            toolTipItem1.Text = "Hoje  ( T )\r\nOntem  ( R )\r\nAmanhã  ( Y )\r\nPrimeiro dia do mês atual  ( PageUp )\r\n" +
    "Último dia do mês atual  ( PageDown )\r\nAcresce um dia  ( + )\r\nDiminui um dia  ( " +
    "- )";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem1);
            this.dateEditBGM1.SuperTip = superToolTip2;
            this.dateEditBGM1.TabIndex = 2;
            this.dateEditBGM1.TipoMascara.Codigo = 0;
            this.dateEditBGM1.TipoMascara.Controles = null;
            this.dateEditBGM1.TipoMascara.DescricaoTipo = "";
            this.dateEditBGM1.TipoMascara.EditMask = "";
            this.dateEditBGM1.TipoMascara.Exemplo = "";
            this.dateEditBGM1.TipoMascara.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEditBGM1.TipoMascara.PasswordChar = ' ';
            this.dateEditBGM1.TipoMascara.PlaceHolder = '\0';
            this.dateEditBGM1.TipoMascara.Tipo = "";
            // 
            // buttonEditBGM1
            // 
            this.buttonEditBGM1.EditValue = "buttonEditBGM1";
            this.buttonEditBGM1.ExecutarPesquisa = true;
            this.buttonEditBGM1.Location = new System.Drawing.Point(7, 12);
            this.buttonEditBGM1.Name = "buttonEditBGM1";
            this.buttonEditBGM1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditBGM1.ResultadoPesquisaDados = null;
            this.buttonEditBGM1.Size = new System.Drawing.Size(100, 20);
            this.buttonEditBGM1.TabIndex = 0;
            this.buttonEditBGM1.TamanhoCampo = 0;
            this.buttonEditBGM1.TipoMascara.Codigo = 0;
            this.buttonEditBGM1.TipoMascara.Controles = null;
            this.buttonEditBGM1.TipoMascara.DescricaoTipo = "";
            this.buttonEditBGM1.TipoMascara.EditMask = "";
            this.buttonEditBGM1.TipoMascara.Exemplo = "";
            this.buttonEditBGM1.TipoMascara.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.buttonEditBGM1.TipoMascara.PasswordChar = ' ';
            this.buttonEditBGM1.TipoMascara.PlaceHolder = '\0';
            this.buttonEditBGM1.TipoMascara.Tipo = "";
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(113, 12);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(100, 20);
            this.buttonEdit1.TabIndex = 1;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(21, 47);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.itemBarraItensBGM1;
            this.propertyGrid1.Size = new System.Drawing.Size(190, 416);
            this.propertyGrid1.TabIndex = 4;
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(478, 297);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(253, 176);
            this.elementHost1.TabIndex = 5;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.userControl11;
            // 
            // Form1
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 485);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.itemBarraItensBGM1);
            this.Controls.Add(this.panelControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.itemBarraItensBGM1, 0);
            this.Controls.SetChildIndex(this.propertyGrid1, 0);
            this.Controls.SetChildIndex(this.elementHost1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBarraItensBGM1.AreaItem)).EndInit();
            this.itemBarraItensBGM1.AreaItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.itemBarraItensBGM1.AreaItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBGM1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditBGM1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditBGM1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private FGlobus.Componentes.WinForms.ItemBarraItensBGM itemBarraItensBGM1;
        private FGlobus.Componentes.WinForms.ButtonEditBGM buttonEditBGM1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private FGlobus.Componentes.WinForms.DateEditBGM dateEditBGM1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private UserControl1 userControl11;









    }
}