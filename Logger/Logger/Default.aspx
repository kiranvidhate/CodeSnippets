<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Label ID="lblMessage" runat="server" ForeColor="#0000ff" /><br /><br />

        <asp:Button ID="btnLog" Text="Log Exception" runat="server" OnClick="btnLog_Click" CssClass="btn"/>
         <asp:Button ID="btnLogAuditTrail" Text="Log Audit Trail" runat="server" OnClick="btnLogAuditTrail_Click" CssClass="btn"/>
    </div>
</asp:Content>
