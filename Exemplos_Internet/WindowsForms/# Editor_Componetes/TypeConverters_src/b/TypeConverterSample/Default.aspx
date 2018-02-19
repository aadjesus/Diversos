<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="SimpleLocationControl" Namespace="UIControl" TagPrefix="ui" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TypeConvertert sample</title>
</head>
<body>
    <form id="MainForm" runat="server">
        <br />
        <span><strong><em>GPS Location:</em></strong></span>
        <ui:LocationControl ID="LocationControl1" runat="server">
            <Location GPSLatitude="12N1'2&quot;" GPSLongitude="24W3'4&quot;" />
        </ui:LocationControl>
    </form>
</body>
</html>
