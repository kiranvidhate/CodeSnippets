<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EF-TPT-Demo.aspx.cs" Inherits="EF_Samples.EF_TPT_Demo" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="font-family: Arial">
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True"
            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
            <asp:ListItem Text="Load all Employees" Value="All"></asp:ListItem>
            <asp:ListItem Text="Load Permanent Employees" Value="Permanent"></asp:ListItem>
            <asp:ListItem Text="Load Contract Employees" Value="Contract"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <br />
        <asp:Button ID="btnAddPermanentEmployee" runat="server"
            Text="Add Permanent Employee"
            OnClick="btnAddPermanentEmployee_Click" />
        <br />
        <br />
        <asp:Button ID="btnAddContractEmployee" runat="server" Text="Add Contract Employee"
            OnClick="btnAddContractEmployee_Click" />
    </div>
</asp:Content>
