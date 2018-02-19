<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Globus5.WPF.Sistemas.Wap.WebForm1" %>

<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <mobile:Form ID="Form2" Runat="server" Title="1">
        <mobile:Label ID="Label1" Runat="server" Text="Label" />
        <mobile:Command Runat="server" ID="Command1" Text="Session" OnClick="Command1_Click" />
    </mobile:Form>
    <mobile:Form ID="Form1" Runat="server" Title="2">
        <mobile:Label ID="Label21" Runat="server" Text="Label" />
    </mobile:Form>
</body>
</html>
