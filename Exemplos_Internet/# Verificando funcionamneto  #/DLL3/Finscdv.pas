unit Finscdv;

interface

uses
  SysUtils, WinTypes, WinProcs, Messages, Classes, Graphics, Controls,
  Forms, Dialogs, Buttons, StdCtrls, ExtCtrls, Gauges, DB, DBTables;

type
  TConsisteInscricaoEstadual  = function (const Insc, UF: String): Integer; stdcall;

type
  TfrmInscDv = class(TForm)
    Panel1: TPanel;
    btnCalcula: TButton;
    btnFechar: TBitBtn;
    pnlEdit: TPanel;
    Label2: TLabel;
    edtUF: TEdit;
    edtInscricao: TEdit;
    Label1: TLabel;
    Button1: TButton;
    dtsInscr: TDataSource;
    tblInscr: TTable;
    pnlInscr: TPanel;
    gauInscr: TGauge;
    lblInscr: TLabel;
    procedure btnCalculaClick(Sender: TObject);
    procedure btnFecharClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmInscDv: TfrmInscDv;

implementation

{//function  ConsisteInscricaoEstadual (const Insc, UF: String): Integer; far;
//          external 'DllInscE32' index 1;}

{$R *.DFM}

procedure TfrmInscDv.btnCalculaClick(Sender: TObject);
var
  IRet, IOk, IErro, IPar    : Integer;

  LibHandle                 : THandle;
  ConsisteInscricaoEstadual : TConsisteInscricaoEstadual;
begin

  try
    LibHandle :=  LoadLibrary (PChar (Trim ('DllInscE32.Dll')));
    if  LibHandle <=  HINSTANCE_ERROR then
      raise Exception.Create ('Dll não carregada');

    @ConsisteInscricaoEstadual  :=  GetProcAddress (LibHandle,
                                                    'ConsisteInscricaoEstadual');
    if  @ConsisteInscricaoEstadual  = nil then
      raise Exception.Create('Entrypoint Download não encontrado na Dll');



    IRet := ConsisteInscricaoEstadual (edtInscricao.Text,edtUF.Text);
    if      Iret = 0 then
       MessageDlg ('Inscrição válida para '+edtUf.Text,mtInformation,[mbOk],0)
    else if Iret = 1 then
       MessageDlg ('Inscrição inválida para '+edtUf.Text,mtError,[mbOk],0)
    else
       MessageDlg ('Parâmetros inválidos',mtError,[mbOk],0);
    edtInscricao.SetFocus;


  finally
    FreeLibrary (LibHandle);
  end;

end;

procedure TfrmInscDv.btnFecharClick(Sender: TObject);
begin
  Close;
end;

procedure TfrmInscDv.Button1Click(Sender: TObject);
var
  IRet, IOk, IErro, IPar    : Integer;

  LibHandle                 : THandle;
  ConsisteInscricaoEstadual : TConsisteInscricaoEstadual;
begin
  try
    LibHandle :=  LoadLibrary (PChar (Trim ('DllInscE32.Dll')));
    if  LibHandle <=  HINSTANCE_ERROR then
      raise Exception.Create ('Dll não carregada');

    @ConsisteInscricaoEstadual  :=  GetProcAddress (LibHandle,
                                                    'ConsisteInscricaoEstadual');
    if  @ConsisteInscricaoEstadual  = nil then
      raise Exception.Create('Entrypoint Download não encontrado na Dll');

    { Abre arquivo com inscrições }
    tblInscr.Active   :=  True;
    gauInscr.MaxValue :=  tblInscr.RecordCount;
    gauInscr.Progress :=  0;
    { Troca o painel ativo }
    pnlEdit.SendToBack;
    pnlInscr.BringToFront;
    { Varre todo o arquivo }
    IOk   :=  0;
    IErro :=  0;
    IPar  :=  0;
    with  tblInscr  do
    begin
      First;
      while not EOF do
      begin
        lblInscr.Caption  :=  'Inscrição : '  +
                              FieldByName('ESTADO').AsString  + ' - ' +
                              FieldByName ('INSCRICAO').AsString  +
                              '             ';
        lblInscr.Refresh;
        Edit;
        try
          IRet := ConsisteInscricaoEstadual  (FieldByName ('INSCRICAO').AsString,
                                              FieldByName ('ESTADO').AsString);
          if      IRet = 0 then
          begin
            FieldByName ('OK').AsBoolean  :=  True;
            Inc (IOk);
          end
          else if IRet = 1 then
          begin
            FieldByName ('OK').AsBoolean  :=  False;
            Inc (IErro);
          end
          else
          begin
            FieldByName ('OK').AsBoolean  :=  False;
            Inc (IPar);
          end;
        except
          ShowMessage ('Erro no registro : '  + lblInscr.Caption);
        end;
        { Avança }
        gauInscr.Progress :=  gauInscr.Progress + 1;
        Next;
      end;
      { Fim }
      ShowMessage ('Corretas   : '  + IntToStr (IOk)    + #13 +
                   'Incorretas : '  + IntToStr (IErro)  + #13 +
                   'Parametros : '  + IntToStr (IPar)   + #13 + #13 +
                   'Total      : '  + IntToStr (IOk + IErro + IPar));
      { Retorna o painel }
      pnlInscr.SendToBack;
      pnlEdit.BringToFront;
    end;


  finally
    FreeLibrary (LibHandle);
  end;


end;

procedure TfrmInscDv.FormCreate(Sender: TObject);
begin
  tblInscr.TableName  :=  ExtractFilePath(Application.ExeName)  + 'Inscr.DBF';
end;

end.
