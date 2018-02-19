//--------------------------------------------------
// SerializeGeometry.cs (c) 2007 by Charles Petzold
//--------------------------------------------------
using System;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using System.Xml;
using Petzold.AnimatableResourceDemo;

namespace Petzold.SerializeGeometry
{
    public class SerializeGeometry
    {
        [STAThread]
        public static void Main()
        {
            // Create mesh-generator object and set properties.
            SphereMeshGenerator2 meshgen = new SphereMeshGenerator2();
            meshgen.Slices = 10;
            meshgen.Stacks = 5;

            // Make the XML look nice.
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = new string(' ', 4);
            settings.NewLineOnAttributes = true;
            settings.OmitXmlDeclaration = true;

            // Dump the MeshGeometry3D.
            MeshGeometry3D mesh = meshgen.Geometry;
            StringBuilder strbuild = new StringBuilder();
            XmlWriter xmlwrite = XmlWriter.Create(strbuild, settings);
            XamlWriter.Save(mesh, xmlwrite);

            // Copy it to the clipboard.
            Clipboard.SetText(strbuild.ToString());
        }
    }
}
