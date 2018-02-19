using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WFA_ComponentDeImagens
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;



            //DwmEnableComposition(false);
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.Skins.SkinManager.EnableFormSkinsIfNotVista();
            DevExpress.Skins.SkinManager.AllowWindowGhosting = true;
            //DevExpress.Skins.SkinManager.e
            DevExpress.UserSkins.BonusSkins.Register();
            //DevExpress.UserSkins.NamespaceEricc.ClassEricc

            DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.FGlobus.Skin).Assembly);            

            
            Application.Run(new RibbonForm1());
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableComposition(bool fEnable);

        #region 2?
        
        //public static bool EnableComposition()
        //{
        //    try
        //    {
        //        DwmEnableComposition(0);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //[DllImport("dwmapi.dll", EntryPoint = "DwmEnableComposition")]
        //extern static uint DwmEnableComposition(uint compositionAction);

        #endregion


        #region 1º

        //[DllImport("kernel32", SetLastError = true)]
        //static extern IntPtr LoadLibrary(string lpFileName);
        //[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        //static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        //internal delegate int DwmEnableComposition(int enable);


        //private static void DisableAeroDesktopWindowManager()
        //{
        //    IntPtr pDll = IntPtr.Zero;

        //    try
        //    {
        //        pDll = LoadLibrary("dwmapi.dll");
        //        if (pDll == IntPtr.Zero)
        //            return;

        //        // Get the DwmEnableComposition Function
        //        IntPtr pFunc = GetProcAddress(pDll, "DwmEnableComposition");
        //        if (pFunc == IntPtr.Zero)
        //            return;


        //        DwmEnableComposition enableDwm = (DwmEnableComposition)System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer(pFunc, typeof(DwmEnableComposition));
        //        if (enableDwm == null) return;
        //        {
        //            // Disable Desktop Window Manager
        //            enableDwm(0);
        //        }
        //    }
        //    catch
        //    {
        //        //Todo: log error
        //    }

        //}

        #endregion

    }
}
