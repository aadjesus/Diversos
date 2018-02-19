using System.Drawing;

namespace System.Windows.Forms
{
    class ImgCbo : ComboBox //Inherits ComboBox
    {
        private ImageList ICImagList = new ImageList(); //Create New ImageList

		public ImgCbo()
		{
			//Set DrawMode
			this.DrawMode = DrawMode.OwnerDrawFixed;	
		}

		/// <summary>
        /// ImageList Property
		/// </summary>
		public ImageList ICImageList 
		{
			get 
			{
				return ICImagList; //Get value
			}
			set 
			{
				ICImagList = value; //Set Value
			}
		}

		/// <summary>
		/// Override OnDrawItem, To Be able To Draw Images, Text, And Font Formatting
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			e.DrawBackground(); //Draw Background Of Item
			e.DrawFocusRectangle(); //Draw Its rectangle

			if (e.Index < 0) //Do We Have A Valid List ¿

				//Just Draw Indented Text
				e.Graphics.DrawString(this.Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + ICImagList.ImageSize.Width, e.Bounds.Top);

			else //We Have A List
			{
				
				if (this.Items[e.Index].GetType() == typeof(ICItem))  //Is It A ImageCombo Item ¿
				{															

					ICItem ICCurrItem = (ICItem)this.Items[e.Index]; //Get Current Item

					//Obtain Current Item's ForeColour
                    Color ICCurrForeColour = (ICCurrItem.ICForeColour != Color.FromKnownColor(KnownColor.Transparent)) ? ICCurrItem.ICForeColour : e.ForeColor;

					//Obtain Current Item's Font
                    Font ICCurrFont = ICCurrItem.ICHighLight ? new Font(e.Font, FontStyle.Bold) : e.Font;

					if (ICCurrItem.ICImageIndex != -1) //If In Actual List ( Which Needs Images )
					{
						//Draw Image
						this.ICImageList.Draw(e.Graphics, e.Bounds.Left, e.Bounds.Top, ICCurrItem.ICImageIndex);

						//Then, Draw Text In Specified Bounds
                        e.Graphics.DrawString(ICCurrItem.ICText, ICCurrFont, new SolidBrush(ICCurrForeColour), e.Bounds.Left + ICImagList.ImageSize.Width, e.Bounds.Top);
					}
					else //No Image Needed, Index = -1

						//Just Draw The Indented Text
						e.Graphics.DrawString(ICCurrItem.ICText, ICCurrFont, new SolidBrush(ICCurrForeColour), e.Bounds.Left + ICImagList.ImageSize.Width, e.Bounds.Top);

				}
				else //Not An ImageCombo Box Item
				
					//Just Draw The Text
					e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + ICImagList.ImageSize.Width, e.Bounds.Top);
				
			}

			base.OnDrawItem (e);
		}

       public class ICItem : object
        {

            private string ICIText = null;	//ImageCombo Item Text

            private int ICIImageIndex = -1; //Image Combo Item Image Index

            private bool ICIHighlight = false; //Highlighted ¿

            private Color ICIForeColour = Color.FromKnownColor(KnownColor.Transparent); //ImageCombo Item ForeColour

            public ICItem()
            {
            }

            /// <summary>
            /// Text & Image Index Only
            /// </summary>
            /// <param name="ICIItemText"></param>
            /// <param name="ICImageIndex"></param>
            public ICItem(string ICIItemText, int ICImageIndex) //First Overload
            {
                ICIText = ICIItemText; //Text
                ICIImageIndex = ICImageIndex; //Image Index
            }

            /// <summary>
            /// Text, Image Index, Highlight, ForeColour
            /// </summary>
            /// <param name="ICIItemText"></param>
            /// <param name="ICImageIndex"></param>
            /// <param name="ICHighLight"></param>
            /// <param name="ICForeColour"></param>
            public ICItem(string ICIItemText, int ICImageIndex, bool ICHighLight, Color ICForeColour) //Second Overload
            {
                ICIText = ICIItemText; //Text
                ICIImageIndex = ICImageIndex; //Image Index
                ICIHighlight = ICHighLight; //Highlighted ¿
                ICIForeColour = ICForeColour; //ForeColour
            }

            /// <summary>
            /// ImageCombo Item Text
            /// </summary>
            public string ICText
            {
                get
                {
                    return ICIText; //Get Value
                }
                set
                {
                    ICIText = value; //Set Value
                }
            }

            /// <summary>
            /// Image Index
            /// </summary>
            public int ICImageIndex
            {
                get
                {
                    return ICIImageIndex; //Get Value
                }
                set
                {
                    ICIImageIndex = value; //Set Value
                }
            }

            /// <summary>
            /// Highlighted ¿
            /// </summary>
            public bool ICHighLight
            {
                get
                {
                    return ICIHighlight; //Get Value
                }
                set
                {
                    ICIHighlight = value; //Set Value
                }
            }

            /// <summary>
            /// ForeColour
            /// </summary>
            public Color ICForeColour
            {
                get
                {
                    return ICIForeColour; //Get Value
                }
                set
                {
                    ICIForeColour = value; //Set Value
                }
            }

            /// <summary>
            /// Override ToString To Return Item Text
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return ICIText;
            }

        }
		
    }
}
