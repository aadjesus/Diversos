<%@ Register TagPrefix="ui" Namespace="UIControl" Assembly="SimpleLocationControl" %>
<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="TypeConverterSample.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TypeConverter sample</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Label id="TheLabel" runat="server" Font-Bold="True" Font-Italic="True">GPS Location </asp:Label>
			<ui:LocationControl id="LocationControl1" runat="server">
				<Location GPSLatitude="11N1'2&quot;" GPSLongitude="24W3'4&quot;"></Location>
			</ui:LocationControl>
		</form>
	</body>
</HTML>
