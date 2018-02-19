using System.Drawing; //Import Drawing NameSpace

namespace System.Windows.Forms
{
   class FontCbo //Font ComboBox Class
    {
       public Font FCFont; //Font Used

       public FontCbo(Font FCCurrFont) 
        {
            FCFont = FCCurrFont;  //Set This Font Equal To Font Supplied
        } 
    
       /// <summary>
       /// Override ToString Method To Display Current Font's Name
       /// </summary>
       /// <returns></returns>
        public override string ToString() 
        { 
            return FCFont.Name; //Display Font Name
        } 

    }
}
