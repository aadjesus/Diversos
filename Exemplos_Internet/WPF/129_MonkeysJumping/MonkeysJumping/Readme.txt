Last Changed: 26 May 2006
Version: 0.1


WPF Flexible Application Readme
----------------------------------
This project enables you to run your application as either a full trust standalone application
or a sandboxed XAML Browser Application (XBAP).

Note: Because XAML Browser Applications (XBAPs) run partially trusted with internet zone permissions, 
certain features are not available to them.  This includes standalone windows and BitmapEffects.


Switching between Standalone & XBAP
-----------------------------------
To switch from standalone to XBAP choose, choose the appropriate project configuration:  

	Standalone app:
		"Debug"
		"Release" 
	XAML Browser Application (XBAP)
		"XBAP Debug"
		"XBAP Release

In your code, you can have conditional compilation:

	#if XBAP
		//  XBAP specific code
	#else
		//  Standalone specific code
	#endif

Or runtime switching:
	
	if (MyApp.IsXBAP)
	{
		//  XBAP specific code
	}
	else
	{
		//  Standalone specific code
	}

Or XAML switching:

	//Sample of visibilty toggling
	<Grid>
		<Grid.Resources>
		        <BooleanToVisibilityConverter x:Key="BoolToVis" />
		</Grid.Resources>
	<Button Visibility="{Binding Source={x:Static Application.Current}, Path=IsXBAP, Converter={StaticResource BoolToVis}}">
		FOO BAR
	</Button>


Debugging as XAML Browser Applications - (F5)
--------------------------------------------------------
Developers using Visual C# and Visual Basic Express need to save any newly created 
project.  Close the Solution.  Then open it, or F5 will not work.

If you are using the "Debug from URL" feature, you will also need to save your project,
close the solution and reopen it in order for it to work.


Release Notes
--------------
This drop of the template does NOT have publishing support. 

If you want to deploy the standalone, you can distribute the EXE.

To publish the standalone XBAP, copy the files from bin\XBAP Release or
bin\XBAP Debug to your web server.  Users should navigate to the .xbap file.
You should also add the following mime-times to your server:

	Extension		MIME Type 
	---------		---------
	.manifest 		application/manifest 
	.xaml 			application/xaml+xml 
	.application 		application/x-ms-application 
	.xbap 			application/x-ms-xbap 
	.deploy 		application/octet-stream 
