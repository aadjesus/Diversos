using System.Runtime.InteropServices; //Necessary For APIs

namespace System.Windows.Forms
{
    public class ComboAlignSettings : ComboBox //Inherits ComboBox
    {
        [DllImport("user32", CharSet = CharSet.Auto)]
        public extern static int GetWindowLong(IntPtr hwnd, int nIndex); //Retrieve Info About Specified Window

        [DllImport("user32")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong); //Change An Attribute Of Specified Window

        [DllImport("user32.dll")]
        public static extern int GetComboBoxInfo(IntPtr hWnd, ref COMBOBOXINFO pcbi); //Retrieve Info About Specified Combo Box


        [StructLayout(LayoutKind.Sequential)]
        public struct COMBOBOXINFO //Contains ComboBox Status Info
        {
            public Int32 cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public CASComboBoxButtonState caState;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }


        [StructLayout(LayoutKind.Sequential)] //Describes Width, Height, And Location Of A Rectangle
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public enum CASComboBoxButtonState //Determines Current State Of ComboBox
        {
            STATE_SYSTEM_NONE = 0,
            STATE_SYSTEM_INVISIBLE = 0x00008000,
            STATE_SYSTEM_PRESSED = 0x00000008
        }

        /// <summary> 
        /// Alignment Enum For Left & Right
        /// </summary> 
        public enum CASAlignment
        {
            CASLeft = 0,
            CASRight = 1
        }

        private const int GWL_EXSTYLE = -20; //ComboBox Style
        private const int WS_EX_RIGHT = 4096; //Right Align Text 
        private const int WS_EX_LEFTSCROLLBAR = 16384; //Left ScrollBar
        private const int CB_SHOWDROPDOWN = 335; //Show Drop Down

        private IntPtr CASHandle; //Handle Of ComboBox

        private CASAlignment CASList; //Alignment Options For Text
        private CASAlignment CASButton; //Alignment Options For Button
        private CASAlignment CASScroll; //Alignment Options For ScrollBar


        public ComboAlignSettings()
        {
            CASHandle = CASGetHandle(this); //Get Handle Of ComboBox

            //Set Alignments 
            CASButton = CASAlignment.CASRight;
            CASScroll = CASAlignment.CASRight;
            CASList = CASAlignment.CASLeft;
        }

        /// <summary>
        /// Retrieves ComboBox Handle
        /// </summary>
        /// <param name="CASCombo"></param>
        /// <returns></returns>
        public IntPtr CASGetHandle(ComboBox CASCombo)
        {

            COMBOBOXINFO CASCBI = new COMBOBOXINFO(); //New ComboBox Settings Object
            CASCBI.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(CASCBI); //Call In Correct Size
            GetComboBoxInfo(CASCombo.Handle, ref CASCBI); //Obtain Handle
            return CASCBI.hwndList; //Return Handle
        }

        /// <summary>
        /// Align The ComboBox List
        /// </summary>
        private void CASAlignList()
        {
            if (CASHandle != IntPtr.Zero) //If Valid Handle
            {
                int CASStyle = GetWindowLong(CASHandle, GWL_EXSTYLE); //Get ComboBox Style
                switch (CASList)
                {
                    case CASAlignment.CASRight: 
                        CASStyle = CASStyle | WS_EX_RIGHT; //Align Text To The Right
                        break;
                }
                SetWindowLong(CASHandle, GWL_EXSTYLE, CASStyle); //Apply On ComboBox
            }
        }

        /// <summary>
        /// Align The ComboBox ScrollBar
        /// </summary>
        private void CASAlignScroll()
        {
            if (CASHandle != IntPtr.Zero) //If Valid Handle
            {
                int CASStyle = GetWindowLong(CASHandle, GWL_EXSTYLE); //Get ComboBox Style
                switch (CASScroll)
                {
                    case CASAlignment.CASLeft:
                        CASStyle = CASStyle | WS_EX_LEFTSCROLLBAR; //Align ScrollBare To The Left
                        break;
                }
                SetWindowLong(CASHandle, GWL_EXSTYLE, CASStyle); //Apply On ComboBox
            }
        }

        /// <summary>
        /// Align The ComboBox Button
        /// </summary>
        private void CASAlignButton()
        {
            if (CASHandle != IntPtr.Zero) //If Valid Handle
            {
                int CASStyle = GetWindowLong(this.Handle, GWL_EXSTYLE); //Get ComboBox Style

                switch (CASButton)
                {
                    case CASAlignment.CASLeft:
                        CASStyle = CASStyle | WS_EX_RIGHT; //Align ComboBox Button To The Left
                        break;

                }

                SetWindowLong(this.Handle, GWL_EXSTYLE, CASStyle); //Apply On ComboBox
            }

        }

        /// <summary> 
        /// Set Text Alignment 
        /// </summary> 
        public CASAlignment CASDropListAlignment
        {
            get
            {
                return CASList; //Get Value
            }
            set
            {
                if (CASList == value) //If Not Valid Value
                    return; 

                CASList = value; //Set Value
                CASAlignList(); //Call AlignList Method
            }
        }


        /// <summary> 
        /// Align ScrollBar 
        /// </summary> 
        public CASAlignment CASScrollAlignment
        {
            get
            {
                return CASScroll; //Get Value
            }
            set
            {
                if (CASScroll == value) //If Not Valid Value
                    return;  

                CASScroll = value; //Set Value
                CASAlignScroll(); //Call AlignScroll Method
            }
        }

        /// <summary> 
        /// Align ComboBox Button
        /// </summary> 
        public CASAlignment CASDropButtonAlignment
        {
            get
            {
                return CASButton; //Get Value
            }
            set
            {
                if (CASButton == value) //If Not Valid Value
                    return; 

                CASButton = value; //Set Value
                CASAlignButton(); //Call AlignButton Method
            }
        }


    }

}