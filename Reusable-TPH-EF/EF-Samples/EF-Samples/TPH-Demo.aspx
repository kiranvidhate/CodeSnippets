<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TPH-Demo.aspx.cs" Inherits="EF_Samples.TPH_Demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="Assets/css/style.css" />
    <style>
        body {
            padding: 10px;
            font: 14px/18px Calibri;
        }

        .bold {
            font-weight: bold;
        }

        td {
            padding: 5px;
            border: 1px solid #999;
        }

        p, output {
            margin: 10px 0 0 0;
        }

        #drop_zone {
            margin: 10px 0;
            width: 100%;
            min-height: 150px;
            text-align: center;
            text-transform: uppercase;
            font-weight: bold;
            border: 8px dashed #898;
            height: 160px;
        }
    </style>
</head>
<body>
    <div id="mainDiv">
        <div id="divHeader">
            <div id="divHeader2">
                <div id="divLogo">
                    <h1>Table Per Hierarchy (TPH) in Entity Framework
                    </h1>
                </div>
                <div id="divMenu">
                </div>
            </div>
        </div>
        <div id="divMain">
            <div id="divMain2">
                <div id="divContent">
                    <form id="form1" runat="server">
                        <div style="font-family: Arial">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Load all Employees" Value="All"></asp:ListItem>
                                <asp:ListItem Text="Load Permanent Employees" Value="Permanent"></asp:ListItem>
                                <asp:ListItem Text="Load Contract Employees" Value="Contract"></asp:ListItem>
                            </asp:RadioButtonList><br />
                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="True">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                <SortedDescendingHeaderStyle BackColor="#820000" />
                            </asp:GridView>

                            <br />
                            <asp:Button ID="btnAddPermanentEmployee" runat="server" CssClass="btn" 
                                Text="Add Permanent Employee" OnClick="btnAddPermanentEmployee_Click" />
                           &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAddContractEmployee" runat="server" CssClass="btn" 
                                Text="Add Contract Employee" OnClick="btnAddContractEmployee_Click" />
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
