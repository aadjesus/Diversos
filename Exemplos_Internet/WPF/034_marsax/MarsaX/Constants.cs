using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MarsaX
{
    static class Constants
    {
        static Constants()
        {
            //read the values from App.Config
            int.TryParse(ConfigurationSettings.AppSettings["rows"].ToString(), out Constants.ROWS);
            int.TryParse(ConfigurationSettings.AppSettings["columns"].ToString(), out Constants.COLUMNS); 
            bool.TryParse(ConfigurationSettings.AppSettings["should3DModelFlipOnMouseOver"].ToString(), 
                out Constants.should3DModelFlipOnMouseOver);
            Constants.savedImageLocation = 
                ConfigurationSettings.AppSettings["savedImageLocation"].ToString() != string.Empty ?
                ConfigurationSettings.AppSettings["savedImageLocation"].ToString() : @"c:\";
            bool.TryParse(ConfigurationSettings.AppSettings["stretchImagesFor3DModels"].ToString(),
                out Constants.stretchImagesFor3DModels);  
        }


        #region Data
        //These cant be bigger than 2 and 25 as Yahoo ImageSearch API 
        //has a limit of 50 (so ROWS * COLS can't be > 50)
        public static int ROWS = 2;
        public static int COLUMNS = 25;
        //Dictates what happens when mouse moves over 3d model. 
        //If True 3d Model rotates around its X-Axis
        public static bool should3DModelFlipOnMouseOver = false;
        //Just to be able to use it as the Maximum for the Slider control 
        //(since Slider supports binding to Doubles ONLY)
        //allow 5 images to be shown when fully scrolled
        public static double COLUMNSTOSHOW = (double)COLUMNS-5; 
        //Saved image default location
        public static string savedImageLocation = @"c:\";
        //Set this true to stretch images to fill 3D model, 
        //otherwise images will be shown at natural ratio
        public static bool stretchImagesFor3DModels = true;


        #endregion
    }
}
