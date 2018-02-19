using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.Skins;
using System.Runtime.InteropServices;
using DevExpress.XtraBars.Localization;
using DevExpress.XtraBars.Helpers;
using Microsoft.Win32;

namespace WFA_ComponentDeImagens
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public RibbonForm1()
        {
            InitializeComponent();

            //ribbonGalleryBarItem1.Gallery.
            //    .AddRange(DevExpress.Skins.SkinManager.Default.Skins.OfType<SkinContainer>().Select(s => s.SkinName).ToArray());

            //  SkinHelper.InitSkinGallery(ribbonGalleryBarItem1, true);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

           
        }

        //bool xxx = true;
        //protected override void WndProc(ref Message m)
        //{
        //    //if (xxx && listBoxControl1 != null)
        //    //    listBoxControl1.Items.Add(string.Concat(m));                
        //    base.WndProc(ref m);
        //}




        private void ribbonGalleryBarItem1_GalleryItemClick_1(object sender, GalleryItemClickEventArgs e)
        {
            //this.ribbonGalleryBarItem1.Gallery.Groups
            //    .OfType<GalleryItemGroup>()
            //    .FirstOrDefault().Items
            //    .OfType<GalleryItem>()
            //    .ToList()
            //    .ForEach(f => f.Checked = false);

            //e.Item.Checked = !e.Item.Checked;

            try
            {
                GalleryItem galleryItem = this.ribbonGalleryBarItem1.Gallery.Groups
                    .OfType<GalleryItemGroup>()
                    .FirstOrDefault().Items
                    .OfType<GalleryItem>()
                    .Where(w => w.Caption == e.Item.Caption)
                    .FirstOrDefault();
                galleryItem.Checked = true;

                //GlobusMais_Black
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = galleryItem.Caption;
                //    DevExpress.Skins.SkinManager.Default.GetValidSkinName(listBoxControl1.Items[listBoxControl1.SelectedIndex].ToString());
            }
            catch (Exception)
            {
            }

            //e.Gallery.Groups
            //    .OfType<GalleryItemGroup>()
            //    .Where(w=> w.)
          
            //e.Item.Checked = true;
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            //RibbonPage
            //this.ForeColor

            //new RibbonPageCategory().CategoryInfo.ViewInfo.
            XtraForm1 xtraForm1 = new XtraForm1();
            xtraForm1.Appearance.BorderColor = Color.Red;
            xtraForm1.Show();

            var xx = ribbonPage1.GetType().GetProperties();
            var xx1 = ribbonPage1.GetType().GetFields();
            if (xx == null || xx1 == null)
            {

            }

        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            // The name of the key must include a valid root.
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "DIOTqfqvge^Inqdwu-";
            const string keyName = userRoot + "\\" + subkey;

            // An int value can be stored without specifying the
            // registry data type, but long values will be stored
            // as strings unless you specify the type. Note that
            // the int is stored in the default name/value
            // pair.
            Registry.SetValue(keyName, "", 5280);
            Registry.SetValue(keyName, "TestLong", 12345678901234,RegistryValueKind.QWord);

            // Strings with expandable environment variables are
            // stored as ordinary strings unless you specify the
            // data type.
            Registry.SetValue(keyName, "TestExpand", "My path: %path%");
            Registry.SetValue(keyName, "TestExpand2", "My path: %path%",RegistryValueKind.ExpandString);

            // Arrays of strings are stored automatically as 
            // MultiString. Similarly, arrays of Byte are stored
            // automatically as Binary.
            string[] strings = { "One", "Two", "Three" };
            Registry.SetValue(keyName, "TestArray", strings);

            // Your default value is returned if the name/value pair
            // does not exist.
            string noSuch = (string)Registry.GetValue(keyName,"NoSuchName","Return this default if NoSuchName does not exist.");

            Console.WriteLine("\r\nNoSuchName: {0}", noSuch);

            // Retrieve the int and long values, specifying 
            // numeric default values in case the name/value pairs
            // do not exist. The int value is retrieved from the
            // default (nameless) name/value pair for the key.
            int tInteger = (int)Registry.GetValue(keyName, "", -1);
            Console.WriteLine("(Default): {0}", tInteger);
            long tLong = (long)Registry.GetValue(keyName, "TestLong",long.MinValue);
            Console.WriteLine("TestLong: {0}", tLong);

            // When retrieving a MultiString value, you can specify
            // an array for the default return value. 
            string[] tArray = (string[])Registry.GetValue(keyName,
                "TestArray",
                new string[] { "Default if TestArray does not exist." });
            for (int i = 0; i < tArray.Length; i++)
            {
                Console.WriteLine("TestArray({0}): {1}", i, tArray[i]);
            }

            // A string with embedded environment variables is not
            // expanded if it was stored as an ordinary string.
            string tExpand = (string)Registry.GetValue(keyName,
                 "TestExpand",
                 "Default if TestExpand does not exist.");
            Console.WriteLine("TestExpand: {0}", tExpand);

            // A string stored as ExpandString is expanded.
            string tExpand2 = (string)Registry.GetValue(keyName,
                "TestExpand2",
                "Default if TestExpand2 does not exist.");
            Console.WriteLine("TestExpand2: {0}...",
                tExpand2.Substring(0, 40));

            Console.WriteLine("\r\nUse the registry editor to examine the key.");
            Console.WriteLine("Press the Enter key to delete the key.");
            Console.ReadLine();
            Registry.CurrentUser.DeleteSubKey(subkey);
        }



    }


}