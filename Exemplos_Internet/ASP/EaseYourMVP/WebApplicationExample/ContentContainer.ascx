<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentContainer.ascx.cs" Inherits="Research.MVP.WebApplicationExample.ContentContainer" %>

<%@ Register Src="~/EmployeesList.ascx" TagPrefix="res" TagName="EmployeesList" %>

<div style="margin:10px; border:solid 1px #000000;">
    <asp:Label ID="Label1" runat="server" Text="Content"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Content was last requested on: "></asp:Label>
    <asp:Label ID="labelLastUpdated" runat="server" Text=""></asp:Label>
    <res:EmployeesList ID="EmployeesList1" runat="server" />
</div>
