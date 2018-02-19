<%@ Page Language="c#" Inherits="WebServiceAuthentication.TestAuth" CodeFile="TestAuth.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>TestAuth</title>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <asp:Label runat="server" ID="ServResponse" BackColor="#ffcc00">WebServiceResponse</asp:Label>
    <hr />
    <asp:Label ID="LabelUser" runat="server" Width="72px" Height="24px">UserName: </asp:Label>
    <asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelPwd" runat="server" Width="72px" Height="24px">Password: </asp:Label>
    <asp:TextBox ID="TextBoxPwd" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelToken" runat="server" Width="72px" Height="24px">Token: </asp:Label>
    <asp:TextBox ID="TextBoxToken" runat="server" Width="300px"></asp:TextBox><asp:Label ID="LabelTokendate" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Button ID="ButtonAuth" runat="server" Text="TestAuthentication to ANY service" OnClick="ButtonAuth_Click"></asp:Button>
    <br />
    <br />
    There are two users allowed: "MyUserName" (that has a level = "1") and "OtherUser" (that has a level = "2")<br />
    <hr />
    The password to get authentication is: SeCrEt+"ServiceName".<br/>
    That is, if the "ServiceName" is "Test1" the password must be "SeCrEtTest1"<br/>
    Please note that in this sample the password is case sensisitive, while the username is not.
    <br />
    <br />
    <asp:Label ID="LabelSrv" runat="server" Width="72px" Height="24px">Service: </asp:Label>
    <asp:TextBox ID="TextBoxService" runat="server">ServiceName</asp:TextBox>
    <br />
    <br />
    <asp:Button ID="ButtonService" runat="server" Text="TestService" OnClick="ButtonService_Click"></asp:Button>
    Test Service Authorization using local clock
    <br />
    <br />
    <asp:Button ID="ButtonGetToken" runat="server" Text="GetToken" OnClick="ButtonGetToken_Click"></asp:Button>
    Service GetToken (the token you get here is valid during the current minute and the following)
    <br />
    <asp:Button ID="ButtonUseToken" runat="server" Text="TestService" OnClick="ButtonUseToken_Click"></asp:Button>
    Test Service Authorization using Token
    <br />
    <asp:Label ID="LabelNowDate" runat="server"></asp:Label>
    </form>
</body>
</html>
