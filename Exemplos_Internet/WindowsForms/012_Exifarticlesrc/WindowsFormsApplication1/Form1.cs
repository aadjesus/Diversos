using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
//using com.drew.metadata;
//using com.drew.imaging.jpg;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Goheer.EXIF.EXIFextractor er2 = new Goheer.EXIF.EXIFextractor("c:\\tmp\\a\\20081011_001.jpg", "", "");

            foreach (System.Web.UI.Pair s in er2)
            {
                listBox1.Items.Add(s.First + " : " + s.Second);
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }


        private void WriteNewDescriptionInImage(string Filename, string NewDescription)
        {
            Image Pic;
            PropertyItem[] PropertyItems;
            byte[] bDescription = new Byte[NewDescription.Length];
            int i;
            string FilenameTemp;
            System.Drawing.Imaging.Encoder Enc = System.Drawing.Imaging.Encoder.Transformation;
            EncoderParameters EncParms = new EncoderParameters(1);
            EncoderParameter EncParm;
            ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");

            // copy description into byte array
            for (i = 0; i < NewDescription.Length; i++)
                bDescription[i] = (byte)NewDescription[i];

            // load the image to change
            Pic = Image.FromFile(Filename);

            // put the new description into the right property item
            PropertyItems = Pic.PropertyItems;
            PropertyItems[0].Id = 0x010e; // 0x010e as specified in EXIF standard
            PropertyItems[0].Type = 2;
            PropertyItems[0].Len = NewDescription.Length;
            PropertyItems[0].Value = bDescription;
            Pic.SetPropertyItem(PropertyItems[0]);

            // we cannot store in the same image, so use a temporary image instead
            FilenameTemp = Filename + ".temp";

            // for lossless rewriting must rotate the image by 90 degrees!
            EncParm = new EncoderParameter(Enc, (long)EncoderValue.TransformRotate90);
            EncParms.Param[0] = EncParm;

            // now write the rotated image with new description
            Pic.Save(FilenameTemp, CodecInfo, EncParms);

            // for computers with low memory and large pictures: release memory now
            Pic.Dispose();
            Pic = null;
            GC.Collect();

            // delete the original file, will be replaced later
            System.IO.File.Delete(Filename);

            // now must rotate back the written picture
            Pic = Image.FromFile(FilenameTemp);
            EncParm = new EncoderParameter(Enc, (long)EncoderValue.TransformRotate270);
            EncParms.Param[0] = EncParm;
            Pic.Save(Filename, CodecInfo, EncParms);

            // release memory now
            Pic.Dispose();
            Pic = null;
            GC.Collect();

            // delete the temporary picture
            System.IO.File.Delete(FilenameTemp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
//            ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");
            WriteNewDescriptionInImage("c:\\tmp\\a\\20081011_001.jpg","xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

            //Stream jpegFile = new FileStream("c:\\tmp\\a\\20081011_001.jpg", FileMode.Open);
            //Metadata metadata = JpegMetadataReader.readMetadata(jpegFile);
            //if (CodecInfo == null)
            //{

            //}
        }



    }
}
