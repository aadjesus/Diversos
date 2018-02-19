<%@ Page language="c#" Codebehind="Home.aspx.cs" AutoEventWireup="false" Inherits="WebComponents.Home" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>Mvc Components</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  </HEAD>
  <body style="FONT-SIZE: 8pt; FONT-FAMILY: Tahoma, Verdana">
    <form id="Form1" method="post" runat="server">
      <p>Publisher information:</p>
      <p>
        <table id="Table1" cellspacing="5" cellpadding="1" width="217" border="0" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; FONT-SIZE: 8pt; BORDER-LEFT: 1px solid; WIDTH: 217px; BORDER-BOTTOM: 1px solid; FONT-FAMILY: Tahoma, Verdana; HEIGHT: 191px; BACKGROUND-COLOR: silver">
          <tr>
            <td>ID:</td>
            <td><asp:textbox id="txtID" runat="server" maxlength="4"></asp:textbox></td>
          </tr>
          <tr>
            <td>Name:</td>
            <td><asp:textbox id="txtName" runat="server"></asp:textbox></td>
          </tr>
          <tr>
            <td>City</td>
            <td><asp:textbox id="txtCity" runat="server"></asp:textbox></td>
          </tr>
          <tr>
            <td>State</td>
            <td><asp:textbox id="txtState" runat="server"></asp:textbox></td>
          </tr>
          <tr>
            <td>Country</td>
            <td><asp:textbox id="txtCountry" runat="server"></asp:textbox></td>
          </tr>
          <tr>
            <td colspan="2">
              <asp:button id="btnLoad" runat="server" text="Load"></asp:button>&nbsp;
              <asp:button id="btnSave" runat="server" text="Save"></asp:button>&nbsp;
              <asp:button id="btnDelete" runat="server" text="Delete"></asp:button></td>
          </tr>
        </table>
      </p>
    </form>
  </body>
</HTML>
