Drag and Drop Archiver Gadget ReadMe
====================================

1. What does this gadget do?
============================
This gadget allows you to drag files and folder on its sidebar space and then drag the files out of it compressed. It does this by using the very archiver you use. It has been tested with WinAce, WinZip, WinRar and it should work with any other archiver that offers a command line utility (console application).

2. How do I install it?
========================
Download the dome project zip. The extract it and run teh included Setup.msi file. You will need administrator privileges to correctly install the required libraries. Then you can run teh other file called Zipper.gadget which will install that product.

3. How do I use it?
===================
a) Setting up the profiles.
The gadget offers 3 profiles for compressing files.Each profile can use adifferent archiving utility or the same utility with different parameters. To set up your own archiver, check out the producers site to find out if it comes with a cosole application. If it does then you ahve to enter the path to this application in the Gadget settings. Then check out the documentation of that application to find suitable arguments for calling it and enter then in the Arguments textbox of the profile youentered the name of the application in.

The gadget has so far been tested with WinRar, WinZip and WinAce.

- I use WinRar
--------------
If you have installed in the default location, then no setting editing is need. Otherwise, enter this info in the Settings dialog of the gadget:

PATH : instalation path (ex: C:\Program Files\WinRAR\rar.exe )
ARGS :  a -ep1
EXTENSION : rar

Note: Check out the console application help to tune your compression level. For compatibility , the compression is left to the defalt level.

-I use WinAce
-------------
For winAce the, the console application is a separate download. Extract the files in the package obtained from http://www.winace.com/ to c:\Downloads so that no setting are required. Otherwise, do this:

Note: Because you run on Windows Vista, the only application from those extracted that  will work for you is ace32.exe. 

PATH : path to ace32.exe
ARGS : a -r
EXTENSION : ace

-I use WInZip
--------------
To use WinZip you have to use the Pro version (which costs money ). Install it to the default path and you will not be required to edit any settings, but you will have to install the free add-on that provides console functionality from http://www.winzip.com/prodpagecl.htm

PATH : installation path of wzzip.exe
ARGS : -rp
EXTENSION : zip

Note: Because you run on Windows Vista, the help of the console application for winzip will not work. Use this link if you want to further twak your parameters: 
http://www.bnsftransload.com/markets/agricultural/bnsf4022/csv_db/help/CommandlineWinZip.pdf

-I use another archiver
-----------------------
I recommend you to use 7Zip commadn line utility. http://www.7-zip.org/download.html it is small and works well.

4. Known limitations
=====================
Depending on your archiver , filenames with spaces in tehm might not be supported.


