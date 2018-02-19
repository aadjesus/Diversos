using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using DevExpress.XtraEditors.Controls;

namespace Sessions_WebService
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();

            DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.Globus.Skin).Assembly);

            imageComboBoxEdit1.Properties.Items.Clear();
            imageComboBoxEdit1.Properties.Items
                .AddRange(
                    Enum.GetValues(typeof(LinearGradientMode))
                    .OfType<LinearGradientMode>()
                    .Select(s => new ImageComboBoxItem(s.ToString(), s))
                    .ToArray()
                    );
            imageComboBoxEdit1.SelectedIndex = 3;

            radioGroup1.Properties.Items.Clear();
            radioGroup1.Properties.Items
                .AddRange(
                    Enum.GetValues(typeof(Mensagem.eTipoMensagem))
                    .OfType<Mensagem.eTipoMensagem>()
                    .Where(w => w != Mensagem.eTipoMensagem.CaixaDeEntrada &&
                               w != Mensagem.eTipoMensagem.Interna)


                    .Select(s => new RadioGroupItem(s, FGlobus.Util.Util.GetDescricaoEnum(s)))
                    .ToArray()
                    );
            radioGroup1.SelectedIndex = 0;

            colorEdit1.EditValue = Color.Red;
            colorEdit2.EditValue = Color.Black;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MasterFormMensagem masterFormMensagem = new MasterFormMensagem(new string[] { memoEdit1.Text.Trim() }, (Mensagem.eTipoMensagem)radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value);
            masterFormMensagem.pnlCtrlMensagem.LookAndFeel.UseDefaultLookAndFeel = false;

            masterFormMensagem.pnlCtrlMensagem.BorderStyle = BorderStyles.NoBorder;

            if (radioGroup2.SelectedIndex.Equals(0))
            {
                masterFormMensagem.pnlCtrlMensagem.Padding = new Padding(
                    checkedComboBoxEdit1.Properties.Items[0].CheckState.Equals(CheckState.Checked) ? (int)spinEdit1.Value : 0,
                    checkedComboBoxEdit1.Properties.Items[1].CheckState.Equals(CheckState.Checked) ? (int)spinEdit1.Value : 0,
                    checkedComboBoxEdit1.Properties.Items[2].CheckState.Equals(CheckState.Checked) ? (int)spinEdit1.Value : 0,
                    checkedComboBoxEdit1.Properties.Items[3].CheckState.Equals(CheckState.Checked) ? (int)spinEdit1.Value : 0);

                masterFormMensagem.pnlCtrlImagem.Appearance.BackColor = Color.Transparent;
                masterFormMensagem.pnlCtrlImagem.Appearance.BackColor2 = Color.Transparent;

                masterFormMensagem.pnlCtrlMensagem.Appearance.GradientMode = (LinearGradientMode)((ComboBoxItem)(imageComboBoxEdit1.SelectedItem)).Value;
                masterFormMensagem.pnlCtrlMensagem.Appearance.BackColor = (Color)colorEdit1.EditValue;
                masterFormMensagem.pnlCtrlMensagem.Appearance.BackColor2 = (Color)colorEdit2.EditValue;
            }
            else
            {
                masterFormMensagem.pnlCtrlImagem.Appearance.GradientMode = (LinearGradientMode)((ComboBoxItem)(imageComboBoxEdit1.SelectedItem)).Value;
                masterFormMensagem.pnlCtrlImagem.Appearance.BackColor = (Color)colorEdit1.EditValue;
                masterFormMensagem.pnlCtrlImagem.Appearance.BackColor2 = (Color)colorEdit2.EditValue;
            }
            masterFormMensagem.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            radioGroup1.Properties.Items
                .OfType<RadioGroupItem>()
                .ToList()
                .ForEach(f =>
                {
                    Mensagem.eTipoMensagem tipoMensagem = (Mensagem.eTipoMensagem)f.Value;

                    MasterFormMensagem masterFormMensagem = new MasterFormMensagem(new string[] { f.Description }, tipoMensagem);
                    masterFormMensagem.pnlCtrlMensagem.LookAndFeel.UseDefaultLookAndFeel = false;

                    masterFormMensagem.pnlCtrlMensagem.BorderStyle = BorderStyles.NoBorder;
                    masterFormMensagem.pnlCtrlMensagem.Padding = new Padding(2);
                    masterFormMensagem.pnlCtrlImagem.Appearance.BackColor = Color.Transparent;
                    masterFormMensagem.pnlCtrlImagem.Appearance.BackColor2 = Color.Transparent;

                    masterFormMensagem.pnlCtrlMensagem.Appearance.GradientMode = LinearGradientMode.BackwardDiagonal;
                    switch (tipoMensagem)
                    {
                        case Mensagem.eTipoMensagem.Erro:
                            masterFormMensagem.pnlCtrlMensagem.Appearance.BackColor = Color.Red;
                            masterFormMensagem.pnlCtrlMensagem.Appearance.BackColor2 = Color.Black;
                            break;
                        case Mensagem.eTipoMensagem.Informacao:
                            masterFormMensagem.pnlCtrlMensagem.Appearance.BackColor = Color.Blue;
                            masterFormMensagem.pnlCtrlMensagem.Appearance.BackColor2 = Color.Black;
                            break;
                        case Mensagem.eTipoMensagem.Pergunta:
                            masterFormMensagem.pnlCtrlMensagem.Appearance.BackColor = Color.Yellow;
                            masterFormMensagem.pnlCtrlMensagem.Appearance.BackColor2 = Color.Black;
                            break;
                    }
                    masterFormMensagem.ShowDialog();
                });

        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedComboBoxEdit1.Enabled = radioGroup2.SelectedIndex.Equals(0);
            spinEdit1.Enabled = checkedComboBoxEdit1.Enabled;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radioGroup1.SelectedIndex)
            {
                case 0:
                    colorEdit1.EditValue = Color.Red;
                    colorEdit2.EditValue = Color.Black;
                    break;
                case 1:
                    colorEdit1.EditValue = Color.Blue;
                    colorEdit2.EditValue = Color.Black;
                    break;
                case 2:
                    colorEdit1.EditValue = Color.Yellow;
                    colorEdit2.EditValue = Color.Black;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt16("0a");
            }
            catch (Exception ex)
            {
                ex.MostrarMensagem();
            }
        }
    }

    public static class ExtensaoDeMetodosX
    {
        public static void MostrarMensagem(this Exception valor)
        {
            //Sessions_WebService.Mensagem.Erro.Abrir(String.Concat(valor));
            //AppDomain.CurrentDomain.ActivationContext
            
            //System.Windows.Forms.Form.ActiveForm
            //var x1= System.Windows.Forms.Application.OpenForms
            //    .OfType<DevExpress.XtraEditors.XtraForm>()
            //    .First().

            MessageBox.Show(String.Concat(valor), "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
