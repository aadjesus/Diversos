using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab.Buttons;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Serializing;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using FGlobus.Util;
using DevExpress.XtraGrid;
using System.Linq.Expressions;
using System.Reflection;
using System.Dynamic;

namespace ClassLibrary1
{
    public partial class AutorizaGridBGMx2 : UserControl
    {
        #region Construtor

        public AutorizaGridBGMx2()
        {
            InitializeComponent();

            _criarGridControl = !DesignMode;
            _criarColunas = true;
        }

        #endregion

        #region Atributos

        /// <summary>
        /// Enum para controlar layout
        /// </summary>
        public enum eLayOut
        {
            /// <summary>
            /// Vertical
            /// </summary>
            Vertical,
            /// <summary>
            /// Horizontal
            /// </summary>
            Horizontal,
            /// <summary>
            /// Abas
            /// </summary>
            Abas
        }

        private eLayOut _layOut;
        private List<ColunaAutorizaGridBGM> _colunas;
        private string _titulo;
        private bool _criarColunas;
        private bool _criarGridControl = false;
        private GridControl _gridControlDisponiveis;
        private GridControl _gridControlAssociados;

        #endregion

        #region Propriedades

        /// <summary>
        /// (Set/Get) Informa ou retorna o layout.
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("Informa ou retorna o layout.\nNome: LayOut")]
        [DisplayName("1) LayOut")]
        [DefaultValue(eLayOut.Vertical)]
        public eLayOut LayOut
        {
            get
            {
                this.pnlCtrlDisponiveis.Dock = DockStyle.Fill;
                this.pnlCtrlAssociados.Dock = DockStyle.Fill;

                return _layOut;
            }
            set
            {
                _layOut = value;
                AtualizarLayOut();
            }
        }

        /// <summary>
        /// (Get) Retorna as colunas.
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("Retorna as colunas.\nNome: Colunas")]
        [DisplayName("2) Colunas")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorAttribute(typeof(CustomCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public List<ColunaAutorizaGridBGM> Colunas
        {
            get
            {
                if (_colunas == null)
                    _colunas = new List<ColunaAutorizaGridBGM>();

                return _colunas;
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna o titulo.
        /// </summary>
        [Category(FGlobus.Util.Constantes.CATEGORIA)]
        [Description("Informa ou retorna o titulo.\nNome: titulo")]
        [DisplayName("Titulo")]
        [DefaultValue(null)]
        public string Titulo
        {
            get
            {
                return _titulo;
            }
            set
            {
                _titulo = value;
                this.grpCtrlAutorizaGrid.Text = _titulo;
            }
        }

        /// <summary>
        /// (Set/Get) Informa ou retorna se cria as colunas.
        /// </summary>
        [Browsable(false)]
        internal bool CriarColunas
        {
            get { return _criarColunas; }
            set { _criarColunas = value; }
        }

        #endregion

        #region Metodos controle

        private void xtrTbCtrlAutorizaGrid_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.xtrTbCtrlAutorizaGrid.CustomHeaderButtons
                .OfType<CustomHeaderButton>()
                .ToList()
                .ForEach(f => f.Visible = f.Tag.ToString().Equals(this.xtrTbCtrlAutorizaGrid.SelectedTabPage.Name));
        }

        private void AutorizaGridBGM_Load(object sender, EventArgs e)
        {
            PosicionarSplitterPosition();
            AtualizarLayOut();
            CriarGridControl();
        }

        protected override void WndProc(ref Message m)
        {
            if (DesignMode)
            {
                PosicionarSplitterPosition();
                CriarColunasDesignVisualizacao();
            }
            base.WndProc(ref m);
        }

        #endregion

        #region Metodos

        private void AtualizarLayOut()
        {
            this.xtrTbCtrlAutorizaGrid.Dock = DockStyle.Fill;
            this.spltCtnrCtrlAutorizaGrid.Dock = DockStyle.Fill;

            this.xtrTbCtrlAutorizaGrid.Visible = _layOut.Equals(eLayOut.Abas);
            this.spltCtnrCtrlAutorizaGrid.Visible = !this.xtrTbCtrlAutorizaGrid.Visible;

            this.tblLytPnlDisponiveis.Visible = this.spltCtnrCtrlAutorizaGrid.Visible;
            this.tblLytPnlAssociados.Visible = this.spltCtnrCtrlAutorizaGrid.Visible;
            int paddingRightBottom = 3;
            if (_layOut.Equals(eLayOut.Abas))
            {
                this.xtrTbCtrlAutorizaGrid.SelectedTabPage = this.xtrTbPgAutorizados;
                this.xtrTbCtrlAutorizaGrid.SelectedTabPage = this.xtrTbPgCadastrados;

                this.spltCtnrCtrlAutorizaGrid.Panel1.Controls.Remove(this.pnlCtrlDisponiveis);
                this.spltCtnrCtrlAutorizaGrid.Panel2.Controls.Remove(this.pnlCtrlAssociados);
                this.xtrTbPgCadastrados.Controls.Add(this.pnlCtrlDisponiveis);
                this.xtrTbPgAutorizados.Controls.Add(this.pnlCtrlAssociados);
                paddingRightBottom = 1;
            }
            else
            {
                this.xtrTbPgCadastrados.Controls.Remove(this.pnlCtrlDisponiveis);
                this.xtrTbPgCadastrados.Controls.Remove(this.pnlCtrlAssociados);

                this.spltCtnrCtrlAutorizaGrid.Panel1.Controls.Add(this.pnlCtrlDisponiveis);
                this.spltCtnrCtrlAutorizaGrid.Panel2.Controls.Add(this.pnlCtrlAssociados);
                this.spltCtnrCtrlAutorizaGrid.Visible = true;

                this.spltCtnrCtrlAutorizaGrid.Horizontal = _layOut.Equals(eLayOut.Vertical);
            }

            if (String.IsNullOrWhiteSpace(_titulo))
                this.Titulo = String.Concat("[", this.Name, "]");

            this.grpCtrlAutorizaGrid.Padding = new Padding(3, 3, paddingRightBottom, paddingRightBottom);
            this.pnlCtrlDisponiveis.Dock = DockStyle.Fill;
            this.pnlCtrlAssociados.Dock = DockStyle.Fill;

        }

        private void CriarColunasDesignVisualizacao()
        {
            try
            {
                if (_criarColunas)
                {
                    this.pnlColunasDesignDisp.Controls.Clear();
                    this.pnlColunasDesignAsso.Controls.Clear();
                    if (_colunas != null && _colunas.Count > 0)
                    {
                        Func<Button[]> delegateTextBox =
                            delegate()
                            {
                                return _colunas
                                    .Reverse<ColunaAutorizaGridBGM>()
                                    .Select(s => new Button()
                                    {
                                        Text = s.Titulo,
                                        Dock = DockStyle.Left,
                                        Width = s.Tamanho,
                                        TextAlign = ContentAlignment.MiddleLeft,
                                        FlatStyle = FlatStyle.Flat
                                    })
                                .ToArray();
                            };

                        this.pnlColunasDesignDisp.Controls.AddRange(delegateTextBox());
                        this.pnlColunasDesignAsso.Controls.AddRange(delegateTextBox());
                    }
                    _criarColunas = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void PosicionarSplitterPosition()
        {
            if (this.spltCtnrCtrlAutorizaGrid.Visible)
                this.spltCtnrCtrlAutorizaGrid.SplitterPosition = (_layOut.Equals(eLayOut.Vertical)
                    ? this.spltCtnrCtrlAutorizaGrid.Width
                    : this.spltCtnrCtrlAutorizaGrid.Height) / 2;
        }

        private void CriarGridControl()
        {
            if (_criarGridControl && !DesignMode)
            {
                this.pnlColunasDesignAsso.Dispose();
                this.pnlColunasDesignDisp.Dispose();
                this.lblVisualizacaoDispo.Dispose();
                this.lblVisualizacaoAsso.Dispose();

                Func<PanelControl, GridControl> deleGridControl = delegate(PanelControl panelControl)
                {
                    GridControl gridControl = new GridControl();
                    gridControl.Dock = DockStyle.Fill;
                    gridControl.UseEmbeddedNavigator = true;
                    gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
                    gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                    gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
                    gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                    gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;

                    GridView gridView = new GridView(gridControl);
                    gridView.BorderStyle = BorderStyles.NoBorder;
                    gridView.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                    gridView.OptionsView.ColumnAutoWidth = false;
                    gridView.OptionsView.ShowGroupedColumns = true;
                    gridView.OptionsView.ShowIndicator = false;
                    gridView.OptionsView.ShowFooter = true;
                    gridView.OptionsBehavior.AutoExpandAllGroups = true;
                    if (_colunas != null && _colunas.Count > 0)
                    {
                        gridView.Columns.AddRange(_colunas
                            .Select((s, index) => new GridColumn()
                            {
                                Caption = s.Titulo,
                                FieldName = s.Campo,
                                Visible = true,
                                VisibleIndex = index,
                            })
                            .ToArray());
                        GridColumn gridColumn = gridView.Columns
                            .OfType<GridColumn>()
                            .FirstOrDefault();

                        gridControl.MainView = gridView;
                        gridControl.ViewCollection.Add(gridView);

                        gridView.GroupSummary.Add(
                            new GridGroupSummaryItem(
                                DevExpress.Data.SummaryItemType.Count,
                                gridColumn.FieldName,
                                gridColumn, "Qtde")
                            );

                        gridColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    panelControl.Controls.Add(gridControl);
                    gridControl.BringToFront();

                    return gridControl;
                };

                _gridControlDisponiveis = deleGridControl(this.pnlCtrlDisponiveis);
                _gridControlAssociados = deleGridControl(this.pnlCtrlAssociados);

                _gridControlAssociados.DataSource = _associados;
                _gridControlDisponiveis.DataSource = _disponiveis;

                _criarGridControl = false;
            }
        }

        #endregion

        #region Classes auxiliares

        /// <summary>
        /// Classe para controlar as colunas do grid
        /// </summary>
        [ToolboxItem(false)]
        [DesignTimeVisible(false)]
        public class ColunaAutorizaGridBGM : Component
        {
            #region Construtor

            public ColunaAutorizaGridBGM() { }

            #endregion

            #region Atributos

            private string _titulo;
            private string _campo;
            private string _campoAssociado;
            private bool _chave = true;
            private bool _mostrarColuna = true;
            private int _tamanho = 100;
            private String _nomeColuna;
            private AutorizaGridBGMx2 _autorizaGridBGM;

            #endregion

            #region Propriedades

            [DefaultValue(null)]
            public string Titulo
            {
                get { return _titulo; }
                set { _titulo = value; }
            }

            [DefaultValue(null)]
            public string Campo
            {
                get { return _campo; }
                set
                {
                    if (value.RemoverCaracterEspecial() != value)
                        MessageBox.Show("Nome invalido para a coluna.", "Informação");
                    else
                    {
                        if (_campoAssociado != null &&
                            (_campoAssociado.ToUpper().IndexOf(typeof(ColunaAutorizaGridBGM).Name.ToUpper()).Equals(0) ||
                             _campoAssociado.Equals(_campo)))
                            _campoAssociado = value;
                        _campo = value;
                    }
                }
            }

            [DefaultValue(null)]
            public string CampoAssociado
            {
                get
                {
                    return _campoAssociado;
                }
                set
                {
                    if (value.RemoverCaracterEspecial() != value)
                        MessageBox.Show("Nome invalido para a coluna.", "Informação");
                    else
                        _campoAssociado = value;
                }
            }


            [DefaultValue(100)]
            public int Tamanho
            {
                get { return _tamanho; }
                set { _tamanho = value; }
            }

            [DefaultValue(true)]
            public bool Chave
            {
                get { return _chave; }
                set { _chave = value; }
            }

            [DefaultValue(true)]
            public bool MostrarColuna
            {
                get { return _mostrarColuna; }
                set { _mostrarColuna = value; }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [Browsable(false)]
            internal String NomeColuna
            {
                get
                {
                    if (ToString().IndexOf(' ') != -1)
                        _nomeColuna = RetornarNomeColuna();

                    return _nomeColuna;
                }
                set
                {
                    if (ToString().IndexOf(' ') != -1)
                        _nomeColuna = RetornarNomeColuna();
                    else
                        _nomeColuna = value;
                }
            }

            [Browsable(false)]
            internal AutorizaGridBGMx2 AutorizaGridBGM
            {
                get { return _autorizaGridBGM; }
                set { _autorizaGridBGM = value; }
            }

            #endregion

            #region Metodos

            private string RetornarNomeColuna()
            {
                return ToString().Substring(0, ToString().IndexOf(' '));
            }

            #endregion
        }

        /// <summary>
        /// Classe para controlar a edição das colunas
        /// </summary>
        private class CustomCollectionEditor : CollectionEditor
        {
            #region Construtor

            /// <summary>
            /// Construtor padrao
            /// </summary>
            public CustomCollectionEditor()
                : base(typeof(List<ColunaAutorizaGridBGM>))
            {

            }

            #endregion

            #region Atributos

            private AutorizaGridBGMx2 _autorizaGridBGM;

            #endregion

            #region Metodos

            /// <summary>
            /// DestroyInstance
            /// </summary>
            /// <param name="instance">object</param>
            protected override void DestroyInstance(object instance)
            {
                base.DestroyInstance(instance);
            }

            /// <summary>
            /// SetItems
            /// </summary>
            /// <param name="editValue">object</param>
            /// <param name="colunas">object</param>
            /// <returns>object</returns>
            protected override object SetItems(object editValue, object[] colunas)
            {
                _autorizaGridBGM.CriarColunas = true;
                return base.SetItems(editValue, colunas);
            }

            /// <summary>
            /// CreateInstance
            /// </summary>
            /// <param name="itemType">Type</param>
            /// <returns>object</returns>
            protected override object CreateInstance(Type itemType)
            {
                ColunaAutorizaGridBGM control = base.CreateInstance(itemType) as ColunaAutorizaGridBGM;
                control.Titulo = control.NomeColuna;
                control.Campo = control.NomeColuna;
                control.CampoAssociado = control.NomeColuna;
                control.MostrarColuna = true;
                control.Chave = true;
                control.AutorizaGridBGM = _autorizaGridBGM;

                return control;
            }

            /// <summary>
            /// EditValue
            /// </summary>
            /// <param name="context">ITypeDescriptorContext</param>
            /// <param name="provider">IServiceProvider</param>
            /// <param name="value">object</param>
            /// <returns>object</returns>
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                _autorizaGridBGM = context.Instance as AutorizaGridBGMx2;
                return base.EditValue(context, provider, value);
            }

            /// <summary>
            /// Altera a visialização da coluna na lista
            /// </summary>
            /// <param name="coluna">Coluna</param>
            /// <returns>string</returns>
            protected override string GetDisplayText(object coluna)
            {
                ColunaAutorizaGridBGM control = coluna as ColunaAutorizaGridBGM;
                return control.Campo.Equals(control.Titulo)
                    ? control.NomeColuna
                    : String.Concat("Campo= ", control.Campo, ", Titulo= ", control.Titulo);
            }

            #endregion
        }

        #endregion

        private ICollection _disponiveis;
        private ICollection _associados;

        public virtual void PopularDisponiveis<T>(IEnumerable<T> disponiveis)
        {
            _disponiveis = disponiveis.ToArray();
        }

        public virtual void PopularGrids<T1, T2>(IEnumerable<T1> disponiveis, IEnumerable<T2> associados)
        {
            //_disponiveis = disponiveis.ToArray();
            _associados = associados.ToArray();
            //_associados.GetType().GetProperty

            PropertyInfo[] instanceProps = _associados.GetType().GetProperties();

            var values = new Dictionary<string, object>();
            values.Add("Title", "Hello World!");
            values.Add("Text", "My first post");
            values.Add("Tags", new[] { "hello", "world" });

            var post = new DynamicEntity(values);

            dynamic dynPost = post;
            string text = dynPost.Text;
            object text1 = dynPost.Tags;
            if (text == null || text1 == null)
            {
                
            }


            //PropertyDescriptorCollection propsCollection = new PropertyDescriptorCollection(instanceProps.Cast<PropertyDescriptor>().ToArray());
            //if (propsCollection == null)
            //{                
            //}
            
            //TypeDescriptor.CreateProperty(this.GetType(), "", bool, attrs);

            //disponiveis
            //    .ToList()
            //    .ForEach(f => 
            //    {
            //        _colunas.Where(w=> w.Chave)
            //        //f.GetType()
            //        //_disponiveis
            //    });

            //IEnumerable<T1> disponiveis = disponiveis
            //    .ToList() 
            //    .RemoveAll( )

            //_disponiveis = ;
            //_associados = disponiveis.ToArray();

            //_disponiveis.
        }


        //ICollection
        //public abstract class Items<T> 
        //{ 

        //}



        //public Func<T, bool> ANDOnlyParams<T>(string[] paramNames, object[] values)
        //{
        //    List<ParameterExpression> paramList = new List<ParameterExpression>();
        //    foreach (string param in paramNames)
        //        paramList.Add(Expression.Parameter(typeof(T), param));

        //    List<LambdaExpression> lexList = new List<LambdaExpression>();
        //    for (int i = 0; i < paramNames.Length; i++)
        //    {
        //        Expression bodyInner = null;
        //        if (i == 0)
        //        {
        //            bodyInner = Expression.Equal(
        //                                Expression.Property(
        //                                    paramList[i], paramNames[i]),
        //                                    Expression.Constant(values[i]));

        //        }
        //        else
        //        {
        //            bodyInner = Expression.And(
        //                                Expression.Equal(
        //                                Expression.Property(
        //                                paramList[i], paramNames[i]),
        //                                Expression.Constant(values[i])),
        //                                Expression.Invoke(lexList[i - 1], paramList[i]));

        //        }
        //        lexList.Add(Expression.Lambda(bodyInner, paramList[i]));
        //    }

        //    return ((Expression<Func<T, bool>>)lexList[lexList.Count - 1]).Compile();
        //}

        //public virtual void PopularGrids<T1, T2>(IEnumerable<T1> disponiveis, IEnumerable<T2> associados)
        //{
        //    if (disponiveis != null && disponiveis.Count() > 0)
        //    {
        //        List<T1> listaDisponiveis = disponiveis.ToList();

        //        if (_gridControlDisponiveis == null)
        //            CriarGridControl();
        //        _gridControlDisponiveis.DataSource = listaDisponiveis.ToArray();

        //        if (_gridControlAssociados != null)
        //        {
        //            _gridControlAssociados.DataSource = disponiveis
        //                .Aggregate(new List<T1>(), (a1, b1) =>
        //                {
        //                    //ParameterExpression parameterExpressionT1 = Expression.Parameter(typeof(T1), "p");
        //                    //ParameterExpression parameterExpressionT2 = Expression.Parameter(typeof(T2), "p");

        //                    //int i = 0;
        //                    //List<LambdaExpression> listaLambdaExpression = _colunas
        //                    //    .Where(w => w.Chave)
        //                    //    .Aggregate(new List<LambdaExpression>(), (a, b) =>
        //                    //    {
        //                    //        var valor = b1.GetType().GetProperty(b.Campo).GetValue(b1, null);
        //                    //        Expression expression;

        //                    //        if (i.Equals(0))
        //                    //            expression = Expression.Equal(
        //                    //                                Expression.Property(
        //                    //                                    parameterExpressionT2, b.Campo),
        //                    //                                    Expression.Constant(valor));
        //                    //        else
        //                    //            expression = Expression.And(
        //                    //                                Expression.Equal(
        //                    //                                Expression.Property(
        //                    //                                parameterExpressionT2, b.Campo),
        //                    //                                Expression.Constant(valor)),
        //                    //                                Expression.Invoke(a[i - 1], parameterExpressionT2));

        //                    //        a.Add(Expression.Lambda(expression, parameterExpressionT2));
        //                    //        i++;
        //                    //        return a;
        //                    //    });

        //                    //var xx = ((Expression<Func<T1, bool>>)listaLambdaExpression[listaLambdaExpression.Count - 1]).Compile();

        //                    //if (disponiveis
        //                    //    .Where(w => xx(w)  ).Count() > 0)
        //                    //{

        //                    //}

        //                    Func<T1, bool> aaa = delegate(T1 item)
        //                    {
        //                        var xx = _colunas
        //                            .Where(w => w.Chave)
        //                            .Join(item.GetType().GetProperties(),
        //                                    a => a.Campo,
        //                                    b => b.Name,
        //                                    (a, b) => b.GetValue(item, null))
        //                            .Aggregate("", (a, b) => String.Concat(a, ";", b));

        //                        var xx2 = associados
        //                            .Aggregate(new List<string>(), (a, b) =>
        //                            {

        //                                return a;
        //                            });




        //                        if (xx == null)
        //                        {

        //                        }

        //                        return true;
        //                    };

        //                    if (disponiveis
        //                        .Where(w => aaa(w)).Count() > 0)
        //                    {

        //                    }

        //                    a1.Add(b1);
        //                    return a1;
        //                })
        //                .ToArray();

        //        }
        //    }
        //}

        //public virtual void PopularGridDisponivei<T>(IEnumerable<T> disponiveis)
        //{
        //    if (_gridControlDisponiveis == null)
        //        CriarGridControl();
        //    _gridControlDisponiveis.DataSource = disponiveis.ToArray();
        //}

        //public virtual IEnumerable<T> RetornarAssociados<T>()
        //{
        //    return null;
        //}

    }

    public class DynamicEntity : DynamicObject
    {
        private IDictionary<string, object> _values;

        public DynamicEntity(IDictionary<string, object> values)
        {
            _values = values;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_values.ContainsKey(binder.Name))
            {
                result = _values[binder.Name];
                return true;
            }
            result = null;
            return false;
        }
    }

}

