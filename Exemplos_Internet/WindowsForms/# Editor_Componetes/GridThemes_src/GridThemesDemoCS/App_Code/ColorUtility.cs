using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

/// <summary>
/// Summary description for ColorUtility
/// </summary>
public class ColorUtility
{
    public static Color BlendColors(Color color1, Color color2)
    {
        // return the average between both colors
        int r = (color1.R + color2.R) / 2;
        int b = (color1.B + color2.B) / 2;
        int g = (color1.G + color2.G) / 2;

        return System.Drawing.Color.FromArgb(r, g, b);
    }

    public static Color BlendColors(Color color1, string sColor2)
    {
        // return the average between both colors; the second is supplied as a string
        Color color2 = System.Drawing.ColorTranslator.FromHtml(sColor2);
        return BlendColors(color1, color2);
    }
}
