<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmsb.aspx.cs" Inherits="ImpexCube.frmsb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
       <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" style="font-size: 10pt" Text="Job No  : "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlJobNo" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True" CssClass="style5" Height="20px" Width="250px">
                        <asp:ListItem>~Select~</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnGenerate" runat="server" onclick="btnGenerate_Click" 
                        Text="Generate" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:TextBox ID="txtBeFile" runat="server" BackColor="Black" BorderStyle="None" 
                        ForeColor="White" Height="400px" TextMode="MultiLine" Width="900px"></asp:TextBox>
                </td>
            </tr>
    </table>
    </div>
    </form>
</body>
</html>
