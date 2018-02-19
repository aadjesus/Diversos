using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using EnvDTE;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ToolboxCreatorBGM
{
    public partial class FrmToolboxCreatorBGM : Form
    {
        #region Construtor

        /// <summary>
        /// Construtor default
        /// </summary>
        public FrmToolboxCreatorBGM()
        {
            InitializeComponent();
        }

        #endregion

        #region Atributos

        private IntPtr TeclaAnterior;
        private bool TabCriada = false;
        private const string NOME_TAB = "BGMRodotec ©";
        private string DLLBgmRodotec
        {
            get
            {
                if (AppDomain.CurrentDomain.GetData("DLLBgmRodotec") == null)
                {
                    #region Delegate para procurar a pastas com as Dlls
                    // Procura nas pastas C:\Globus5\WPF\Distribuicao\, C:\GlobusMais\WPF\Distribuicao\ ou na informada

                    Func<string, int, string> delegateProcurarDiretorio = null;
                    delegateProcurarDiretorio = delegate(string diretorioPadrao, int item)
                    {
                        try
                        {
                            string pathArquivo = string.Concat(diretorioPadrao, @"\", "BgmRodotec.FGlobus.Componentes.WinForms.dll");
                            if (!File.Exists(pathArquivo))
                                throw new Exception();

                            return pathArquivo;
                        }
                        catch (Exception)
                        {
                            string diretorioAux = @"C:\GlobusMais\WPF\Distribuicao\";
                            ++item;
                            if (item.Equals(2))
                            {
                                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                                folderBrowserDialog.Description = "Informe a pasta que tenha o arquivo dll BgmRodotec.FGlobus.Componentes.WinForms.dll";
                                folderBrowserDialog.SelectedPath = @"C:\";
                                folderBrowserDialog.ShowDialog();
                                diretorioAux = folderBrowserDialog.SelectedPath;
                            }
                            else if (item.Equals(3))
                                return null;

                            return delegateProcurarDiretorio(diretorioAux, item);
                        }
                    };

                    #endregion

                    AppDomain.CurrentDomain.SetData("DLLBgmRodotec", delegateProcurarDiretorio(@"C:\Globus5\WPF\Distribuicao\", 0));
                }
                return AppDomain.CurrentDomain.GetData("DLLBgmRodotec").ToString();
            }
        }
        private delegate void CreateProjectDelegate(DTE dte);

        #endregion

        #region Metodos

        private void GetVisualStudio2010Project(DTE dte)
        {
            const string TEMPLATENAME = "WindowsApplication.zip";
            const string DUMMYPROJECTNAME = "dummyproj";

            string tmpFile = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            string tmpDir = string.Format("{0}{1}", Path.GetTempPath(), tmpFile);


            EnvDTE100.Solution4 solution = dte.Solution as EnvDTE100.Solution4;

            string templatePath = solution.GetProjectTemplate(TEMPLATENAME, "CSharp");


            solution.AddFromTemplate(templatePath, tmpDir, DUMMYPROJECTNAME, false);

            MessageBox.Show(String.Concat(
                "tmpDir:", tmpDir,
                "\n ",
                "tmpFile:", tmpFile,
                "\n ",
                "solution:", solution,
                "\n ",
                "templatePath:", templatePath
                ));
        }

        private void Teste_NomeMetodo()
        {
            DTE dte = (DTE)System.Activator.CreateInstance(System.Type.GetTypeFromProgID("VisualStudio.DTE.10.0"), true);
            EnvDTE.Window window = dte.Windows.Item("{B1E99781-AB81-11D0-B683-00AA00A3EE26}");
            EnvDTE.ToolBox toolbox = (EnvDTE.ToolBox)window.Object;

            MessageBox.Show(String.Concat(
                toolbox.ToolBoxTabs.OfType<ToolBoxTab>().Where(w => w.Name.Equals("BGMRodotec ©"))
            .Count() > 0
            ));
            if (dte == null)
            {

            }



        }

        private void CriarToolbox(
            CreateProjectDelegate CreateProject,
            EnvDTE.Window window = null,
            EnvDTE.ToolBox toolbox = null)
        {
            string visualStudio = "VisualStudio.DTE.10.0";
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(visualStudio);
            if (registryKey != null)
            {

                DTE dte = (DTE)System.Activator.CreateInstance(System.Type.GetTypeFromProgID(visualStudio), true);
                try
                {
                    bool instalar = window == null || toolbox == null;

                    CreateProject.Invoke(dte);
                    MessageFilter.Registrar();

                    if (instalar)
                    {
                        window = dte.Windows.Item("{B1E99781-AB81-11D0-B683-00AA00A3EE26}");
                        toolbox = (EnvDTE.ToolBox)window.Object;
                    }
                    // Se é pra instalar e não existe a aba então desistalara primeiro
                    if (instalar &&
                        toolbox.ToolBoxTabs
                            .OfType<ToolBoxTab>()
                            .Where(w => w.Name.Equals(NOME_TAB))
                            .Count() > 0)
                    {
                        MessageBox.Show("1");

                        CriarToolbox(CreateProject, window, toolbox);

                        MessageBox.Show("2");
                    }

                    EnvDTE.ToolBoxTab toolBoxTab;
                    if (instalar)
                    {
                        toolBoxTab = toolbox.ToolBoxTabs.Add(NOME_TAB);
                        toolBoxTab.Activate();
                        toolBoxTab.ToolBoxItems.Add(NOME_TAB, this.DLLBgmRodotec, vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);
                    }
                    else
                    {
                        toolBoxTab = toolbox.ToolBoxTabs.Item(NOME_TAB);
                        toolBoxTab.Activate();
                        toolBoxTab.Delete();
                    }

                    TabCriada = true;
                    MessageBox.Show(instalar.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Concat(ex), "Erro: 1", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (dte != null)
                    {
                        dte.Solution.Close(false);
                        dte.Quit();
                        Marshal.ReleaseComObject(dte);
                        MessageFilter.Revoke();
                    }
                }
            }
        }

        private void FrmToolboxCreatorBGM_Load(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();

                EnvDTE80.DTE2 dte2 = System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.10.0") as EnvDTE80.DTE2;
                if (splashScreenManager1.IsSplashFormVisible)
                    splashScreenManager1.CloseWaitForm();

                MessageBox.Show(this, "O Visual Studio não pode estar aberto.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                if (File.Exists(this.DLLBgmRodotec))
                    CriarToolbox(this.GetVisualStudio2010Project);
            }
            finally
            {
                if (splashScreenManager1.IsSplashFormVisible)
                    splashScreenManager1.CloseWaitForm();

                if (TabCriada)
                    MessageBox.Show(this, "Tab " + NOME_TAB + " incluída com êxito.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (((Keys)msg.WParam) == Keys.F4 & TeclaAnterior == ((IntPtr)18))
            {
                return true;
            }
            TeclaAnterior = msg.WParam;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Classe auxiliar

        /// <summary>
        /// Classe para controlar o registro
        /// </summary>
        internal class MessageFilter : IOleMessageFilter
        {
            /// <summary>
            /// Registra
            /// </summary>
            internal static void Registrar()
            {
                IOleMessageFilter newFilter = new MessageFilter();
                IOleMessageFilter oldFilter = null;
                CoRegisterMessageFilter(newFilter, out oldFilter);
            }

            internal static void Revoke()
            {
                IOleMessageFilter oldFilter = null;
                CoRegisterMessageFilter(null, out oldFilter);
            }

            int IOleMessageFilter.HandleInComingCall(int dwCallType, System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr lpInterfaceInfo)
            {
                return 0;
            }

            int IOleMessageFilter.RetryRejectedCall(System.IntPtr hTaskCallee, int dwTickCount, int dwRejectType)
            {
                if (dwRejectType == 2)
                // flag = SERVERCALL_RETRYLATER.
                {
                    // Retry the thread call immediately if return >=0 & 
                    // <100.
                    return 99;
                }
                // Too busy; cancel call.
                return -1;
            }

            int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee, int dwTickCount, int dwPendingType)
            {
                //Return the flag PENDINGMSG_WAITDEFPROCESS.
                return 2;
            }

            [DllImport("Ole32.dll")]
            private static extern int CoRegisterMessageFilter(IOleMessageFilter newFilter, out  IOleMessageFilter oldFilter);
        }

        [ComImport(), Guid("00000016-0000-0000-C000-000000000046"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        interface IOleMessageFilter
        {
            [PreserveSig]
            int HandleInComingCall(int dwCallType, IntPtr hTaskCaller, int dwTickCount, IntPtr lpInterfaceInfo);

            [PreserveSig]
            int RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType);

            [PreserveSig]
            int MessagePending(IntPtr hTaskCallee, int dwTickCount, int dwPendingType);
        }

        #endregion
    }
}
