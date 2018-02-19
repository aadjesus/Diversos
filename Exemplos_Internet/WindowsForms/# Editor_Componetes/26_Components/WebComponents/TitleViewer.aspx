<%@ Page language="c#" Codebehind="TitleViewer.aspx.cs" AutoEventWireup="false" Inherits="WebComponents.TitleViewer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>TitleViewer</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="pagestyles.css" type="text/css" rel="stylesheet">
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <P>Title Viewer:</P>
      <TABLE id="Table1" cellSpacing="1" cellPadding="1" width="472" border="0">
        <TR>
          <TD>Title:</TD>
          <TD>
            <asp:TextBox id="txtTitle" runat="server" CssClass="MediumTextBox"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD>Type:</TD>
          <TD>
            <asp:TextBox id="txtType" runat="server" CssClass="MediumTextBox"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD>Notes:</TD>
          <TD>
            <asp:TextBox id="txtNotes" runat="server" TextMode="MultiLine" Height="91px"
              CssClass="MediumTextBox"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD>Price:</TD>
          <TD>
            <asp:TextBox id="txtPrice" runat="server" CssClass="MediumTextBox"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD>Date:</TD>
          <TD>
            <asp:TextBox id="txtDate" runat="server" CssClass="MediumTextBox"></asp:TextBox></TD>
        </TR>
      </TABLE>
    </form>
  </body>
</HTML>
