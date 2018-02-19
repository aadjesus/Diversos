using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OnBarcode.Barcode.BarcodeScanner;

namespace WFA_CodigoDeBarras
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.cmbBxTpCodBarra.Items.AddRange(Enum.GetValues(typeof(BarcodeType))
                .OfType<object>()
                .ToArray());
            this.cmbBxTpCodBarra.SelectedItem = BarcodeType.Code128;
            this.lblArquivo.Text = String.Empty;
        }

        private void btnArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.lblArquivo.Text = openFileDialog.FileName.ToString();

                    this.pctrBxImagem.Load(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex));
            }

        }

        private void btnLerArquivo_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.lblArquivo.Text))
                return;
            
            try
            {
                String[] barcodes = BarcodeScanner.Scan(this.lblArquivo.Text, (BarcodeType)this.cmbBxTpCodBarra.SelectedItem);
                this.txtBxValor.Text = String.Join(";", barcodes);

                MessageBox.Show(this.txtBxValor.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex));
            }
        }
    }
}