<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Research.MVP.WebApplicationExample.Default" %>

<%@ Register Src="~/ContentContainer.ascx" TagPrefix="res" TagName="ContentContainer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EaseYourMVP Example</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 10px; border: solid 1px #000000;">
        <asp:Label ID="Label1" runat="server" Text="Page"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Choose a datetime format: "></asp:Label>
        <asp:DropDownList ID="dropDownListDateTimeFormats" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropDownListDateTimeFormats_SelectedIndexChanged">
        </asp:DropDownList>
        <div>
            <res:ContentContainer ID="ContentContainer1" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
