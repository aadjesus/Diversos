unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ScktComp, ExtCtrls;

type
  TForm1 = class(TForm)
    ClientSocket1: TClientSocket;
    Label1: TLabel;
    BtnConectar: TButton;
    BtnEnviar: TButton;
    EdtHost: TEdit;
    Label2: TLabel;
    EdtPorta: TEdit;
    Label3: TLabel;
    LblConexao: TLabel;
    RgOpcao: TRadioGroup;
    EdtRastreador: TEdit;
    Label4: TLabel;
    EdtTel: TEdit;
    LblTextoEnvio: TLabel;
    BtnDesconectar: TButton;
    procedure BtnConectarClick(Sender: TObject);
    procedure BtnEnviarClick(Sender: TObject);
    procedure ClientSocket1Connecting(Sender: TObject;
      Socket: TCustomWinSocket);
    procedure ClientSocket1Connect(Sender: TObject;
      Socket: TCustomWinSocket);
    procedure ClientSocket1Disconnect(Sender: TObject;
      Socket: TCustomWinSocket);
    procedure ClientSocket1Error(Sender: TObject; Socket: TCustomWinSocket;
      ErrorEvent: TErrorEvent; var ErrorCode: Integer);
    procedure BtnDesconectarClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.BtnConectarClick(Sender: TObject);
begin
   if ClientSocket1.Active = false then
   begin
      ClientSocket1.Host := EdtHost.Text;
      ClientSocket1.Port := StrtoInt(Trim(EdtPorta.Text));
      ClientSocket1.Active := true;
   end
   else
   begin
     ClientSocket1.Active := false;
   end;
end;

procedure TForm1.BtnEnviarClick(Sender: TObject);
var opcao:String;
begin
   if RgOpcao.ItemIndex = 0 then
     opcao := 'escuta2;'
   else
     opcao := 'escuta3;';

   if ClientSocket1.Active then
   begin
     LblTextoEnvio.Caption :=  trim(EdtRastreador.Text)+';'+opcao+trim(EdtTel.Text);
     Application.ProcessMessages;
     ClientSocket1.Socket.SendText(LblTextoEnvio.Caption);
   end
   else
     ShowMessage('Socket desconectado');
// '020084921,escuta1,1381515294
end;

procedure TForm1.ClientSocket1Connecting(Sender: TObject;
  Socket: TCustomWinSocket);
begin
  LblConexao.Caption := '    Conectando IP: '+ClientSocket1.Host+' Porta: '+InttoStr(ClientSocket1.Port)+' ... Aguarde';
  Application.ProcessMessages;
end;

procedure TForm1.ClientSocket1Connect(Sender: TObject;
  Socket: TCustomWinSocket);
begin
  LblConexao.Caption  := '    Conexão estabelecida. IP: '+ClientSocket1.Host+' Porta: '+InttoStr(ClientSocket1.Port);
  Application.ProcessMessages;
  BtnConectar.enabled := false;
  BtnDesconectar.enabled := true;
  BtnEnviar.enabled   := true;
end;

procedure TForm1.ClientSocket1Disconnect(Sender: TObject;
  Socket: TCustomWinSocket);
begin
  LblConexao.Caption  := '    Disconectado';
  BtnConectar.enabled := true;
  BtnDesconectar.enabled := false;
  BtnEnviar.enabled   := false;
end;

procedure TForm1.ClientSocket1Error(Sender: TObject;
  Socket: TCustomWinSocket; ErrorEvent: TErrorEvent;
  var ErrorCode: Integer);
begin
  If ErrorCode = 10060 then
    ShowMessage('Connection timed out')
  else
    ShowMessage('outro erro');
end;

procedure TForm1.BtnDesconectarClick(Sender: TObject);
begin
  ClientSocket1.Active := false;
end;

end.
