using DevExpress.XtraBars.Ribbon;
using Globus5.WPF.Comum;
using System.Xml;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;
using FGlobus.Componentes.WinForms;
using System.Xml.Linq;
using DevExpress.XtraBars;
using System.IO;
using System.Drawing;

namespace Globus5.WPF.Sistemas.CriaMenuPrincipal
{
    public partial class MenuPrincipal : RibbonForm
    {
        #region Construtor

        private class ItemMenu
        {
            public ItemMenu(IEnumerable<XElement> campos)
            {
                campos.Aggregate(this, (a, b) =>
                {
                    FieldInfo fieldInfo = a.GetType().GetField(b.Name.ToString());
                    if (fieldInfo != null)
                        fieldInfo.SetValue(a, b.Value);

                    a.Nivel1 = b.Parent.Parent.Name.ToString();
                    a.Nivel2 = b.Parent.Parent.Parent.Name.ToString();
                    a.Nivel3 = b.Parent.Parent.Parent.Parent == null
                        ? ""
                        : b.Parent.Parent.Parent.Parent.Name.ToString();
                    return a;
                });
            }

            public string NomeItemMenu;
            public string Caption;
            public string Descricao;
            public string Icone;
            public string NomeTela;
            public string DLL;
            public string DataInclusao;
            public string Usuario;
            public string Nivel1;
            public string Nivel2;
            public string Nivel3;
        };

        public MenuPrincipal()
        {
            InitializeComponent();

            this.appMnMenuPrincipal.ItemLinks.Clear();

            XDocument _xmlDataDoc = XDocument.Load(
                new StreamReader(
                    Assembly.GetExecutingAssembly().GetManifestResourceStream(
                        String.Concat(this.GetType().Namespace, ".MenuPrincipal.Config"))));

            //Bitmap image = new Bitmap(
            //        Assembly.GetExecutingAssembly().GetManifestResourceStream(
            //            String.Concat(this.GetType().Namespace, ".info_icon_gfo.png")));

            //System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream();
            //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //_BinaryFormatter.Serialize(_MemoryStream, image);

            //string constructedString = Convert.ToBase64String(_MemoryStream.ToArray());
            //if (constructedString == null)
            //{

            //}
            //BarItem

            BarSubItem barSubItemNivel1 = null;
            BarSubItem barSubItemNivel2 = null;
            foreach (var item in _xmlDataDoc.Descendants("Nivel1")
                .Aggregate(new List<ItemMenu>(), (nivel1, itemNivel1) =>
                {
                    nivel1.Add(new ItemMenu(itemNivel1.Elements()));
                    nivel1.AddRange(itemNivel1.Descendants()
                                        .Aggregate(new List<ItemMenu>(), (nivel2, itemNivel2) =>
                                        {
                                            nivel2.Add(new ItemMenu(itemNivel2.Elements()));
                                            return nivel2;
                                        }));
                    return nivel1;
                })
                .Where(w => w.NomeItemMenu != null))
            {
                if (item.Nivel1.Equals("MenuPrincipal"))
                {
                    barSubItemNivel1 = new BarSubItem()
                    {
                        Caption = item.Caption,
                        Glyph = StringToImage(item.Icone),
                        Description = item.Descricao
                    };

                    this.appMnMenuPrincipal.AddItem(barSubItemNivel1);
                    continue;
                }

                if (item.Nivel1.Equals("Nivel1") && item.Nivel2.Equals("MenuPrincipal"))
                {
                    if (String.IsNullOrEmpty(item.NomeTela))
                    {
                        barSubItemNivel2 = new BarSubItem()
                        {
                            Caption = item.Caption,
                            Glyph = StringToImage(item.Icone),
                            Description = item.Descricao
                        };
                        barSubItemNivel1.AddItem(barSubItemNivel2);
                    }
                    else
                    {                        
                        BarButtonItem barButtonItem = new BarButtonItem()
                        {
                            Caption = item.Caption,
                            Tag = item,
                            Glyph = StringToImage(item.Icone),
                            Description = item.Descricao,
                            PaintStyle = BarItemPaintStyle.CaptionGlyph
                        };
                        barButtonItem.ItemClick += new ItemClickEventHandler(barButtonItem_ItemClick);
                        barSubItemNivel1.AddItem(barButtonItem);
                    }

                    continue;
                }

                if (item.Nivel1.Equals("Nivel2") && item.Nivel2.Equals("Nivel1"))
                {                    
                    BarButtonItem barButtonItem = new BarButtonItem()
                    {
                        Caption = item.Caption,
                        Tag = item,
                        Glyph = StringToImage(item.Icone),
                        Description = item.Descricao,
                        PaintStyle = BarItemPaintStyle.CaptionGlyph
                    };
                    barButtonItem.ItemClick += new ItemClickEventHandler(barButtonItem_ItemClick);

                    barSubItemNivel2.AddItem(barButtonItem);
                }
            }

            this.appMnMenuPrincipal.AddItem(this.brBtnItmSaida);
            this.appMnMenuPrincipal.ItemLinks[this.appMnMenuPrincipal.ItemLinks.Count - 1].BeginGroup = true;
        }

        public Image StringToImage(string imageString)
        {
           // imageString = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAAlwSFlzAAAK+AAACvgBZiY8/QAABBJJREFUWEfFVzFr20AUVreO/Ssdu2ZroUMDHdqtZDSdPBXTJYgMwWQoIkMwHQLKEDgPAXkIyEtAGQzqEFAGgzxk0JBBQwcNHV6/76STT5bktCGQgw+f4KT3ve+9e+/5hYg4z7pI4DnxrMa1+sEskBoXgajqWWEf8LkHnaoV4gRXiQRXaQWzT0TNgctE4psMr65V1wSKovg3/MY5YHw0liIvWupNzkAaxvuWfxHLzseBxL/0Gf1+i4CaKmngHM9AlmUl7jJxD9wWARLyzx8mEMG4TcKhzFoBeJbneYl7wDJIo+kqlXRZYvR91CJA7+ObUvJtCiTLDOfWJBzGmEZtDzcNJstEktsKMLJJIL8vHBKgE8yBrSH4NJAdC5pAdB23EF5FEs4jCYhLYBYiIYFp0CKgvf8Fw39EJ9uDC+d4lkQ0gUYSVomGmDZCYitkK5Bneek9wsiPMtF6V2WY55oEKqObOTC5SMUjpqmMz4GzRNzTEqOTRIbHsQwOAvGnkSSrXJJlLhPsO5dlvMCefGsF+kJAw8ldIfEKWBYSEbe5hDe5BItcfNxr94eS9A45dF9oTM7Dpv0Nr43xBoG+EDQUoApUoFJhdBLL8BDeQ3LtfaWAd2YR6PCahg1KBZBUzN6ua7gto7MMCkxDyfGuvbzTQMfXQHtcSW4bp9MVAdV7C4ItGR0tYvFOVSvcNoFuwyBUOVwT6AuBmq8zevyzTC41i2QCmd0jH7UhRf1gDSnBXCCBbV4b4/xdE+gJgbrsvlLhVbf3JKgJWHFu7M0V31Sg7xbYBIwCzHLtPUqq7b2twGDfk71vY3n3ZdioenYFNHskoVoXIpshCpE/aytA79OVbqntVSWfiX2KsLD5sJIODzzxfioWLRS/0MF3nGyVOSUBuxGZZoSG5CPeZlGBHKRGh5OW5wkIRdfo9zjPELg/fBl892S4P8E1jTR2Pg3R4HIHtjQEswPRJMAuWIGllxXOXuwL7AeUP1qg88EzlmEaVTASIGnpcXyb6QKWsIChcAXXqbz5MOicIdYELMOm7tsEqACNM/7jY+BEQdLSOH/HJ/D8mN6jWR0pKKUgO644CKg5CLzf6yfQaMfV0MGWPEGh2Vz23c5/CwqR1GU4zeC18RxlO0ZvMAReP0TAWyJzF3t64jHzAO87FyWvgbsfExgqCEpOhAskG6Q28yC9JvxLYJbK67fbFID8o8VIdue7gswspx+AdZ1Xylyrz1/d9tXCjPfGAHFmrCk3Qa9puMTnLSEAgXfXu/Jy+qo2bkiYMayeiDAZmXmAc+D/AoI2hlmdhEy62mDl/b8Q2PzYY55rAmYYsWt1PaBYtYFku4bSxxjnO5oA5/zxIWB+ua/h6jG8xr77tAQey/yp3nv+/4ZP5cljv/MXuhy9aDHRIFUAAAAASUVORK5CYII=";
            if (!String.IsNullOrEmpty(imageString))
            {
                try
                {
                    byte[] array = Convert.FromBase64String(imageString);
                    Image image = Image.FromStream(new MemoryStream(array));
                    image.Save(@"c:\temp\operacina.png");

                    return image;
                }
                catch (Exception)
                {
                    return null;
                }

            }
            return null;
        }

        private void barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemMenu itemMenu = (ItemMenu)e.Item.Tag;
            Assembly assemblyProjetoPesquisas = Assembly.LoadFile(itemMenu.DLL);
            Type tela = assemblyProjetoPesquisas.GetExportedTypes()
                .Where(w => w.Name.Equals(itemMenu.NomeTela))
                .SingleOrDefault();

            gfo.GestaoDeFrotaOnLineWS gfows = new gfo.GestaoDeFrotaOnLineWS();
            gfows.ConectaUsuarioWap();
            if (tela.BaseType.FullName.Equals(typeof(MasterFormCadastro).ToString()))
            {
                MasterFormCadastro item = Activator.CreateInstance(tela) as MasterFormCadastro;
                item.MdiParent = this;
                item.MaximizeBox = false;
                item.Show();
            }
        }

        #endregion

        private void gridControl1_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                Assembly assemblyProjetoPesquisas = Assembly.Load(new AssemblyName("BgmRodotec.Globus5.Cadastros.GestaoDeFrotaOnLine"));//@"c:\Globus5\WPF\Distribuicao\BgmRodotec.Globus5.Cadastros.GestaoDeFrotaOnLine.dll");
                Type tela = assemblyProjetoPesquisas.GetExportedTypes()
                    .Where(w => w.Name.Equals("Operadora"))
                    .SingleOrDefault();

                gfo.GestaoDeFrotaOnLineWS gfows = new gfo.GestaoDeFrotaOnLineWS();
                gfows.ConectaUsuarioWap();
                if (tela.BaseType.FullName.Equals(typeof(MasterFormCadastro).ToString()))
                {
                    MasterFormCadastro item = Activator.CreateInstance(tela) as MasterFormCadastro;
                    item.MdiParent = this;
                    item.MaximizeBox = false;
                    item.Show();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}