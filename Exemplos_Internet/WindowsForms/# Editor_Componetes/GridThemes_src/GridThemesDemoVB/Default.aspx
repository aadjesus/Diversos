<%@ Page Language="VB" MasterPageFile="~/SampleGridThemes.master" Title="GridThemes Demo" %>

<%@ Register Assembly="GridThemes" Namespace="UNLV.IAP.GridThemes" TagPrefix="cc1" %>

<script runat="server">

    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsPostBack Then
            Dim t As System.Data.DataTable = DataProvider.CreateDataSource(4)
            gv1.DataSource = t
            gv2.DataSource = t
            gv3.DataSource = t

            gv1.DataBind()
            gv2.DataBind()
            gv3.DataBind()
        End If
    End Sub
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <b>GridThemes</b> is a framework which allows for
    the application of conditional formatting instructions to one or more GridView controls
    throughout an application using declarative constructs.  With GridThemes, a page designer
    may set a single property to change this grid:
    <br /><br />
    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="true" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GridLines="None" CellPadding="4" />
    <br />to this:<br /><br />
    <asp:GridView ID="gv2" runat="server" AutoGenerateColumns="true" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GridLines="None" CellPadding="4" />
    <br />or this:<br /><br />
    <asp:GridView ID="gv3" runat="server" AutoGenerateColumns="true" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GridLines="None" CellPadding="4" />
    <br />
    
    The framework is useful when conditional cell-by-cell formatting needs to be applied
    to GridViews throughout a project in a consistent way.  It is also useful when 
    <code>AutoGenerateColumns</code>
    is applied on a GridView and column formatting is otherwise unavailable.

    <br /><br />

    The framework combines:
    <ul>
        <li>a custom <code>BuildProvider</code>; the builder identifies files configured
        for GridThemes in the <i>App_Code</i> folder.  It interprets their declarative tags
        specifying conditions and formatting instructions, and
        generates programming methods that may respond to a GridView's <code>RowDataBound</code>
        event.
        </li>
        <li>a custom implementation of <code>IExtenderProvider</code>; the extender control, when present on
        a web form, adds the <code>GridTheme</code> property to all GridView controls on
        the form.</li><li>a custom subclass of <code>UITypeEditor</code>; the editor lists all GridThemes
        constructed by the builder for assignment to the <code>GridTheme</code> property</li></ul>
    Working together, these three custom classes allow for the conditional cell-by-cell
    formatting of GridView's in a declarative and reusable fashion.
    <br /><br />
    <cc1:GridThemesExtender ID="GridThemesExtender1" runat='server'>
        <Props>
            <cc1:ExtenderProperties GridID="gv2" GridTheme="Sample:  Expression attributes 2" />
            <cc1:ExtenderProperties GridID="gv3" GridTheme="Sample: Alternating Rows and Columns" />
        </Props>
    </cc1:GridThemesExtender>
</asp:Content>

