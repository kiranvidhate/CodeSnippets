<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="UserControl/MultipleFileUpload.ascx" TagName="MultipleFileUpload" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Label ID="lblMessage" runat="server" ForeColor="#0000ff" /><br /><br />
        <uc1:MultipleFileUpload ID="MultipleFileUpload1" OnClick="MultipleFileUpload1_Click" runat="server" UpperLimit="3" Rows="6" />
        <asp:Button ID="btnMerge" Text="Merge PDFs" runat="server" OnClick="btnMerge_Click" CssClass="btn"/>
    </div>
</asp:Content>
