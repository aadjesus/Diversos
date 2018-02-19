<%@ Page Language="C#" MasterPageFile="~/SampleGridThemes.master"%>

<%@ Register Assembly="GridThemes" Namespace="UNLV.IAP.GridThemes" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="CreateCategoryDataSource"
        TypeName="DataProvider">
        <SelectParameters>
            <asp:Parameter DefaultValue="5" Name="numValueColumns" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" GridLines="None">
    </asp:GridView>
    <br />

    The variables <code>GroupIndex</code>, <code>GroupText</code>, and 
    <code>RowIndexWithinGroup</code> are appropriately used in condition testing
    and expressions within the &lt;Group&gt; tag.  This example demonstrates
    using GroupIndex and GroupText to apply numbering to each group category,
    and RowIndexWithinGroup to apply line numbering within a group.
            
<pre>  &lt;Theme id="Group4" title="Grouping: Group variables"&gt;
    &lt;Group column="0" suppressRepeating="true"&gt;

      &lt;AlternateFormat&gt;
        &lt;Apply backColor="LightBlue" /&gt;
      &lt;/AlternateFormat&gt;

      &lt;AlternateFormat&gt;
        &lt;Apply backColor="PaleGreen" /&gt;
      &lt;/AlternateFormat&gt;

      &lt;if test="CellIndex==0"&gt;
        &lt;if test="RowIndexWithinGroup==0"&gt;
          &lt;apply Font-Bold="true" 
                 TextExpression='string.Format("{0}. {1}", GroupIndex + 1, GroupText)' 
                 /&gt;
          &lt;elseApply Font-Italic="true"
                     TextExpression='string.Format("{0}.", RowIndexWithinGroup + 1)'
                     HorizontalAlign='right'
                     /&gt;
        
        &lt;/if&gt;
      &lt;/if&gt;

      &lt;if test="IsNumeric"&gt;
        &lt;apply horizontalAlign="right" /&gt;
      &lt;/if&gt;

    &lt;/Group&gt;

    &lt;Header&gt;
      &lt;Apply backColor="#CCCCCC" /&gt;
    &lt;/Header&gt;
  &lt;/Theme&gt;
</pre>            
    <br />
    <br />
    <cc1:gridthemesextender id="GridThemesExtender1" runat="server">
        <Props>
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Grouping: Group variables" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
