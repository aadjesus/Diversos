BarcodeTestApp.exe:
BarcodeTestApp uses BarcodeImaging.dll (c#) and BarcodeScanner.dll directly. Assemblies 
must be located in the same directory as the application or in the GAC.


COMTestApp.exe, COMTest.doc:
COMTestApp and COMTest use the COM interface exposed by BarcodeScanner.dll. To make this
work, please use RegisterForCOM to register the COM interface. To make the VBA code in
the Word document work, you need to enable macros in Word.