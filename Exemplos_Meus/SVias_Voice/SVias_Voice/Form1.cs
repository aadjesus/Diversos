using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Net;
using System.Net.Sockets;
using CTIServerFlexClientAPI.Commands;
using System.Linq.Expressions;

namespace SVias_Voice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //   toolTip1.SetToolTip(this.checkBox1, this.checkBox1.Checked ? "ok" : "não");       

            //this.checkBox1.MouseMove += delegate(object sender, MouseEventArgs e)
            //{
            //    toolTip1.SetToolTip(checkBox1, this.checkBox1.Checked ? "ok" : "não");
            //};
        }

        CTIServerFlexClientAPI.CTIServerFlexClientAPI ApiVoice = new CTIServerFlexClientAPI.CTIServerFlexClientAPI();

        private void cmd_CommandResponse(Command sender)
        {
            try
            {
                lblCtrlRetornoVoice.Text = String.Concat(
                    "EVENT-Command:",
                    sender.ToString(),
                    "\n",
                    "EVENT-IsTimeout:",
                    sender.IsTimeout());
            }
            catch (Exception)
            {
            }
        }

        private void smplBtnLigarSVias_Click(object sender, EventArgs e)
        {
            try
            {
                Socket _socketSVias = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress[] ipAddress = Dns.GetHostAddresses(textEditHostSVias.Text);
                IPEndPoint iPEndPoint = new IPEndPoint(ipAddress[0], Convert.ToInt32(textEditPortaSVias.Text.Trim()));

                int PreLigacao = 0;
                if (!_socketSVias.Connected)
                    _socketSVias.Connect(iPEndPoint);

                lblCtrlRetornoSVias.Text = String.Concat(""
                    , "interagy[04]"
                    , textEditNumeroRastreador.Text
                    , ","
                    , (PreLigacao == 0
                        ? ""
                        : PreLigacao.ToString())
                    , textEditTelefoneSVias.Text.Trim()
                    );

                _socketSVias.Send(Encoding.ASCII.GetBytes(lblCtrlRetornoSVias.Text));
                lblCtrlRetornoSVias.Text = "OK";
            }
            catch (Exception ex)
            {
                lblCtrlRetornoSVias.Text = ex.Message;
                if (sender != smplBtnLigarSVias)
                    throw ex;
            }

        }

        private void smplBtnLigarVoice_Click(object sender, EventArgs e)
        {
            try
            {
                ApiVoice.AppInformation.Number = 1;
                ApiVoice.AppInformation.Version = System.Windows.Forms.Application.ProductVersion;
                ApiVoice.Stream.Port = Convert.ToInt32(textEditPortaVoice.Text);
                ApiVoice.Stream.HostAddress = textEditHostVoice.Text;
                ApiVoice.AppInformation.Device = textEditRamalVoice.Text;
                ApiVoice.CommandControl.CommandResponse += new CommandResponseDelegate(cmd_CommandResponse);
                ApiVoice.Stream.Open();
                ApiVoice.Connected(ApiVoice.Stream);

                lblCtrlRetornoVoice.Text = "OK";
            }
            catch (Exception ex)
            {
                lblCtrlRetornoVoice.Text = ex.Message;
                if (sender != smplBtnLigarVoice)
                    throw ex;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CmdMakeCall cmd = new CmdMakeCall()
                {
                    Device = ApiVoice.AppInformation.Device,
                    DeviceTo = textEditTelefoneVoice.Text
                };
                cmd.CommandResponse += new CommandResponseDelegate(cmd_CommandResponse);
                ApiVoice.Execute(cmd);

                lblCtrlRetornoVoice.Text = "OK";

            }
            catch (Exception ex)
            {
                lblCtrlRetornoVoice.Text = ex.Message;
            }
        }

        ToolTip toolTip1 = new ToolTip();

        private void checkBox1_Enter(object sender, EventArgs e)
        {
            //toolTip1.Show(this.checkBox1.Checked ? "ok" : "não", this);
        }

        private void checkBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_MouseHover(object sender, EventArgs e)
        {
           // toolTip1.SetToolTip(checkBox1, this.checkBox1.Checked ? "ok" : "não");
        }
    }

}

