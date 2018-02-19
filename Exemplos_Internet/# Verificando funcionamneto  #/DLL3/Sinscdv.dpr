program Sinscdv;

uses
  Forms,
  Finscdv in 'Finscdv.pas' {frmInscDv};

{$R *.RES}

begin
  Application.Initialize;
  Application.CreateForm(TfrmInscDv, frmInscDv);
  Application.Run;
end.
