<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeesList.ascx.cs" Inherits="Research.MVP.WebApplicationExample.EmployeesList" %>

<div style="margin:10px; border:solid 1px #000000;">
    <asp:Label ID="Label1" runat="server" Text="Employee list"></asp:Label>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="BirthDate" HeaderText="Birth Date" HtmlEncode="false" />
            <asp:BoundField DataField="HiredDate" HeaderText="Hired Date" HtmlEncode="false" />
        </Columns>
    </asp:GridView>
</div>
