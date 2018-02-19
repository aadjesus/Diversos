<%@ Page Language="VB" MasterPageFile="~/SampleGridThemes.master"%>

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

    This example builds on the previous, using <code>RowIndexWithinGroup</code>
    to alternate row colors within &lt;AlternateFormat&gt; blocks.
                
<pre>  &lt;Theme id="Group5" title="Grouping: Group variables 2"&gt;
    &lt;Group column="0" suppressRepeating="true"&gt;

      &lt;AlternateFormat&gt;
        &lt;If test='CellIndex=0'&gt;
          &lt;Apply BackColor='SkyBlue' /&gt;
          &lt;Else&gt;
            &lt;If test='RowIndexWithinGroup mod 2 = 0'&gt;
              &lt;Apply BackColor='SkyBlue' /&gt;
              &lt;ElseApply BackColor='LightBlue' /&gt;
            &lt;/If&gt;
          &lt;/Else&gt;
        &lt;/If&gt;
      &lt;/AlternateFormat&gt;

      &lt;AlternateFormat&gt;
        &lt;If test='CellIndex=0'&gt;
          &lt;Apply BackColor='LightGreen' /&gt;
          &lt;Else&gt;
            &lt;If test='RowIndexWithinGroup mod 2 = 0'&gt;
              &lt;Apply BackColor='LightGreen' /&gt;
              &lt;ElseApply BackColor='PaleGreen' /&gt;
            &lt;/If&gt;
          &lt;/Else&gt;
        &lt;/If&gt;
      &lt;/AlternateFormat&gt;

      &lt;if test="CellIndex=0 and RowIndexWithinGroup=0"&gt;
        &lt;apply Font-Bold="true" 
               TextExpression='string.Format("{0}. {1}", GroupIndex + 1, GroupText)' 
               /&gt;
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
            <cc1:ExtenderProperties GridID="GridView1" GridTheme="Grouping: Group variables 2" />
        </Props>
    </cc1:gridthemesextender>
    
</asp:Content>
