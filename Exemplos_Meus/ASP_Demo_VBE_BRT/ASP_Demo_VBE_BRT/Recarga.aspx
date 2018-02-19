<%@ Page Title="Recarga" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Recarga.aspx.cs" Inherits="ASP_Demo_VBE_BRT.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 445px;
        }
        .style2
        {
            width: 231px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <table style="width: 100%;">
                    <tr>
                        <td class="style2">
                            <input id="Button1" type="button" value="R$2,75" style="width: 80%;" />
                        </td>
                        <td>
                            <input id="Button2" type="button" value="R$5,00" style="width: 80%;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <input id="Button3" type="button" value="R$10,00" style="width: 80%;" />
                        </td>
                        <td>
                            <input id="Button4" type="button" value="R$20,00" style="width: 80%;" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
