//--------------------------------------------------------
// MeshGeometry3DExtractor.cs (c) 2007 by Charles Petzold
//--------------------------------------------------------
using System;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using System.Xml;

namespace Petzold.MeshGeometry3DExtractor
{
    public partial class MeshGeometry3DExtractor : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new MeshGeometry3DExtractor());
        }

        public MeshGeometry3DExtractor()
        {
            InitializeComponent();

            // Get the assembly with the Petzold.Media3D classes.
            Assembly asmbly = 
                Assembly.GetAssembly(typeof(Petzold.Media3D.ModelVisualBase));
            Type[] types = asmbly.GetTypes();

            // Loop through the types.
            foreach (Type type in types)
                if (type.IsPublic && type.IsClass && !type.IsAbstract)
                {
                    // Get all the properties.
                    PropertyInfo[] props = type.GetProperties(BindingFlags.Public | 
                                                              BindingFlags.Instance);
                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.PropertyType == typeof(MeshGeometry3D))
                        {
                            // Add the item to the list box.
                            ClassAndProperty item = new ClassAndProperty(type, prop);
                            lstboxClass.Items.Add(item);
                        }
                    }
                }
        }

        void ClassListBoxOnSelectionChanged(object sender, 
                                            SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            ClassAndProperty item = lstbox.SelectedItem as ClassAndProperty;
            itemsProperties.Items.Clear();

            if (item != null)
            {
                // Get constructor and properties.
                ConstructorInfo construct = item.Type.GetConstructor(Type.EmptyTypes);
                object obj = construct.Invoke(null);
                PropertyInfo[] props = item.Type.GetProperties(BindingFlags.Public | 
                                                               BindingFlags.Instance);
                foreach (PropertyInfo prop in props)
                {
                    if (prop.PropertyType == typeof(int) || 
                        prop.PropertyType == typeof(Point3D) || 
                        prop.PropertyType == typeof(Vector3D) || 
                        prop.PropertyType == typeof(double))
                    {
                        PropertyAndValue propvalue = new PropertyAndValue(prop, prop.GetValue(obj, null).ToString());
                        itemsProperties.Items.Add(propvalue);
                    }
                }
                DoDump();
            }
        }
        void ValueOnTextChanged(object sender, TextChangedEventArgs args)
        {
            DoDump();
        }

        // Gets the MeshGeometry3D and converts to XAML.
        void DoDump()
        {
            txtboxDump.Text = String.Empty;
            ClassAndProperty item = lstboxClass.SelectedItem as ClassAndProperty;

            if (item != null)
            {
                // Create the object with the MeshGeometry3D property.
                ConstructorInfo construct = 
                    item.Type.GetConstructor(Type.EmptyTypes);
                object obj = construct.Invoke(null);
                bool isError = false;

                // Get all the proeprties and set them.
                foreach (object objPropValue in itemsProperties.Items)
                {
                    PropertyAndValue propvalue = objPropValue as PropertyAndValue;
                    string str = propvalue.Value;

                    Type typeProperty = propvalue.Property.PropertyType;
                    MethodInfo method = typeProperty.GetMethod("Parse", 
                                                               new Type[] { typeof(string) });
                    object objValue;

                    try
                    {
                        objValue = method.Invoke(obj, new object[] { str });
                    }
                    catch
                    {
                        Console.WriteLine("catch1");
                        txtboxDump.Text += "Value for property " + 
                                propvalue.Property.Name + " cannot be parsed\n";
                        isError = true;
                        continue;
                    }

                    try
                    {
                        propvalue.Property.SetValue(obj, objValue, null);
                    }
                    catch
                    {
                        Console.WriteLine("catch2");
                        txtboxDump.Text += "Value for property " + 
                                propvalue.Property.Name + " cannot be set\n";
                        isError = true;
                        continue;
                    }
                }
                if (!isError)
                {
                    // Dump the MeshGeometry3D.
                    MeshGeometry3D geo = item.Property.GetValue(obj, null) as MeshGeometry3D;

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.IndentChars = new string(' ', 4);
                    settings.NewLineOnAttributes = true;
                    settings.OmitXmlDeclaration = true;

                    StringBuilder strbuild = new StringBuilder();
                    XmlWriter xmlwrite = XmlWriter.Create(strbuild, settings);

                    XamlWriter.Save(geo, xmlwrite);
                    txtboxDump.Text = strbuild.ToString();
                }
            }
        }
    }
}
