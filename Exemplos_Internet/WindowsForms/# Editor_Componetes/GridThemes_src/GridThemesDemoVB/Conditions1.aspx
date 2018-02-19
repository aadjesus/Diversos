<%@ Page Language="VB" MasterPageFile="~/SampleGridThemes.master"%>

<%@ Register Assembly="GridThemes" Namespace="UNLV.IAP.GridThemes" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="CreateDataSource"
        TypeName="DataProvider">
        <SelectParameters>
            <asp:Parameter DefaultValue="5" Name="numValueColumns" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" GridLines="None">
    </asp:GridView>
    <br />
    The real purpose for the GridThemes framework is to allow declarative conditional
    cell formatting, particularly useful when AutoGenerateColumns is in effect.
    Conditions are specified with the &lt;If&gt; tag and required <em>test</em> attribute.
    The <em>test</em> attribute value is an expression in the syntax of the project language,
    or a special predefined variable (see the listing below).  If the <em>test</em> attribute's
    value evaluates to true for a given cell, the formatting in nested &lt;Apply&gt; tag(s)
    is applied.
    
    In this example, numeric cells are right-aligned.&nbsp;<br />
    <pre>  &lt;theme id="Conditions1" title="Sample: If conditions"&gt;
    &lt;DataRow&gt;
      &lt;If test="IsNumeric"&gt;
        &lt;Apply horizontalAlign="right" /&gt;
      &lt;/If&gt;
    &lt;/DataRow&gt;
  &lt;/theme&gt;
</pre>
    
    <br />
A number of variables are predefined and may be used in condition testing or property expressions. They are as follows:
<table class='definitionTable'>
<tr valign='top'><td class='definitionTerm'>CellText</td><td class='definitionDesc'>The value of a given cell interpreted as a string</td>
</tr><tr valign='top'><td class='definitionTerm'>CellValue</td><td class='definitionDesc'>The numeric value of a given cell, interpreted as a double</td>
</tr><tr valign='top'><td class='definitionTerm'>CellIndex</td><td class='definitionDesc'>The 0-based index value of the current cell</td>
</tr><tr valign='top'><td class='definitionTerm'>RowIndex</td><td class='definitionDesc'>The 0-based index value of the current row</td>
</tr><tr valign='top'><td class='definitionTerm'>IsNumeric</td><td class='definitionDesc'>True if the value in the cell is numeric; false if not</td>
</tr><tr valign='top'><td class='definitionTerm'>IsNegative</td><td class='definitionDesc'>True if the value in the cell is numeric and less than zero; false if not</td>
</tr><tr valign='top'><td class='definitionTerm'>IsPositive</td><td class='definitionDesc'>True if the value in the cell is numeric and greater than zero; false if not</td>
</tr><tr valign='top'><td class='definitionTerm'>IsZero</td><td class='definitionDesc'>True if the value in the cell is numeric and equal to zero; false if not</td>
</tr><tr valign='top'><td class='definitionTerm'>IsNotNumeric</td><td class='definitionDesc'>True if the text in the cell is not numeric; false if the cell is numeric</td>
</tr><tr valign='top'><td class='definitionTerm'>IsNotNegative</td><td class='definitionDesc'>True if the value in the cell is numeric and greater than or equal to zero; false otherwise</td>
</tr><tr valign='top'><td class='definitionTerm'>IsNotPositive</td><td class='definitionDesc'>True if the value in the cell is numeric and less than or equal to zero; false otherwise</td>
</tr><tr valign='top'><td class='definitionTerm'>IsNotZero</td><td class='definitionDesc'>True if the value in the cell is numeric and not equal to zero; false otherwise</td>
</tr><tr valign='top'><td class='definitionTerm'>GroupText</td><td class='definitionDesc'>Used in &lt;Group&gt; tags to indicate the text value of the current category </td>
</tr><tr valign='top'><td class='definitionTerm'>GroupIndex</td><td class='definitionDesc'>Used in &lt;Group&gt; tags to indicate the 0-based index value of the current group</td>
</tr>
<tr valign='top'><td class='definitionTerm'>RowIndexWithinGroup</td><td class='definitionDesc'>Used in &lt;Group&gt; tags to indicate the 0-based index value of the current row within the current group. This value is reset to 0 with the first row of each group.</td></tr>
</table>    
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Sample: If conditions" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
