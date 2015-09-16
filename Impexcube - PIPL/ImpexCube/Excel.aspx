<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Excel.aspx.cs" Inherits="Excelread.Excel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 49px;
        }
    </style>
</head>
<body> 
    <form id="form1" runat="server">
    <div>
        <table style="width: 68%;" align="center">
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:Button ID="btnHome" runat="server" onclick="btnHome_Click" Text="Home" />
                </td>
            </tr>
            <tr>
                <td>
                    File Name
                </td>
                <td>
                    &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" CssClass="tx4" />
                    <asp:Button ID="btnreadsheet" runat="server" CssClass="Blue-box1" OnClick="btnreadsheet_Click"
                        Text="Read Sheet" />
                </td>
                <td>
                    Sheet Name&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="drfile" runat="server" AutoPostBack="True" CssClass="tx3" OnSelectedIndexChanged="drfile_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Table Name
                </td>
                <td>
                    <asp:DropDownList ID="drtable" runat="server" OnSelectedIndexChanged="drtable_SelectedIndexChanged"
                        Width="160px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>            
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td>
                    <asp:ListBox ID="lstShowField" runat="server" Height="335px" Width="204px"></asp:ListBox>
                </td>
                <td style="text-align: center">
                    <asp:Button ID="Button3" Style="width: 31px; height: 26px;" runat="server" Text=" >"
                        OnClick="Button3_Click" />
                    <br />
                    <asp:Button ID="Addall" Style="width: 31px" Text=" >>" runat="server" OnClick="Addall_Click" />
                    <br />
                    <asp:Button ID="Button4" Style="width: 31px" runat="server" Text="< " OnClick="Button4_Click" /><br />
                    <asp:Button ID="RemoveAll" runat="server" Style="width: 31px" Text="<< " OnClick="RemoveAll_Click" />
                    <br />
                    <asp:Button ID="empty" runat="server" Text="Empty" OnClick="empty_Click" Width="42px" />
                </td>
                <td>
                    <asp:ListBox ID="lstView" runat="server" Height="331px" Width="202px"></asp:ListBox>
                </td>
                <td>
                    <asp:ListBox ID="dbview" runat="server" Height="324px" Width="235px"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Save" Width="70px" />
                </td>
            </tr>
        </table>
    </div>
    <p>
    </p>
    </form>
</body>
</html>
