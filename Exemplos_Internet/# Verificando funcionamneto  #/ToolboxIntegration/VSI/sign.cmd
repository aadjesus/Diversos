REM convert ZIP to EXE
REM you can obtain MakeZipExe from Visual Studio binary folder (e.g. "c:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\")

MakeZipExe.exe -zipfile:SampleControl.vsi -output:SampleControl-unsigned.vsi -overwrite


REM copy file to avoid overwrite

copy SampleControl-unsigned.vsi SampleControl-signed.vsi


REM sign file
REM you can obtail signtool.exe from Windows SDK binary folder (e.g. "c:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\")

signtool.exe sign /du "http://www.componentowl.com/sample-control" /f certificate.pfx /p abc123 /t "http://timestamp.comodoca.com/authenticode" "SampleControl-signed.vsi"