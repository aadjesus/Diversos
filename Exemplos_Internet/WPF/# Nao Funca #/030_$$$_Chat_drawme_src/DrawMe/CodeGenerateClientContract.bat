SET OUTDIR=bin\DEBUG
SET DLLDIR="..\..\..\..\DrawMeInterfaces\bin\Debug"

cd %OUTDIR%

SET SVCUTIL="C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\svcutil"
mkdir temp
cd temp
%SVCUTIL% %DLLDIR%\DrawMeInterfaces.dll
%SVCUTIL% *.wsdl *.xsd /language:C# /out:TempDrawMeGateway.cs /noConfig

cd ..\..\..
@REM echo namespace DrawMeServerGateway { > DrawMeGatewayTemp.cs
sleep 1
type %OUTDIR%\temp\TempDrawMeGateway.cs >> DrawMeGatewayTemp.cs
sleep 1
@REM echo } >> DrawMeGatewayTemp.cs
sleep 1
del /F /S /Q %OUTDIR%\temp\*.*
rmdir /S /Q %OUTDIR%\temp

pause