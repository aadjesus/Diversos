:: use /Gec option for compatibility mode (required by some .fx files)
echo ******* Compiling Pixel Shaders *******
"%DXSDK_DIR%\Utilities\bin\x86\fxc" /T ps_2_0 /E main /Fo "%1\MyEffect1\MyEffect1.ps" "%1\MyEffect1\MyEffect1.fx"
echo ******* Compiling Pixel Shaders Completed *******
