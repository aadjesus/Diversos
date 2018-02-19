<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PerformanceNHibernate1.Index" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <div>



            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                CellPadding="3"
                GridLines="Vertical" >

                <Columns>
                    <asp:BoundField DataField="CODCLIENTE" HeaderText="CODCLIENTE" />
                </Columns>

            </asp:GridView>





        </div>
    </form>
</body>
</html>
