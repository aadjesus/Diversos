using System;
using System.Drawing;
using System.Windows.Forms;

namespace CrazyCombos
{
    public partial class frmCrazy : Form //Testing Form
    {
        private SolidBrush FontForeColour; //Font's Colour

        public frmCrazy()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Upfront Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCrazy_Load(object sender, EventArgs e)
        {
            imgCboCrazy.Items.Add(new ImgCbo.ICItem("No Icon", -1)); //Insert Text Only In ImageCombo
            imgCboCrazy.Items.Add(new ImgCbo.ICItem("Blue Hills", 0, true, Color.Green)); //First Real Item In ImageCombo
            imgCboCrazy.Items.Add(new ImgCbo.ICItem("Sunset", 1, false, Color.Blue)); //Second ImageCombo Item
            imgCboCrazy.Items.Add(new ImgCbo.ICItem("Water Lillies", 2, false, Color.Gray)); //Third ImageCombo Item
            imgCboCrazy.Items.Add(new ImgCbo.ICItem("Winter", 3, false, Color.Red)); //Fourth ImageCombo Item

            //Colour Combo
            //Add Each Known Colour Into Colour ComboBox
            foreach (string CurrColourName in System.Enum.GetNames(typeof(System.Drawing.KnownColor))) //Get System's Known Colours
            {
                cboColourCrazy.Items.Add(Color.FromName(CurrColourName)); //Add Known Colours
            }

            //Alignment Combo
            string[] AllAlignArr = new string[10]; //Array For Combo Items

            int intAllLoop; //Loop Counter
            for (intAllLoop = 0; intAllLoop <= 9; intAllLoop++) //Initiate Loop
            {
                AllAlignArr[intAllLoop] = "Item " + intAllLoop; //Fill Each Array Element With Appropriate Text

                cboAlignAllCrazy.Items.Add(AllAlignArr[intAllLoop]); //Display Items In Alignment Combo

                //Set Alignment Combo's Text To Left ( Default )
                cboAlignAllCrazy.CASDropListAlignment = ComboAlignSettings.CASAlignment.CASLeft;
            }

            //FontCombo
            FontFamily[] families = FontFamily.Families; //Obtain & Store System fonts Into Array
    
            foreach (FontFamily family in families) //Loop Through System Fonts
            { 
                FontStyle style = FontStyle.Bold; //Set Current Font's Style To bold
        
                //These Are Only Available In Italic, Not In "Regular", So Test For Them, Else, Exception!!
                if (family.Name == "Monotype Corsiva" || family.Name == "Brush Script MT" || family.Name == "Harlow Solid Italic" || family.Name == "Palace Script MT" || family.Name == "Vivaldi") 
                { 
                        style = style | FontStyle.Italic; //Set Style To Italic, To Overt "Regular" & Exception
                }


            cboFontsCrazy.Items.Add(new FontCbo(new Font(family.Name, 12, style, GraphicsUnit.Point))); //Display The Font Combo Items
            }
 
        }

        /// <summary>
        /// Make Sure All Items Will Fit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboColourCrazy_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
        {
            Random ColourRnd = new Random(); //Set A Random value
            e.ItemHeight = ColourRnd.Next(20, 40);

        }

        /// <summary>
        /// Paint Background, etc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboColourCrazy_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index < 0) //Don't We Have A List ¿
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                return; 
            }
            //Get Colour Object From Items List 
            Color CurrColour = (Color)cboColourCrazy.Items[e.Index];

            //Create A Rectangle To Fit New Item 
            Rectangle ColourSize = new Rectangle(2, e.Bounds.Top + 2, e.Bounds.Width, e.Bounds.Height - 2);

            Brush ColourBrush; //New Colour Brush To Draw With

            e.DrawBackground(); //Draw Item's Background
            e.DrawFocusRectangle(); //Draw Item's Focus Rectangle

            if (e.State == System.Windows.Forms.DrawItemState.Selected) //If Item Selected
            {
                ColourBrush = Brushes.White; //Change To White
            }
            else
            {
                ColourBrush = Brushes.Black; //Change Back to Black
            }

            e.Graphics.DrawRectangle(new Pen(CurrColour), ColourSize); //Draw New Item Rectangle With Current Colour
            e.Graphics.FillRectangle(new SolidBrush(CurrColour), ColourSize); //Fill New Item rectangle With Current Colour

            //Add Border Around Rectangle
            ColourSize.Inflate(1, 1); //Border Size
            e.Graphics.DrawRectangle(Pens.Black, ColourSize); //Draw New Border

            //Draw Current Colour Name, In The Middle 
            e.Graphics.DrawString(CurrColour.Name, cboColourCrazy.Font, ColourBrush, e.Bounds.Height + 5, ((e.Bounds.Height - cboColourCrazy.Font.Height) / 2) + e.Bounds.Top);
        }

        /// <summary>
        /// Center The Text Again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCenterCrazy_DropDown(object sender, EventArgs e)
        {
            cboCenterCrazy.Items.Clear(); //Clear ComboBox
            string[] stringArr = new string[10]; //New Items

            int intLoop; //Loop Counter

            for (intLoop = 0; intLoop <= 9; intLoop++) //Start Loop
            {
                stringArr[intLoop] = "Item " + intLoop; //Add Items To Array

                //Center Align Items Again
                cboCenterCrazy.Items.Add(stringArr[intLoop].PadLeft(((cboCenterCrazy.DropDownWidth / 3) - (stringArr[intLoop].Length)) / 2));

            }

        }

        /// <summary>
        /// Drawing Of Fonts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFontsCrazy_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush FontBrush; //Brush To Be used

            //If No Current Colour
            if (FontForeColour == null)
            {
                FontForeColour = new SolidBrush(e.ForeColor); //Set ForeColour
            }
            else
            {
                if (!FontForeColour.Color.Equals(e.ForeColor)) //Fore Colour Changed, Create New Brush
                {
                    FontForeColour.Dispose(); //Dispose Old Brush
                    FontForeColour = new SolidBrush(e.ForeColor); //Create New Brush
                }
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) //Set Appropriate Brush
            {
                FontBrush = SystemBrushes.HighlightText;
            }
            else
            {
                FontBrush = FontForeColour;
            }

            Font font = ((FontCbo)cboFontsCrazy.Items[e.Index]).FCFont; //Current item's Font
            
            e.DrawBackground(); //Redraw Item Background

            e.Graphics.DrawString(font.Name, font, FontBrush, e.Bounds.X, e.Bounds.Y); //Draw Current Font

            e.DrawFocusRectangle(); //Draw Focus Rectangle Around It

        }

        /// <summary>
        /// Item Height Measurements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFontsCrazy_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            Font font = ((FontCbo)cboFontsCrazy.Items[e.Index]).FCFont; //Get Current Font In ComboBox

            SizeF stringSize = e.Graphics.MeasureString(font.Name, font); //determine Its Size

            e.ItemHeight = (int)stringSize.Height; //Set Appropriate Height
            e.ItemWidth = (int)stringSize.Width; //Set Appropriate Width

        }

        /// <summary>
        /// Button To Right Align Alignment Combo's Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRightText_Click(object sender, EventArgs e)
        {

            if (cboAlignAllCrazy.CASDropListAlignment == ComboAlignSettings.CASAlignment.CASLeft) //If Left
            {
                cboAlignAllCrazy.CASDropListAlignment = ComboAlignSettings.CASAlignment.CASRight; //Set To Right
            }
 
        }

        /// <summary>
        /// Button To Left Align Alignment Combo's ScrollBar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeftAlignScroll_Click(object sender, EventArgs e)
        {
            if (cboAlignAllCrazy.CASScrollAlignment == ComboAlignSettings.CASAlignment.CASRight) //If Right Aligned
            {
                cboAlignAllCrazy.CASScrollAlignment = ComboAlignSettings.CASAlignment.CASLeft; //Set To Left
            }
        }

        /// <summary>
        /// Button To Left Align Alignment Combo's Drop Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeftAlignButton_Click(object sender, EventArgs e)
        {
            if (cboAlignAllCrazy.CASDropButtonAlignment == ComboAlignSettings.CASAlignment.CASRight) //If Right
            {
                cboAlignAllCrazy.CASDropButtonAlignment = ComboAlignSettings.CASAlignment.CASLeft; //Set To Left
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); //Exit
        }

    }
}