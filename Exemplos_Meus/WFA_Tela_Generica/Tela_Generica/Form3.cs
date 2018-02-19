using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using DevExpress.XtraTreeList.Nodes;

namespace Tela_Generica
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            XDocument xDocumentTelasGenericas = XDocument.Load(new StreamReader(
                assembly.GetManifestResourceStream(assembly.GetManifestResourceNames()
                    .Where(w => w.IndexOf("XMLFile1.xml") > 0)
                    .SingleOrDefault())));

            
            treeList1.Nodes.Clear();
            Func<TreeListNode, XElement, int> delegateElementostreeList1 = null;
            delegateElementostreeList1 = delegate(TreeListNode treeListNodes, XElement xElements)
            {
                xElements.Nodes()
                    .ToList()
                    .ForEach(f =>
                    {
                        XElement xElement = f as XElement;                        
                        
                        TreeListNode treeListNode = null;
                        if (treeListNodes == null)
                            treeListNode = treeList1.AppendNode(new object[] { xElement.Name.LocalName, 0 }, null);                            
                        else
                            treeListNode = treeList1.AppendNode(new object[] { xElement.Name.LocalName , 1}, treeListNodes);

                        delegateElementostreeList1(treeListNode, xElement);
                    });
                return 0;
            };
            delegateElementostreeList1(null,  xDocumentTelasGenericas.Root);



            treeView1.Nodes.Clear();
            Func<TreeNode, XElement, int> delegateElementos = null;
            delegateElementos = delegate(TreeNode treeNodes, XElement xElements)
            {
                xElements.Nodes()
                    .ToList()
                    .ForEach(f =>
                    {
                        XElement xElement = f as XElement;
                        //xElement.Attribute("Descricao").Value

                        TreeNode treeNode = new TreeNode(xElement.Name.LocalName);

                        if (treeNodes == null)
                            treeView1.Nodes.Add(treeNode);
                        else
                            treeNodes.Nodes.Add(treeNode);

                        delegateElementos(treeNode,  xElement);
                    });
                return 0;
            };

            delegateElementos(null,  xDocumentTelasGenericas.Root);
        }

    }
}
