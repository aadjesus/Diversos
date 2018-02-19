using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace SerializarIMG
{
    public interface IAddInterface
    {
        string SerializarImagem();
    }

    [ClassInterface(ClassInterfaceType.None)]
    public class SerializarIMG : IAddInterface
    {
        private static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public string SerializarImagem()
        {
            OpenFileDialog pathImagem = new OpenFileDialog();

            pathImagem.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            pathImagem.Title = "Selecione o Arquivo";

            if (pathImagem.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImageList imgList = new ImageList();
                    imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
                    imgList.ImageSize = new Size(16, 16);
                    imgList.TransparentColor = Color.Transparent;
                    imgList.Images.Clear();
                    imgList.Images.Add(Image.FromFile(pathImagem.FileName));

                    return ImageToBase64(imgList.Images[0], ImageFormat.Png);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Concat(ex));
                }
            }

            return "";
        }

        //regasm SimpleDll.dll /tlb:Add.tlb
        //c:\windows\Microsoft.NET\Framework\v3.5\csc.exe /t:library /out:SerializarImagem.dll *.cs
        //c:\windows\Microsoft.NET\Framework\v2.0.50727\regasm.exe SerializarImagem.dll /tlb:SerializarImagem.tlb

        //c:\windows\Microsoft.NET\Framework\v3.5\csc /t:library /out:SimpleDll.dll *.cs
        //c:\windows\Microsoft.NET\Framework\v2.0.50727\regasm SimpleDll.dll /tlb:Add.tlb
    }
}
