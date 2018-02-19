using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Specialized;
using System.Drawing.Design;
using System.Drawing;
using System.Resources;
using System.Windows.Forms.Design;


namespace SampleClasses
{

    internal class HE_GlobalVars
    {
        internal static string[] _ListofRules;

    }

    public class ContrastEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context,
            IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(
                typeof(IWindowsFormsEditorService)) as
                IWindowsFormsEditorService;

            if (wfes != null)
            {
                frmContrast _frmContrast = new frmContrast();

                _frmContrast.trackBar1.Value = (int)value;
                _frmContrast.BarValue = _frmContrast.trackBar1.Value;
                _frmContrast._wfes = wfes;

                wfes.DropDownControl(_frmContrast);
                value = _frmContrast.BarValue;

            }
            return value;
        }
    }

    public enum HE_SourceType { LAN, WebPage, FTP, eMail, OCR }
    public class SourceTypePropertyGridEditor : UITypeEditor
    {
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            //Set to true to implement the PaintValue method
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            //Load SampleResources file
            string m = this.GetType().Module.Name;
            m = m.Substring(0, m.Length - 4);
            ResourceManager resourceManager =
                new ResourceManager(m + ".ResourceStrings.SampleResources", Assembly.GetExecutingAssembly());

            int i = (int)e.Value;
            string _SourceName = "";
            switch (i)
            {
                case ((int)HE_SourceType.LAN): _SourceName = "LANTask"; break;
                case ((int)HE_SourceType.WebPage): _SourceName = "WebTask"; break;
                case ((int)HE_SourceType.FTP): _SourceName = "FTPTask"; break;
                case ((int)HE_SourceType.eMail): _SourceName = "eMailTask"; break;
                case ((int)HE_SourceType.OCR): _SourceName = "OCRTask"; break;
            }

            //Draw the corresponding image
            Bitmap newImage = (Bitmap)resourceManager.GetObject(_SourceName);
            Rectangle destRect = e.Bounds;
            newImage.MakeTransparent();
            e.Graphics.DrawImage(newImage, destRect);

        }
    }

    public class RuleConverter : StringConverter
    {

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //true means show a combobox
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //true will limit to list. false will show the list, but allow free-form entry
            return true;
        }

        public override
            System.ComponentModel.TypeConverter.StandardValuesCollection
            GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(HE_GlobalVars._ListofRules);
        }

    }

    public class HE_Task
    {
        private string _Rule;
        private HE_SourceType _SourceType;
        private int _Contrast;

        [Browsable(true)]
        [TypeConverter(typeof(RuleConverter))]
        public string Rule
        {

            //When first loaded set property with the first item in the rule list.
            get
            {
                string S = "";
                if (_Rule != null)
                {
                    S = _Rule;
                }
                else
                {
                    if (HE_GlobalVars._ListofRules.Length > 0)
                    {
                        //Sort the list before displaying it
                        Array.Sort(HE_GlobalVars._ListofRules);

                        S = HE_GlobalVars._ListofRules[0];
                    }
                }

                return S;
            }
            set { _Rule = value; }

        }

        [Editor(typeof(SourceTypePropertyGridEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public HE_SourceType SourceType
        {
            get { return _SourceType; }
            set { _SourceType = value; }
        }

        [Editor(typeof(ContrastEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public int Contrast
        {
            get { return _Contrast; }
            set { _Contrast = value; }
        }
    }


}
