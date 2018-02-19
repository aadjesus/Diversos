using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace TClientSocket
{
    public class ClientSocket
    {
        #region Delegates
        public delegate void ConnectionDelegate(Socket soc);
        public delegate void ErrorDelegate(string ErroMessage, Socket soc, int ErroCode);
        #endregion

        #region Eventos
        public event ConnectionDelegate OnConnect;
        public event ConnectionDelegate OnDisconnect;
        public event ConnectionDelegate OnRead;
        public event ConnectionDelegate OnWrite;
        public event ErrorDelegate OnError;
        public event ConnectionDelegate OnSendFile;
        #endregion

        #region Variaveis
        private AsyncCallback WorkerCallBack;
        private Socket mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private IPEndPoint ipLocal;
        private byte[] dataBuffer = new byte[1024];
        private int mPorta = 0;
        private byte[] mBytesRecebidos;
        private string mTextoRecebido = "";
        private string mTextoEnviado = "";
        private string mRemoteAddress = "";
        private string mRemoteHost = "";
        #endregion

        #region Propriedades
        /// <summary>
        /// Porta para conexão com o Servidor
        /// </summary>
        public int Port
        {
            get
            {
                return (mPorta);
            }
        }

        /// <summary>
        /// Bytes que chegaram ao Socket
        /// </summary>
        public byte[] ReceivedBytes
        {
            get
            {
                byte[] temp = null;
                if (mBytesRecebidos != null)
                {
                    temp = mBytesRecebidos;
                    mBytesRecebidos = null;
                }
                return (temp);
            }
        }

        /// <summary>
        /// Messagem que chegou ao Socket
        /// </summary>
        public string ReceivedText
        {
            get
            {
                string temp = mTextoRecebido;
                mTextoRecebido = "";
                return (temp);
            }
        }

        /// <summary>
        /// Messagem enviada pelo Socket
        /// </summary>
        public string WriteText
        {
            get
            {
                string temp = mTextoEnviado;
                mTextoEnviado = "";
                return (temp);
            }
        }

        /// <summary>
        /// IP do Servidor
        /// </summary>
        public string RemoteAddress
        {
            get
            {
                if (mainSocket.Connected)
                    return (mRemoteAddress);
                else
                    return "";
            }
        }

        /// <summary>
        /// Host do Servidor
        /// </summary>
        public string RemoteHost
        {
            get
            {
                if (mainSocket.Connected)
                    return (mRemoteHost);
                else
                    return "";
            }
        }

        /// <summary>
        /// Retorna true se o ClientSocket estiver conectado a um Servidor
        /// </summary>
        public bool Connected
        {
            get
            {
                return (mainSocket.Connected);
            }
        }
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor padrão da classe
        /// </summary>
        /// <param name="port">Porta para conexão
        /// </param>
        public ClientSocket(string IP, int port)
        {
            try
            {
                mPorta = port;
                IPAddress ipAddress = IPAddress.Parse(IP);
                mRemoteAddress = ipAddress.ToString();
                IPHostEntry ipss = Dns.GetHostEntry(mRemoteAddress);
                mRemoteHost = ipss.HostName;
                ipLocal = new IPEndPoint(ipAddress, port);
            }
            catch (Exception se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
            }
        }
        #endregion

        #region Funcoes e Eventos
        /// <summary>
        /// Conecta-se ao IP e Porta configurados
        /// </summary>
        public bool Connect()
        {
            try
            {
                //Connect to the server
                mainSocket.BeginConnect(ipLocal, new AsyncCallback(ConfirmConnect), null);
                return true;
            }
            catch (ArgumentException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
                return false;
            }
            catch (InvalidOperationException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
                return false;
            }
            catch (SocketException se)
            {
                if (OnError != null)
                    OnError(se.Message, mainSocket, se.ErrorCode);
                return false;
            }
        }

        private void ConfirmConnect(IAsyncResult asyn)
        {
            try
            {
                mainSocket.EndConnect(asyn);
                WaitForData(mainSocket);
                if (OnConnect != null)
                    OnConnect(mainSocket);
            }
            catch (ObjectDisposedException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
            }
            catch (SocketException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
            }
        }

        private void WaitForData(Socket soc)
        {
            try
            {
                if (WorkerCallBack == null)
                    WorkerCallBack = new AsyncCallback(OnDataReceived);
                soc.BeginReceive(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, WorkerCallBack, null);
            }
            catch (SocketException se)
            {
                if (OnError != null)
                    OnError(se.Message, soc, se.ErrorCode);
            }
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                int iRx = mainSocket.EndReceive(asyn);
                if (iRx < 1)
                {
                    mainSocket.Close();
                    if (!mainSocket.Connected)
                        if (OnDisconnect != null)
                            OnDisconnect(mainSocket);
                }
                else
                {
                    mBytesRecebidos = dataBuffer;
                    char[] chars = new char[iRx + 1];
                    Decoder d = Encoding.UTF8.GetDecoder();
                    d.GetChars(dataBuffer, 0, iRx, chars, 0);
                    mTextoRecebido = new String(chars);
                    if (OnRead != null)
                        OnRead(mainSocket);
                    WaitForData(mainSocket);
                }
            }
            catch (ArgumentException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
            }
            catch (InvalidOperationException se)
            {
                mainSocket.Close();
                if (!mainSocket.Connected)
                    if (OnDisconnect != null)
                        OnDisconnect(mainSocket);
                if (OnError != null)
                    OnError(se.Message, null, 0);
            }
            catch (SocketException se)
            {
                if (OnError != null)
                    OnError(se.Message, mainSocket, se.ErrorCode);
                if (!mainSocket.Connected)
                    if (OnDisconnect != null)
                        OnDisconnect(mainSocket);
            }
        }

        /// <summary>
        /// Envia uma mensagem de texto pela conexão
        /// </summary>
        /// <param name="mens">Mensagem para ser enviada</param>
        public bool SendText(string mens)
        {
            try
            {
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(mens);
                int NumBytes = mainSocket.Send(byData);
                if (NumBytes == byData.Length)
                {
                    if (OnWrite != null)
                    {
                        mTextoEnviado = mens;
                        OnWrite(mainSocket);
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (ArgumentException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
                return false;
            }
            catch (ObjectDisposedException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
                return false;
            }
            catch (SocketException se)
            {
                if (OnError != null)
                    OnError(se.Message, mainSocket, se.ErrorCode);
                return false;
            }
        }

        /// <summary>
        /// Envia arquivo para a conexão selecionada no índice
        /// </summary>
        /// <param name="FileName">Arquivo a ser enviado</param>
        public bool SendFile(string FileName)
        {
            try
            {
                mainSocket.BeginSendFile(FileName, new AsyncCallback(FileSendCallback), mainSocket);
                return true;
            }
            catch (FileNotFoundException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
                return false;
            }
            catch (ObjectDisposedException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
                return false;
            }
            catch (SocketException se)
            {
                if (OnError != null)
                    OnError(se.Message, mainSocket, se.ErrorCode);
                return false;
            }
        }

        /// <summary>
        /// Envia arquivo para a conexão selecionada no índice
        /// </summary>
        /// <param name="FileName">Arquivo a ser enviado</param>
        /// <param name="PreString">String enviada antes do arquivo</param>
        /// <param name="PosString">String enviada logo depois do arquivo</param>
        public bool SendFile(string FileName, string PreString, string PosString)
        {
            try
            {
                byte[] preBuf = Encoding.ASCII.GetBytes(PreString);
                byte[] postBuf = Encoding.ASCII.GetBytes(PosString);
                mainSocket.BeginSendFile(FileName, preBuf, postBuf, 0, new AsyncCallback(FileSendCallback), mainSocket);
                return true;
            }
            catch (ArgumentException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
                return false;
            }
            catch (ObjectDisposedException se)
            {
                if (OnError != null)
                    OnError(se.Message, null, 0);
                return false;
            }
            catch (SocketException se)
            {
                if (OnError != null)
                    OnError(se.Message, mainSocket, se.ErrorCode);
                return false;
            }
        }

        private void FileSendCallback(IAsyncResult ar)
        {
            Socket workerSocket = (Socket)ar.AsyncState;
            workerSocket.EndSendFile(ar);
            if (OnSendFile != null)
                OnSendFile(workerSocket);
        }

        /// <summary>
        /// Desfaz a conexão com o Servidor
        /// </summary>
        public bool Disconnect()
        {
            mainSocket.Close();
            if (!mainSocket.Connected)
            {
                if (OnDisconnect != null)
                    OnDisconnect(mainSocket);
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}
