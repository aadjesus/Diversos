<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="Globus5.WPF.Sistemas.Wap._Default" %>

<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <mobile:form id="Form1" runat="server">
        <mobile:TextBox Runat="server" ID="TextBox1" />
        <mobile:Command Runat="server" ID="Command1" Text="Session" OnClick="Command1_Click" />
        <mobile:Command Runat="server" ID="Command2" Text="QueryString" OnClick="Command2_Click" />
    </mobile:form>
</body>
</html>
