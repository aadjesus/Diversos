using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FGlobus.Util;
using FGlobus.Componentes.WinForms;
using System.Windows.Forms.Design;
using DevExpress.XtraEditors.Mask;

namespace FGlobus.Componentes.WinForms.ControleDeComponente
{
    [ToolboxItem(false)]
    public partial class TipoMascaras : UserControl
    {
        #region Constructor

        public TipoMascaras(Control _controle, IWindowsFormsEditorService _iWindowsFormsEditorService)
        {
            InitializeComponent();
            _iWindowsFormsEditorService_TipoMascaras = _iWindowsFormsEditorService;

            FiltroRelatorioBGM filtroRelatorioBGM = _controle as FiltroRelatorioBGM;
            if (filtroRelatorioBGM != null)
                _mascaraSelecionada = filtroRelatorioBGM.Tipo.Equals(FiltroRelatorioBGM.eTipo.AlfaNumerico)
                    ? filtroRelatorioBGM.btEdtBGMInicial.TipoMascara
                    : filtroRelatorioBGM.Tipo.Equals(FiltroRelatorioBGM.eTipo.Data)
                        ? filtroRelatorioBGM.dtEdtBGMInicial.TipoMascara
                        : filtroRelatorioBGM.clcEdtBGMInicial.TipoMascara;
            else
                _mascaraSelecionada = (_controle is CalcEditBGM)
                    ? ((CalcEditBGM)_controle).TipoMascara
                    : (_controle is DateEditBGM)
                        ? ((DateEditBGM)_controle).TipoMascara
                        : ((TextEditBGM)_controle).TipoMascara;

            PopularMascaras();

            this.grdCtrlTipoMascaras.DataSource = _listaClassMascaras
                .Where(w => w.Controles.Contains((_controle is CalcEditBGM || (filtroRelatorioBGM != null && filtroRelatorioBGM.Tipo.Equals(FiltroRelatorioBGM.eTipo.Numerico)))
                                                    ? ClassMascaras.TiposControles.CalcEdit
                                                    : (_controle is DateEditBGM || (filtroRelatorioBGM != null && filtroRelatorioBGM.Tipo.Equals(FiltroRelatorioBGM.eTipo.Data)))
                                                        ? ClassMascaras.TiposControles.DateEdit
                                                        : (_controle is ButtonEditBGM || (filtroRelatorioBGM != null && filtroRelatorioBGM.Tipo.Equals(FiltroRelatorioBGM.eTipo.AlfaNumerico)))
                                                            ? ClassMascaras.TiposControles.ButtonEdit
                                                            : ClassMascaras.TiposControles.TextEdit))
                .ToArray();

            this.grdVwTipoMascaras.FocusedRowHandle =
                this.grdVwTipoMascaras.LocateByValue(0, this.grdClmnTipo, _mascaraSelecionada.DescricaoTipo);
        }

        #endregion

        #region Atributtes

        private ClassMascaras _mascaraSelecionada = new ClassMascaras();
        private IWindowsFormsEditorService _iWindowsFormsEditorService_TipoMascaras;
        private List<ClassMascaras> _listaClassMascaras = new List<ClassMascaras>();

        #endregion

        #region Propriedades

        public ClassMascaras MascaraSelecionada
        {
            get
            {
                return _mascaraSelecionada;
            }
            set
            {
                _mascaraSelecionada = value;
            }
        }

        #endregion

        #region Methods

        void PopularMascaras()
        {
            _listaClassMascaras.Clear();


            ClassMascaras _default = new ClassMascaras();
            _default.SetCodigo(1);
            _default.SetTipo("Default");
            _default.SetDescricaoTipo("Default");
            _default.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.CalcEdit,
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit,
            });
            _listaClassMascaras.Add(_default);


            ClassMascaras _valor = new ClassMascaras();
            _valor.SetCodigo(2);
            _valor.SetTipo("Valor");
            _valor.SetDescricaoTipo("Valor");
            _valor.SetExemplo("R$ 999.999,99");
            _valor.SetExemplo("R$ 999.999,99");
            _valor.SetEditMask("c");
            _valor.SetMaskType(MaskType.Numeric);
            _valor.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.CalcEdit
            });
            _listaClassMascaras.Add(_valor);


            ClassMascaras _percentual = new ClassMascaras();
            _percentual.SetCodigo(3);
            _percentual.SetTipo("Percentual");
            _percentual.SetDescricaoTipo("Percentual");
            _percentual.SetExemplo("9,99 %");
            _percentual.SetEditMask("P");
            _percentual.SetMaskType(MaskType.Numeric);
            _percentual.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.CalcEdit
            });
            _listaClassMascaras.Add(_percentual);


            ClassMascaras _diaMesEAnoDoisDigitos = new ClassMascaras();
            _diaMesEAnoDoisDigitos.SetCodigo(4);
            _diaMesEAnoDoisDigitos.SetTipo("DiaMesEAnoDoisDigitos");
            _diaMesEAnoDoisDigitos.SetDescricaoTipo("Dia, mês e ano (2 digítos)");
            _diaMesEAnoDoisDigitos.SetExemplo("01/09/10");
            _diaMesEAnoDoisDigitos.SetEditMask("99/99/00");
            _diaMesEAnoDoisDigitos.SetMaskType(MaskType.Simple);
            _diaMesEAnoDoisDigitos.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_diaMesEAnoDoisDigitos);


            ClassMascaras _diaMesEAnoQuatroDigitos = new ClassMascaras();
            _diaMesEAnoQuatroDigitos.SetCodigo(5);
            _diaMesEAnoQuatroDigitos.SetTipo("DiaMesEAnoQuatroDigitos");
            _diaMesEAnoQuatroDigitos.SetDescricaoTipo("Dia, mês e ano (4 digítos)");
            _diaMesEAnoQuatroDigitos.SetExemplo("01/09/1910");
            _diaMesEAnoQuatroDigitos.SetEditMask("99/99/0000");
            _diaMesEAnoQuatroDigitos.SetMaskType(MaskType.Simple);
            _diaMesEAnoQuatroDigitos.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_diaMesEAnoQuatroDigitos);


            ClassMascaras _horaSemSegundos = new ClassMascaras();
            _horaSemSegundos.SetCodigo(6);
            _horaSemSegundos.SetTipo("HoraSemSegundos");
            _horaSemSegundos.SetDescricaoTipo("Horas sem segundos");
            _horaSemSegundos.SetExemplo("02:17");
            _horaSemSegundos.SetEditMask("90:00");
            _horaSemSegundos.SetMaskType(MaskType.Simple);
            _horaSemSegundos.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_horaSemSegundos);


            ClassMascaras _horaComSegundos = new ClassMascaras();
            _horaComSegundos.SetCodigo(7);
            _horaComSegundos.SetTipo("HoraComSegundos");
            _horaComSegundos.SetDescricaoTipo("Horas com segundos");
            _horaComSegundos.SetExemplo("02:17:20");
            _horaComSegundos.SetEditMask("90:00:00");
            _horaComSegundos.SetMaskType(MaskType.Simple);
            _horaComSegundos.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_horaComSegundos);


            ClassMascaras _diaMesAnoDoisDigitosHorasSemSegundos = new ClassMascaras();
            _diaMesAnoDoisDigitosHorasSemSegundos.SetCodigo(8);
            _diaMesAnoDoisDigitosHorasSemSegundos.SetTipo("DiaMesAnoDoisDigitosHorasSemSegundos");
            _diaMesAnoDoisDigitosHorasSemSegundos.SetDescricaoTipo("Dia, mês e ano (2 digítos) / Horas sem segundos");
            _diaMesAnoDoisDigitosHorasSemSegundos.SetExemplo("01/09/10 02:17");
            _diaMesAnoDoisDigitosHorasSemSegundos.SetEditMask("99/99/00 90:00");
            _diaMesAnoDoisDigitosHorasSemSegundos.SetMaskType(MaskType.Simple);
            _diaMesAnoDoisDigitosHorasSemSegundos.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_diaMesAnoDoisDigitosHorasSemSegundos);


            ClassMascaras _diaMesAnoDoisDigitosHorasComSegundos = new ClassMascaras();
            _diaMesAnoDoisDigitosHorasComSegundos.SetCodigo(9);
            _diaMesAnoDoisDigitosHorasComSegundos.SetTipo("DiaMesAnoDoisDigitosHorasComSegundos");
            _diaMesAnoDoisDigitosHorasComSegundos.SetDescricaoTipo("Dia, mês e ano (2 digítos) / Horas com segundos");
            _diaMesAnoDoisDigitosHorasComSegundos.SetExemplo("01/09/10 02:17:20");
            _diaMesAnoDoisDigitosHorasComSegundos.SetEditMask("99/99/00 90:00:00");
            _diaMesAnoDoisDigitosHorasComSegundos.SetMaskType(MaskType.Simple);
            _diaMesAnoDoisDigitosHorasComSegundos.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_diaMesAnoDoisDigitosHorasComSegundos);


            ClassMascaras _diaMesAnoQuatroDigitosHorasSemSegundos = new ClassMascaras();
            _diaMesAnoQuatroDigitosHorasSemSegundos.SetCodigo(10);
            _diaMesAnoQuatroDigitosHorasSemSegundos.SetTipo("DiaMesAnoQuatroDigitosHorasSemSegundos");
            _diaMesAnoQuatroDigitosHorasSemSegundos.SetDescricaoTipo("Dia, mês e ano (4 digítos) / Horas sem segundos");
            _diaMesAnoQuatroDigitosHorasSemSegundos.SetExemplo("01/09/1910 02:17");
            _diaMesAnoQuatroDigitosHorasSemSegundos.SetEditMask("99/99/0000 90:00");
            _diaMesAnoQuatroDigitosHorasSemSegundos.SetMaskType(MaskType.Simple);
            _diaMesAnoQuatroDigitosHorasSemSegundos.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_diaMesAnoQuatroDigitosHorasSemSegundos);


            ClassMascaras _diaMesAnoQuatroDigitosHorasComSegundos = new ClassMascaras();
            _diaMesAnoQuatroDigitosHorasComSegundos.SetCodigo(11);
            _diaMesAnoQuatroDigitosHorasComSegundos.SetTipo("DiaMesAnoQuatroDigitosHorasComSegundos");
            _diaMesAnoQuatroDigitosHorasComSegundos.SetDescricaoTipo("Dia, mês e ano (4 digítos) / Horas com segundos");
            _diaMesAnoQuatroDigitosHorasComSegundos.SetExemplo("01/09/1910 02:17:20");
            _diaMesAnoQuatroDigitosHorasComSegundos.SetEditMask("99/99/0000 90:00:00");
            _diaMesAnoQuatroDigitosHorasComSegundos.SetMaskType(MaskType.Simple);
            _diaMesAnoQuatroDigitosHorasComSegundos.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_diaMesAnoQuatroDigitosHorasComSegundos);


            ClassMascaras _diaEMesNumero = new ClassMascaras();
            _diaEMesNumero.SetCodigo(12);
            _diaEMesNumero.SetTipo("DiaEMesNumero");
            _diaEMesNumero.SetDescricaoTipo("Dia e mês (número)");
            _diaEMesNumero.SetExemplo("01/09");
            _diaEMesNumero.SetEditMask("99/99");
            _diaEMesNumero.SetMaskType(MaskType.Simple);
            _diaEMesNumero.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_diaEMesNumero);


            ClassMascaras _diaEMesExtenso = new ClassMascaras();
            _diaEMesExtenso.SetCodigo(13);
            _diaEMesExtenso.SetTipo("DiaEMesExtenso");
            _diaEMesExtenso.SetDescricaoTipo("Dia e mês (extenso)");
            _diaEMesExtenso.SetExemplo("01 de Setembro");
            _diaEMesExtenso.SetEditMask("m");
            _diaEMesExtenso.SetMaskType(MaskType.DateTime);
            _diaEMesExtenso.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_diaEMesExtenso);


            ClassMascaras _diaEDiaDaSemana = new ClassMascaras();
            _diaEDiaDaSemana.SetCodigo(14);
            _diaEDiaDaSemana.SetTipo("DiaEDiaDaSemana");
            _diaEDiaDaSemana.SetDescricaoTipo("Dia e dia da semana");
            _diaEDiaDaSemana.SetExemplo("01, quinta-feira");
            _diaEDiaDaSemana.SetEditMask("d, dddd");
            _diaEDiaDaSemana.SetMaskType(MaskType.DateTime);
            _diaEDiaDaSemana.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_diaEDiaDaSemana);


            ClassMascaras _placaVeiculo = new ClassMascaras();
            _placaVeiculo.SetCodigo(15);
            _placaVeiculo.SetTipo("PlacaVeiculo");
            _placaVeiculo.SetDescricaoTipo("Placa do veículo");
            _placaVeiculo.SetExemplo("AAA-9999");
            _placaVeiculo.SetEditMask("\\w{3}-\\d\\d\\d\\d");
            _placaVeiculo.SetMaskType(MaskType.Regular);
            _placaVeiculo.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_placaVeiculo);


            ClassMascaras _CPF = new ClassMascaras();
            _CPF.SetCodigo(16);
            _CPF.SetTipo("CPF");
            _CPF.SetDescricaoTipo("CPF");
            _CPF.SetExemplo("999.999.999-99");
            _CPF.SetEditMask("000.000.000-00");
            _CPF.SetMaskType(MaskType.Simple);
            _CPF.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_CPF);


            ClassMascaras _RG = new ClassMascaras();
            _RG.SetCodigo(17);
            _RG.SetTipo("RG");
            _RG.SetDescricaoTipo("RG");
            _RG.SetExemplo("99.999.999-9");
            _RG.SetEditMask("00.000.000-a");
            _RG.SetMaskType(MaskType.Simple);
            _RG.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_RG);


            ClassMascaras _CNPJ = new ClassMascaras();
            _CNPJ.SetCodigo(18);
            _CNPJ.SetTipo("CNPJ");
            _CNPJ.SetDescricaoTipo("CNPJ");
            _CNPJ.SetExemplo("99.999.999/9999-99");
            _CNPJ.SetEditMask("00.000.000/0000-00");
            _CNPJ.SetMaskType(MaskType.Simple);
            _CNPJ.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_CNPJ);


            ClassMascaras _CEP = new ClassMascaras();
            _CEP.SetCodigo(19);
            _CEP.SetTipo("CEP");
            _CEP.SetDescricaoTipo("CEP");
            _CEP.SetExemplo("99999-999");
            _CEP.SetEditMask("00000-000");
            _CEP.SetMaskType(MaskType.Simple);
            _CEP.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_CEP);


            ClassMascaras _telefoneSemDDD = new ClassMascaras();
            _telefoneSemDDD.SetCodigo(20);
            _telefoneSemDDD.SetTipo("TelefoneSemDDD");
            _telefoneSemDDD.SetDescricaoTipo("Telefone sem DDD");
            _telefoneSemDDD.SetExemplo("9999-9999");
            _telefoneSemDDD.SetEditMask("0000-0000");
            _telefoneSemDDD.SetMaskType(MaskType.Simple);
            _telefoneSemDDD.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_telefoneSemDDD);


            ClassMascaras _telefoneDoisDigitosDDD = new ClassMascaras();
            _telefoneDoisDigitosDDD.SetCodigo(21);
            _telefoneDoisDigitosDDD.SetTipo("TelefoneDoisDigitosDDD");
            _telefoneDoisDigitosDDD.SetDescricaoTipo("Telefone com 2 dígitos no DDD");
            _telefoneDoisDigitosDDD.SetExemplo("(99) 9999-9999");
            _telefoneDoisDigitosDDD.SetEditMask("(99) 0000-0000");
            _telefoneDoisDigitosDDD.SetMaskType(MaskType.Simple);
            _telefoneDoisDigitosDDD.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_telefoneDoisDigitosDDD);


            ClassMascaras _telefoneTresDigitosDDD = new ClassMascaras();
            _telefoneTresDigitosDDD.SetCodigo(22);
            _telefoneTresDigitosDDD.SetTipo("TelefoneTresDigitosDDD");
            _telefoneTresDigitosDDD.SetDescricaoTipo("Telefone com 3 dígitos no DDD");
            _telefoneTresDigitosDDD.SetExemplo("(999) 9999-9999");
            _telefoneTresDigitosDDD.SetEditMask("(999) 0000-0000");
            _telefoneTresDigitosDDD.SetMaskType(MaskType.Simple);
            _telefoneTresDigitosDDD.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_telefoneTresDigitosDDD);


            ClassMascaras _email = new ClassMascaras();
            _email.SetCodigo(23);
            _email.SetTipo("Email");
            _email.SetDescricaoTipo("E-mail");
            _email.SetExemplo("desenvolvimento@bgmrodotec.com.br");
            _email.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_email);


            ClassMascaras _senha = new ClassMascaras();
            _senha.SetCodigo(24);
            _senha.SetTipo("Senha");
            _senha.SetDescricaoTipo("Senha");
            _senha.SetExemplo("**********");
            _senha.SetPasswordChar('*');
            _senha.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_senha);


            ClassMascaras _numero = new ClassMascaras();
            _numero.SetCodigo(25);
            _numero.SetTipo("Numero");
            _numero.SetDescricaoTipo("Número");
            _numero.SetExemplo("12345");
            _numero.SetEditMask("\\d{0,5}");
            _numero.SetMaskType(MaskType.RegEx);
            _numero.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_numero);

            ClassMascaras _classificador = new ClassMascaras();
            _classificador.SetCodigo(26);
            _classificador.SetTipo("Classificador");
            _classificador.SetDescricaoTipo("Classificador");
            _classificador.SetExemplo("1.01.01.000");
            _classificador.SetMaskType(MaskType.RegEx);
            _classificador.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.TextEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_classificador);

            ClassMascaras _meseAnoComQuatroDigitos = new ClassMascaras();
            _meseAnoComQuatroDigitos.SetCodigo(27);
            _meseAnoComQuatroDigitos.SetTipo("MeseAnoComQuatroDigitos");
            _meseAnoComQuatroDigitos.SetDescricaoTipo("Mês e ano (4 digítos)");
            _meseAnoComQuatroDigitos.SetExemplo("09/1910");
            _meseAnoComQuatroDigitos.SetEditMask("MM/yyyy");
            _meseAnoComQuatroDigitos.SetMaskType(MaskType.DateTime);
            _meseAnoComQuatroDigitos.SetListaControles(new ClassMascaras.TiposControles[]
            {
                ClassMascaras.TiposControles.DateEdit,
                ClassMascaras.TiposControles.ButtonEdit
            });
            _listaClassMascaras.Add(_meseAnoComQuatroDigitos);

        }


        private void grdVwTipoMascaras_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            _mascaraSelecionada =
                this.grdVwTipoMascaras.GetFocusedRow() as ClassMascaras;
        }

        private void grdVwTipoMascaras_DoubleClick(object sender, EventArgs e)
        {
            _mascaraSelecionada =
                this.grdVwTipoMascaras.GetFocusedRow() as ClassMascaras;

            _iWindowsFormsEditorService_TipoMascaras.CloseDropDown();
        }

        #endregion
    }
}
