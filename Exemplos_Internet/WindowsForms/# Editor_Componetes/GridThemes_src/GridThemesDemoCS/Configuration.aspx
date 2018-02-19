<%@ Page Language="C#" MasterPageFile="~/SampleGridThemes.master" Title="GridThemes Demo" %>

<%@ Register Assembly="GridThemes" Namespace="UNLV.IAP.GridThemes" TagPrefix="cc1" %>

<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
To use GridThemes, configure your application by performing the following steps:
<ol>
    <li>
        Copy the file <i>GridThemes.dll</i> to your project’s bin directory.
    </li>
    
    <li>
        Activate the build provider by adding the following entry to the <code>&lt;buildProviders&gt;</code> section of your web.config file:
<pre>&lt;system.web&gt;
  &lt;compilation debug="false"&gt;
      
    &lt;buildProviders&gt;
      &lt;add extension=".gt" 
           type="UNLV.IAP.GridThemes.GridThemesBuildProvider, GridThemes"/&gt;
    &lt;/buildProviders&gt;

  &lt;/compilation&gt;
&lt;/system.web&gt;
</pre>       
This associates the GridThemes custom builder with any files in the App_Code folder with .gt extensions.  You may substitute a different extension if you wish.
    </li>
    <li>
        Create and store conditional GridTheme files in the App_Code folder.  Examples of the 
        XML syntax used for GridTheme files are shown throughout this demo application.
    </li>
    <li>
    Add a <code>GridThemeExtender</code> control to your GridView’s .aspx page.
    </li>
    <li>
    Assign the desired theme name to the GridView’s newly extended <code>GridTheme</code> property.
    </li>
</ol>


</asp:Content>

