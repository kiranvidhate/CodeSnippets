<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAPIDemo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtProductId" runat="server" />
        </div>
        <div>
            <asp:Button ID="btnTestAPI" Text="Get Product(s)" runat="server" OnClick="btnTestAPI_Click" />
        </div>
        <div>
            <u><b>Results:</b></u><br /><br />
            <asp:Label ID="lblResults" runat="server" />

        </div>
    </form>
</body>
</html>
